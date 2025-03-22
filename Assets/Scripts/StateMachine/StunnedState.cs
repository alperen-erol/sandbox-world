using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class StunnedState : AiState
{
    [Header("Force Settings")]
    [Header("Hammer Knockback")]
    [SerializeField] private float hammerKnockbackX;
    [SerializeField] private float hammerKnockbackY;
    [SerializeField] private float hammerStunCooldown;

    [Header("Scepter Knockback")]
    [SerializeField] private float scepterKnockbackX;
    [SerializeField] private float scepterKnockbackY;
    [SerializeField] private float scepterStunCooldown;

    [Header("Stone Knockback")]
    [SerializeField] private float stoneKnockbackX;
    [SerializeField] private float stoneKnockbackY;
    [SerializeField] private float stoneStunCooldown;

    [Header("Obstacle Destruction")]
    [SerializeField] private float obstacleDestructionX;
    [SerializeField] private float obstacleDestructionY;
    [SerializeField] private float obstacleStunCooldown;

    //aistatedeki stun tipi kulandım
    public StunType selectedForceType;

    [SerializeField] Vector3 knockbackDirection;
    float knockbackDirectionY;
    float knockbackForce;
    [SerializeField] float stunDuration;

    Transform player;
    Transform enemyTransform;
    Transform playerTransform;
    Rigidbody rb;
    NavMeshAgent navMeshAgent;


    public override void Enter(AiAgent agent)
    {
        Debug.Log("Enter Stunned STate");
        navMeshAgent = agent.NavMeshAgent;
        navMeshAgent.isStopped = true;
        navMeshAgent.ResetPath();
        navMeshAgent.enabled = false;
        rb = GetComponent<Rigidbody>();
        enemyTransform = agent.enemyTransform;
        playerTransform = agent.player;
        rb.useGravity = true;
        rb.isKinematic = false;
        ApplyForce(agent);
    }


    public void ApplyForce(AiAgent agent)
    {
        switch (selectedForceType)
        {
            case StunType.HammerKnockback:
                ApplyKnockback(agent, hammerKnockbackX, hammerKnockbackY, hammerStunCooldown);
                break;
            case StunType.ScepterKnockback:
                ApplyKnockback(agent, scepterKnockbackX, scepterKnockbackY, scepterStunCooldown);
                break;
            case StunType.StoneKnockBack:
                ApplyKnockback(agent, stoneKnockbackX, stoneKnockbackY, stoneStunCooldown);
                break;
            case StunType.ObstacleDestruction:
                ApplyKnockback(agent, obstacleDestructionX, obstacleDestructionY, obstacleStunCooldown);
                break;
        }
    }


    private void ApplyKnockback(AiAgent agent, float knockBackX, float knockBackY, float cooldown)
    {
        Debug.Log("Applying Knockback: " + selectedForceType);
        knockbackDirection = (enemyTransform.position - playerTransform.position).normalized;
        rb.AddForce(knockbackDirection * knockBackX, ForceMode.Impulse);
        rb.AddForce(Vector3.up * knockBackY, ForceMode.Impulse);
        StopAllCoroutines();
        StartCoroutine(HammerStunDuration(cooldown, agent));
    }


    public override void Tick(AiAgent agent)
    {
    }


    public override void Exit(AiAgent agent)
    {
    }


    IEnumerator HammerStunDuration(float stunTime, AiAgent agent)
    {
        yield return new WaitForSeconds(stunTime);
        agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
    }
}

