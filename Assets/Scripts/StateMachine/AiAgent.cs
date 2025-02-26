using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{
    public AiStateMachine stateMachine;
    public AiStateId initialState;
    public NavMeshAgent NavMeshAgent;
    public Transform player;
    public Transform enemyTransform;
    public LayerMask whatIsGround, whatIsPlayer;
    public AiAgentConfig config;
    public GameObject walkpointDebug;
    public bool isWaiting = false;

    void Start()
    {
        stateMachine = new AiStateMachine(this);
        stateMachine.RegisterState(new PatrolState());
        stateMachine.RegisterState(new ChasePlayerState());
        stateMachine.ChangeState(initialState);

        NavMeshAgent = GetComponent<NavMeshAgent>();
        enemyTransform = GetComponent<Transform>();
    }

    void Update()
    {
        stateMachine.Update();
    }
}
