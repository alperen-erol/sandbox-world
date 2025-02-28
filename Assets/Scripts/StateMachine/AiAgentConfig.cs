using UnityEngine;

[CreateAssetMenu()]
public class AiAgentConfig : ScriptableObject
{
    public float RandomWalkPointX, RandomWalkPointZ;
    public float playerCheckRadius = 8f;
    public float playerAttackRadius = 2f;

    public float agentPatrolSpeed = 2f;
    public float agentChaseSpeed = 4f;
}
