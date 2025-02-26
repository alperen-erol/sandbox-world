using UnityEngine;

public class DrawGizmos : MonoBehaviour
{
    private AiAgent agent;
    Transform enemyTransform;
    public float playerCheckRadius, playerAttackRadius;

    void Start()
    {
        agent = GetComponent<AiAgent>();
        playerCheckRadius = agent.config.playerCheckRadius;
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        enemyTransform = GetComponent<Transform>();
        Debug.Log("drawing gizmos");
        Gizmos.DrawWireSphere(enemyTransform.position, playerCheckRadius);
    }
}
