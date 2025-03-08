using UnityEngine;
using UnityEngine.AI;

public class ChasePlayerState : AiState
{

    NavMeshAgent enemyAgent;
    Transform player;
    Transform enemyTransform;
    float playerCheckRadius;
    LayerMask whatIsPlayer;



    public override void Enter(AiAgent agent)
    {
        enemyAgent = agent.NavMeshAgent;
        player = agent.player;
        enemyTransform = agent.enemyTransform;
        playerCheckRadius = agent.config.playerCheckRadius;
        whatIsPlayer = agent.whatIsPlayer;

        enemyAgent.speed = agent.config.agentChaseSpeed;
    }

    public override void Tick(AiAgent agent)
    {
        enemyAgent.SetDestination(agent.player.position);
        if (Vector3.Distance(transform.position, agent.player.transform.position) < 2f)
        {
            enemyAgent.SetDestination(transform.position);
        }
        // Debug.Log(Vector3.Distance(transform.position, agent.player.transform.position));
    }

    public override void Exit(AiAgent agent)
    {
    }

    // bool CheckPlayerDistance()
    // {
    //     return Physics.CheckSphere(enemyTransform.position, playerCheckRadius, whatIsPlayer);
    // }
}