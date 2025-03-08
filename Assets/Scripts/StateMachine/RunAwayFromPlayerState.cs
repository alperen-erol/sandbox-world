using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class RunAwayFromPlayerState : AiState
{
    Transform enemyTransform;
    Transform playerTransform;
    LayerMask whatIsPlayer;
    LayerMask Obstacle;
    NavMeshAgent enemyAgent;
    public AiAgentConfig config;

    float playerCheckRadius, chaseSpeed;
    Vector3 runDirection;
    Vector3 walkPoint;

    public override void Enter(AiAgent agent)
    {
        enemyTransform = agent.enemyTransform;
        playerTransform = agent.player;
        whatIsPlayer = agent.whatIsPlayer;
        Obstacle = LayerMask.GetMask("Obstacle");
        enemyAgent = agent.NavMeshAgent;
        playerCheckRadius = agent.config.playerCheckRadius;
        chaseSpeed = config.agentChaseSpeed;

        enemyAgent.speed = chaseSpeed;
    }


    public override void Tick(AiAgent agent)
    {
        if (!CheckPlayerDistance())
        {
            agent.stateMachine.ChangeState(AiStateId.PatrolState);
        }

        runDirection = (enemyTransform.position - playerTransform.position).normalized;
        float runAwayDistance = 2f;
        walkPoint = enemyTransform.position + runDirection * runAwayDistance;

        if (Physics.Raycast(enemyTransform.position, runDirection, 7f, Obstacle))
        {
            Debug.Log("Wall detected! Adjusting run direction.");
            walkPoint = FindDifferentPath();
        }

        enemyAgent.SetDestination(walkPoint);
    }


    public override void Exit(AiAgent agent)
    {
    }


    bool CheckPlayerDistance()
    {
        return Physics.CheckSphere(enemyTransform.position, playerCheckRadius, whatIsPlayer);
    }


    Vector3 rightDir;
    Vector3 leftDir;
    bool rightClear;
    bool leftClear;

    Vector3 FindDifferentPath()
    {
        rightDir = Quaternion.Euler(0, 90, 0) * runDirection;
        leftDir = Quaternion.Euler(0, -90, 0) * runDirection;

        rightClear = !Physics.Raycast(enemyTransform.position, rightDir, 3f, Obstacle);
        leftClear = !Physics.Raycast(enemyTransform.position, leftDir, 3f, Obstacle);

        if (rightClear) return enemyTransform.position + rightDir * 5f;
        if (leftClear) return enemyTransform.position + leftDir * 5f;

        return enemyTransform.position;
    }


    // void OnDrawGizmosSelected()
    // {


    //     {
    //         Gizmos.color = Color.red;
    //         Gizmos.DrawRay(enemyTransform.position, runDirection * 5f);
    //     }
    // }
}
