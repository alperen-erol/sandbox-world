using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class EnemyStateMachine : MonoBehaviour
{
    public enum CharacterState { Patrol, Wait, Chase, Attack }

    private NavMeshAgent agent;
    public CharacterState currentState;
    public float RandomWalkPointX, RandomWalkPointZ;
    public bool isWalkPointSet;

    [SerializeField] LayerMask whatIsGround;
    Vector3 walkPoint;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        currentState = CharacterState.Patrol;
    }

    void Update()
    {
        switch (currentState)
        {
            case CharacterState.Patrol:
                PatrolState();
                break;
            case CharacterState.Attack:
                AttackState();
                break;
            case CharacterState.Chase:
                ChaseState();
                break;
            case CharacterState.Wait:
                WaitState();
                break;
        }
    }

    void PatrolState()
    {
        if (!isWalkPointSet)
        {
            RandomWalkPointX = Random.Range(-20, 20);
            RandomWalkPointZ = Random.Range(-20, 20);
            walkPoint = new Vector3(transform.position.x + RandomWalkPointX, transform.position.y, transform.position.z + RandomWalkPointZ);
            if (Physics.Raycast(walkPoint, -transform.up, 20f, whatIsGround))
            {
                agent.SetDestination(walkPoint);
                isWalkPointSet = true;
            }
            Debug.Log(walkPoint);
        }
        if ((walkPoint - transform.position).magnitude < 2.2f)
        {
            isWalkPointSet = false;
            ChangeState(CharacterState.Wait);
        }
    }

    bool isWaiting = false;

    IEnumerator Wait2Seconds()
    {
        isWaiting = true;
        Debug.Log("Waiting");
        yield return new WaitForSeconds(2);
        ChangeState(CharacterState.Patrol);
        isWaiting = false;
    }

    void WaitState()
    {
        if (!isWaiting)
        {
            StartCoroutine(Wait2Seconds());
        }
    }

    void ChaseState()
    {

    }

    void AttackState()
    {

    }


    public void ChangeState(CharacterState newState)
    {
        currentState = newState;
    }
}