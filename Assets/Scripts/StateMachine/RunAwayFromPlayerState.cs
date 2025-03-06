using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class RunAwayFromPlayerState : AiState
{
    Transform enemyTransform;
    Transform playerTransform;
    LayerMask whatIsPlayer;
    LayerMask obstacleMask;
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
        obstacleMask = LayerMask.GetMask("Obstacle"); // Make sure walls are on the "Obstacle" layer
        enemyAgent = agent.NavMeshAgent;
        playerCheckRadius = agent.config.playerCheckRadius;
        chaseSpeed = config.agentChaseSpeed;

        enemyAgent.speed = chaseSpeed;
    }

    public override void Tick(AiAgent agent)
    {
        if (!CheckPlayerDistance())
        {
            return;
        }

        runDirection = (enemyTransform.position - playerTransform.position).normalized;
        float runAwayDistance = 2f;
        walkPoint = enemyTransform.position + runDirection * runAwayDistance;

        if (Physics.Raycast(enemyTransform.position, runDirection, 3f, obstacleMask))
        {
            Debug.Log("Wall detected! Adjusting run direction.");
            walkPoint = FindAlternativePath();
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

    Vector3 FindAlternativePath()
    {
        Vector3 rightDir = Quaternion.Euler(0, 90, 0) * runDirection;
        Vector3 leftDir = Quaternion.Euler(0, -90, 0) * runDirection;

        bool rightClear = !Physics.Raycast(enemyTransform.position, rightDir, 2f, obstacleMask);
        bool leftClear = !Physics.Raycast(enemyTransform.position, leftDir, 2f, obstacleMask);

        if (rightClear) return enemyTransform.position + rightDir * 5f;
        if (leftClear) return enemyTransform.position + leftDir * 5f;

        return enemyTransform.position - runDirection * 3f; // If stuck, move slightly backward
    }
}
