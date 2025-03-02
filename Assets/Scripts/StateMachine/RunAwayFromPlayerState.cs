using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class RunAwayFromPlayerState : AiState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    Transform enemyTransform;
    Transform playerTransform;
    LayerMask whatIsPlayer;
    NavMeshAgent enemyAgent;

    float playerCheckRadius;
    public Transform runDirectionDebug;

    Vector3 runDirection;
    Vector3 walkPoint;

    public override void Enter(AiAgent agent)
    {
        enemyTransform = agent.enemyTransform;
        playerTransform = agent.player;
        whatIsPlayer = agent.whatIsPlayer;
        enemyAgent = agent.NavMeshAgent;
        playerCheckRadius = agent.config.playerCheckRadius;
    }


    public override void Tick(AiAgent agent)
    {
        if (!CheckPlayerDistance())
        {
            agent.stateMachine.ChangeState(AiStateId.RunAwayPatrol);
            Debug.Log("exiting run state");
        }
        runDirection = (enemyTransform.position - playerTransform.position).normalized;
        float runAwayDistance = 5f;
        walkPoint = enemyTransform.position + runDirection * runAwayDistance;
        Debug.Log(walkPoint);
        enemyAgent.SetDestination(walkPoint);
    }

    public override void Exit(AiAgent agent)
    {

    }


    bool CheckPlayerDistance()
    {
        return Physics.CheckSphere(enemyTransform.position, playerCheckRadius, whatIsPlayer);
    }

    private void OnDrawGizmos()
    {

    }
}
