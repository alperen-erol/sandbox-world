using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class StunnedState : AiState
{

    NavMeshAgent enemyAgent;
    Transform player;
    Transform enemyTransform;
    Transform playerTransform;
    Rigidbody rb;
    NavMeshAgent navMeshAgent;
    Vector3 knockbackDirection;

    [SerializeField] float knockbackForce;


    public override void Enter(AiAgent agent)
    {
        rb = GetComponent<Rigidbody>();
        navMeshAgent = agent.NavMeshAgent;
        enemyTransform = agent.enemyTransform;
        playerTransform = agent.player;
        navMeshAgent.ResetPath();
        navMeshAgent.enabled = false;


        Debug.Log("Enter Stunned STate");

    }

    public override void Tick(AiAgent agent)
    {

    }

    public override void Exit(AiAgent agent)
    {
    }

}

