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
                ApplyHammerKnockBack(agent);
                break;
            case StunType.ScepterKnockback:
                ApplyScepterKnockback(agent);
                break;
            case StunType.StoneKnockBack:
                ApplyStoneKnockBack(agent);
                break;
            case StunType.ObstacleDestruction:
                ApplyObstacleDestruction(agent);
                break;
        }
    }

    private void ApplyHammerKnockBack(AiAgent agent)
    {
        Debug.Log("Applying Hammer Knockback");
        knockbackDirection = (enemyTransform.position - playerTransform.position).normalized;
        rb.AddForce(knockbackDirection * hammerKnockbackX, ForceMode.Impulse);
        rb.AddForce(Vector3.up * hammerKnockbackY, ForceMode.Impulse);
        StopAllCoroutines();
        StartCoroutine(HammerStunDuration(hammerStunCooldown, agent));
    }

    private void ApplyScepterKnockback(AiAgent agent)
    {
        Debug.Log("Applying Scepter Knockback");
        knockbackDirection = (enemyTransform.position - playerTransform.position).normalized;
        rb.AddForce(knockbackDirection * scepterKnockbackX, ForceMode.Impulse);
        rb.AddForce(Vector3.up * scepterKnockbackY, ForceMode.Impulse);
        StopAllCoroutines();
        StartCoroutine(HammerStunDuration(scepterStunCooldown, agent));
    }


    private void ApplyObstacleDestruction(AiAgent agent)
    {
        Debug.Log("Object destroyed apply force");
        knockbackDirection = (enemyTransform.position - playerTransform.position).normalized;
        rb.AddForce(knockbackDirection * obstacleDestructionX, ForceMode.Impulse);
        rb.AddForce(Vector3.up * obstacleDestructionY, ForceMode.Impulse);
        StopAllCoroutines();
        StartCoroutine(HammerStunDuration(obstacleStunCooldown, agent));
    }


    private void ApplyStoneKnockBack(AiAgent agent)
    {
        Debug.Log("Applying stone knockback");
        knockbackDirection = (enemyTransform.position - playerTransform.position).normalized;
        rb.AddForce(knockbackDirection * stoneKnockbackX, ForceMode.Impulse);
        rb.AddForce(Vector3.up * stoneKnockbackY, ForceMode.Impulse);
        StopAllCoroutines();
        StartCoroutine(HammerStunDuration(stoneStunCooldown, agent));
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

