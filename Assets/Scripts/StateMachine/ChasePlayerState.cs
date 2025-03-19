using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.AI;

public class ChasePlayerState : AiState
{

    NavMeshAgent enemyAgent;
    Transform player;
    Transform enemyTransform;
    float playerCheckRadius;
    LayerMask whatIsPlayer;
    Rigidbody rb;

    [SerializeField] float chaseSpeed, attackSpeed;


    public override void Enter(AiAgent agent)
    {
        Debug.Log("Chase Player State Entered");
        enemyAgent = agent.NavMeshAgent;
        enemyAgent.enabled = true;
        player = agent.player;
        enemyTransform = agent.enemyTransform;
        playerCheckRadius = agent.config.playerCheckRadius;
        whatIsPlayer = agent.whatIsPlayer;
        enemyAgent.speed = 16;
        rb = GetComponent<Rigidbody>();
        if (this.gameObject.tag == "TITAN")
            rb.isKinematic = false;
        else
            rb.isKinematic = true;
    }

    public override void Tick(AiAgent agent)
    {
        if (agent.player != null)
        {
            enemyAgent.SetDestination(agent.player.position);
            if (Vector3.Distance(transform.position, agent.player.transform.position) < 2f)
            {
                enemyAgent.SetDestination(transform.position);
            }


            if (CheckPlayerDistance())
            {
                enemyAgent.speed = attackSpeed;
            }
            else
                enemyAgent.speed = chaseSpeed;
        }
        else
            enemyAgent.SetDestination(enemyTransform.position);
    }

    public override void Exit(AiAgent agent)
    {
        Debug.Log("Chase Player State Exited");

    }

    bool CheckPlayerDistance()
    {
        return Physics.CheckSphere(enemyTransform.position, 1.8f, whatIsPlayer);
    }
}