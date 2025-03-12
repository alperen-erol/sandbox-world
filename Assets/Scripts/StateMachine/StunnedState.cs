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
    [SerializeField] Vector3 knockbackDirection;
    [SerializeField] float knockbackDirectionY;
    [SerializeField] float knockbackForce;
    [SerializeField] float stunDuration;


    public override void Enter(AiAgent agent)
    {
        rb = GetComponent<Rigidbody>();
        navMeshAgent = agent.NavMeshAgent;
        enemyTransform = agent.enemyTransform;
        playerTransform = agent.player;
        navMeshAgent.speed = 0f;

        navMeshAgent.enabled = false;
        StopAllCoroutines();
        StartCoroutine(ApplyForce(agent));
        Debug.Log("Enter Stunned STate");
    }

    public override void Tick(AiAgent agent)
    {
    }

    public override void Exit(AiAgent agent)
    {
        rb.isKinematic = true;
        navMeshAgent.enabled = true;
    }

    void HandleApplyForce()
    {
        rb.isKinematic = false;
        knockbackDirection = (enemyTransform.position - playerTransform.position).normalized;
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
        rb.AddForce(knockbackDirectionY * Vector3.up, ForceMode.Impulse);
    }

    IEnumerator ApplyForce(AiAgent agent)
    {
        yield return new WaitForSeconds(0.12f);
        HandleApplyForce();
        yield return new WaitForSeconds(stunDuration);

        agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
    }
}

