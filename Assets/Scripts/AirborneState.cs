using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AirborneState : AiState
{
    Rigidbody rb;

    public override void Enter(AiAgent agent)
    {
        rb = GetComponent<Rigidbody>();
        agent.NavMeshAgent.enabled = false;
        rb.isKinematic = false;
        rb.useGravity = false;
        rb.linearDamping = 10f;
    }

    public override void Tick(AiAgent agent)
    {
    }

    public override void Exit(AiAgent agent)
    {
        rb.linearDamping = 1f;
        rb.useGravity = true;
    }

}

