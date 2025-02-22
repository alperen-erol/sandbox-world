using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class EnemyStateMachine : MonoBehaviour
{
    public enum CharacterState { Patrol, Wait, Chase, Attack }

    [Header("References")]
    [SerializeField] Transform player;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] LayerMask whatIsGround;
    private NavMeshAgent agent;

    [Header("Variables")]
    Vector3 walkPoint;
    public CharacterState currentState;
    public float RandomWalkPointX, RandomWalkPointZ;
    public bool isWalkPointSet;
    public float playerCheckRadius = 5f;
    public float playerAttackRadius = 2f;


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
        if (CheckPlayerDistance())
        {
            StopAllCoroutines();
            ChangeState(CharacterState.Chase);
        }

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
    void WaitState()
    {
        if (!isWaiting)
        {
            StartCoroutine(Wait2Seconds());
        }
    }


    void ChaseState()
    {
        isWalkPointSet = false;
        agent.SetDestination(player.transform.position);
        if (CheckPlayerAttackDistance())
        {
            ChangeState(CharacterState.Attack);
        }
        if (!CheckPlayerDistance())
        {
            ChangeState(CharacterState.Patrol);
        }
    }


    void AttackState()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        //attack

        if (!CheckPlayerAttackDistance())
        {
            ChangeState(CharacterState.Chase);
        }
    }


    public void ChangeState(CharacterState newState)
    {
        currentState = newState;
    }


    bool CheckPlayerDistance()
    {
        return Physics.CheckSphere(transform.position, playerCheckRadius, playerLayer);
    }

    bool CheckPlayerAttackDistance()
    {
        return Physics.CheckSphere(transform.position, playerAttackRadius, playerLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, playerCheckRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerAttackRadius);
    }

    IEnumerator Wait2Seconds()
    {
        isWaiting = true;
        Debug.Log("Waiting");
        yield return new WaitForSeconds(2);
        ChangeState(CharacterState.Patrol);
        isWaiting = false;
    }
}