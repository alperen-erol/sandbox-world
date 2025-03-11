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

        enemyAgent.speed = 16;
    }

    public override void Tick(AiAgent agent)
    {
        if (agent.player != null)
        {
            enemyAgent.SetDestination(agent.player.position);
            if (Vector3.Distance(transform.position, agent.player.transform.position) < 2f)
            {
                enemyAgent.SetDestination(transform.position);
            }


            if (CheckPlayerDistance())
            {
                enemyAgent.speed = 3f;
            }
            else
                enemyAgent.speed = 16f;
        }
        else
            enemyAgent.SetDestination(enemyTransform.position);
    }

    public override void Exit(AiAgent agent)
    {
    }

    bool CheckPlayerDistance()
    {
        return Physics.CheckSphere(enemyTransform.position, 1.8f, whatIsPlayer);
    }
}