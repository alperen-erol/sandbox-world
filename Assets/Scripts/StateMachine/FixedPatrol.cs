using System.Collections;
using System.Data.Common;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class FixedPatrol : AiState
{
    public Transform[] waypoints;
    private Transform enemyTransform;
    public NavMeshAgent enemyAgent;
    public LayerMask whatIsPlayer;
    public AiAgentConfig config;

    float playerCheckRadius, agentWalkSpeed, agentChaseSpeed;
    public int selectedIndex, previousIndex;
    public bool isWalkPointSet = false;


    public override void Enter(AiAgent agent)
    {
        enemyAgent = agent.NavMeshAgent;
        enemyTransform = GetComponent<Transform>();
        playerCheckRadius = config.playerCheckRadius;
        Debug.Log("enter patrol state");
        enemyAgent.speed = config.agentChaseSpeed;
    }


    public override void Tick(AiAgent agent)
    {
        if (!isWalkPointSet && !isWaiting)
        {
            MoveAgent();
        }
        if (Vector3.Distance(enemyAgent.transform.position, waypoints[selectedIndex].position) < 1f)
        {
            if (isWaiting == false)
                StartCoroutine(Wait1s());

            isWalkPointSet = false;
            previousIndex = selectedIndex;
        }

        if (CheckPlayerDistance())
        {
            agent.stateMachine.ChangeState(AiStateId.RunToFixedWaypointState);
        }
    }


    public override void Exit(AiAgent agent)
    {
        isWalkPointSet = false;
        isWaiting = false;
        Debug.Log("exiting patrolstate");

    }


    void MoveAgent()
    {
        selectedIndex = Random.Range(0, waypoints.Length);
        if (selectedIndex != previousIndex)
        {
            enemyAgent.SetDestination(waypoints[selectedIndex].position);
            isWalkPointSet = true;
        }
    }


    bool isWaiting = false;
    IEnumerator Wait1s()
    {
        isWaiting = true;
        yield return new WaitForSeconds(1);
        isWaiting = false;
    }


    bool CheckPlayerDistance()
    {
        return Physics.CheckSphere(enemyTransform.position, playerCheckRadius, whatIsPlayer);
    }

}
