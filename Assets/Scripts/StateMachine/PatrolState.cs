using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : AiState
{
    // Agent Config References
    float RandomWalkPointX, RandomWalkPointZ, playerCheckRadius, playerAttackRadius;

    // Agent References
    private Transform enemyTransform;
    private Transform playerTransform;
    private NavMeshAgent enemyAgent;
    private LayerMask whatIsGround, whatIsPlayer;
    private Coroutines coroutines;
    [SerializeField] AiStateId FinishState;

    // Variables
    public Vector3 walkPoint;
    public bool isWalkPointSet = false;
    public GameObject walkpointDebug;

    void Awake()
    {
        id = AiStateId.PatrolState;
    }

    public override void Enter(AiAgent agent)
    {
        RandomWalkPointX = agent.config.RandomWalkPointX;
        RandomWalkPointZ = agent.config.RandomWalkPointZ;
        playerCheckRadius = agent.config.playerCheckRadius;
        playerAttackRadius = agent.config.playerAttackRadius;
        enemyTransform = agent.enemyTransform;
        playerTransform = agent.player;
        enemyAgent = agent.NavMeshAgent;
        whatIsGround = agent.whatIsGround;
        whatIsPlayer = agent.whatIsPlayer;
        coroutines = agent.GetComponent<Coroutines>();
        walkpointDebug = agent.walkpointDebug;

        isWalkPointSet = false;
        enemyAgent.ResetPath();
        enemyAgent.speed = agent.config.agentPatrolSpeed;
    }


    public override void Tick(AiAgent agent)
    {
        if (CheckPlayerDistance())
        {
            coroutines.StopCoroutines();
            agent.stateMachine.ChangeState(FinishState);
        }



        WalkPointDebug();
        if (!isWalkPointSet && !coroutines.isWaiting)
        {
            RandomWalkPointX = Random.Range(-20, 20);
            RandomWalkPointZ = Random.Range(-20, 20);
            walkPoint = new Vector3(enemyTransform.position.x + RandomWalkPointX, enemyTransform.position.y, enemyTransform.position.z + RandomWalkPointZ);
            if (Physics.Raycast(walkPoint, -enemyTransform.up, 20f, agent.whatIsGround))
            {
                enemyAgent.SetDestination(walkPoint);
                isWalkPointSet = true;
            }
        }
        if ((walkPoint - enemyTransform.position).magnitude < 2.2f)
        {
            if (!coroutines.isWaiting)
            {
                coroutines.StartCoroutineWait2Seconds();
            }
            isWalkPointSet = false;
        }
    }

    public override void Exit(AiAgent agent)
    {
        isWalkPointSet = false;
    }


    bool CheckPlayerDistance()
    {
        return Physics.CheckSphere(enemyTransform.position, playerCheckRadius, whatIsPlayer);
    }


    void WalkPointDebug()
    {
        walkpointDebug.transform.position = walkPoint;
    }




}
