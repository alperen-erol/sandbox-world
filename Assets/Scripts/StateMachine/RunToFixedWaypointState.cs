using System.Collections;
using System.Data.Common;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class RunToFixedWaypointState : AiState
{
    public Transform[] waypoints;
    private Transform enemyTransform;
    private Transform playerTransform;
    public AiAgentConfig config;
    public NavMeshAgent enemyAgent;
    public LayerMask whatIsPlayer;
    FixedPatrol fixedPatrol;

    float playerCheckRadius, agentWalkSpeed, agentChaseSpeed;
    public int selectedIndex, previousIndex;
    public bool isWalkPointSet = false;
    public float distance;
    public float maxDistance = 0f;
    Transform farthestWaypoint;


    public override void Enter(AiAgent agent)
    {
        fixedPatrol = GetComponent<FixedPatrol>();
        waypoints = fixedPatrol.waypoints;
        enemyAgent = agent.NavMeshAgent;
        enemyTransform = GetComponent<Transform>();
        playerCheckRadius = agent.config.playerCheckRadius;
        playerTransform = agent.player;
        Debug.Log("enter runwaypoint state");
        agentWalkSpeed = config.agentPatrolSpeed;
        agentChaseSpeed = config.agentChaseSpeed;
    }

    public bool foundFarthestWaypoint;
    public override void Tick(AiAgent agent)
    {
        if (!foundFarthestWaypoint)
        {
            FindFarthestWaypoint();
            enemyAgent.SetDestination(farthestWaypoint.position);
            enemyAgent.speed = agentChaseSpeed;
        }


        if (Vector3.Distance(enemyAgent.transform.position, farthestWaypoint.position) < 1f)
        {
            StartCoroutine(Wait1s());
        }

        if (changeState)
        {
            agent.stateMachine.ChangeState(AiStateId.FixedPatrol);
        }
    }


    public override void Exit(AiAgent agent)
    {
        foundFarthestWaypoint = false;
        changeState = false;
        Debug.Log("exiting runwaypointstate");
    }


    Transform FindFarthestWaypoint()
    {
        foreach (Transform waypoint in waypoints)
        {
            distance = Vector3.Distance(enemyTransform.position, waypoint.position);
            if (distance > maxDistance)
            {
                maxDistance = distance;
                farthestWaypoint = waypoint;
            }
        }
        foundFarthestWaypoint = true;

        return farthestWaypoint;
    }


    bool changeState = false;
    IEnumerator Wait1s()
    {
        yield return new WaitForSeconds(1);
        changeState = true;
    }


}
