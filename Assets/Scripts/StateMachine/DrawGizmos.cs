using UnityEngine;

public class DrawGizmos : MonoBehaviour
{
    private AiAgent agent;
    public float playerCheckRadius, playerAttackRadius;
    [SerializeField] Transform enemyTransform;

    void Start()
    {
        agent = GetComponent<AiAgent>();
        playerCheckRadius = agent.config.playerCheckRadius;
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(enemyTransform.position, playerCheckRadius);
    }
}
