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

    [Header("Scepter Knockback")]
    [SerializeField] private float scepterKnockbackX;
    [SerializeField] private float scepterKnockbackY;

    [Header("Stone Knockback")]
    [SerializeField] private float stoneKnockbackX;
    [SerializeField] private float stoneKnockbackY;

    [Header("Obstacle Destruction")]
    [SerializeField] private float obstacleDestructionX;
    [SerializeField] private float obstacleDestructionY;

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
        rb = GetComponent<Rigidbody>();
        enemyTransform = agent.enemyTransform;
        playerTransform = agent.player;
        rb.useGravity = true;
        ApplyForce();
    }


    public void ApplyForce()
    {
        switch (selectedForceType)
        {
            case StunType.HammerKnockback:
                ApplyHammerKnockBack();
                break;
            case StunType.ScepterKnockback:
                ApplyScepterKnockback();
                break;
            case StunType.StoneKnockBack:
                ApplyStoneKnockBack();
                break;
            case StunType.ObstacleDestruction:
                ApplyObstacleDestruction();
                break;
        }
    }

    private void ApplyHammerKnockBack()
    {
        Debug.Log("Applying Hammer Knockback");
        knockbackDirection = (enemyTransform.position - playerTransform.position).normalized;
        rb.AddForce(knockbackDirection * hammerKnockbackX, ForceMode.Impulse);
        rb.AddForce(Vector3.up * hammerKnockbackY, ForceMode.Impulse);
    }

    private void ApplyScepterKnockback()
    {
        Debug.Log("Applying Scepter Knockback");
        knockbackDirection = (enemyTransform.position - playerTransform.position).normalized;
        rb.AddForce(knockbackDirection * scepterKnockbackX, ForceMode.Impulse);
        rb.AddForce(Vector3.up * scepterKnockbackY, ForceMode.Impulse);
    }

    private void ApplyStoneKnockBack()
    {

    }

    private void ApplyObstacleDestruction()
    {

    }

    public override void Tick(AiAgent agent)
    {
    }

    public override void Exit(AiAgent agent)
    {
    }

    void HandleApplyForce()
    {
        knockbackDirection = (enemyTransform.position - playerTransform.position).normalized;
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
        rb.AddForce(knockbackDirectionY * Vector3.up, ForceMode.Impulse);
    }

    // IEnumerator ApplyForce(AiAgent agent)
    // {
    //     yield return new WaitForSeconds(0.12f);
    //     HandleApplyForce();
    //     yield return new WaitForSeconds(stunDuration);

    //     agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
    // }
}

