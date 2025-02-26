using UnityEngine;
using UnityEngine.AI;

public class ChasePlayerState : AiState
{

    NavMeshAgent enemyAgent;
    Transform player;
    Transform enemyTransform;
    float playerCheckRadius;
    LayerMask whatIsPlayer;

    public AiStateId GetId()
    {
        return AiStateId.ChasePlayer;
    }

    public void Enter(AiAgent agent)
    {
        enemyAgent = agent.NavMeshAgent;
        player = agent.player;
        enemyTransform = agent.enemyTransform;
        playerCheckRadius = agent.config.playerCheckRadius;
        whatIsPlayer = agent.whatIsPlayer;

        enemyAgent.speed = agent.config.agentChaseSpeed;
    }

    public void Update(AiAgent agent)
    {
        enemyAgent.SetDestination(agent.player.position);

        if (!CheckPlayerDistance())
        {
            agent.stateMachine.ChangeState(AiStateId.PatrolState);
        }
    }

    public void Exit(AiAgent agent)
    {
    }

    bool CheckPlayerDistance()
    {
        return Physics.CheckSphere(enemyTransform.position, playerCheckRadius, whatIsPlayer);
    }
}