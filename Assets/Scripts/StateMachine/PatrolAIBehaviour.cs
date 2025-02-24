using UnityEngine;

public class PatrolAIBehaviour : MonoBehaviour
{
    private AIState currentState;
    private AIStateType currentStateType;

    private Patrol patrolState;
    private Chase chaseState;

    private void Start()
    {
        // Initialize states
        patrolState = new Patrol();
        chaseState = new Chase();

        // Set initial state
        ChangeState(AIStateType.Patrol);
    }

    private void Update()
    {
        // Update the current state
        if (currentState != null)
        {
            currentState.Update();
        }

        // Example: Switch state based on some condition
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentStateType == AIStateType.Patrol)
            {
                ChangeState(AIStateType.Chase);
            }
            else
            {
                ChangeState(AIStateType.Patrol);
            }
        }
    }

    public void ChangeState(AIStateType newState)
    {
        // Exit the current state
        if (currentState != null)
        {
            currentState.Exit();
        }

        // Set the new state
        switch (newState)
        {
            case AIStateType.Patrol:
                currentState = patrolState;
                break;
            case AIStateType.Chase:
                currentState = chaseState;
                break;
        }

        currentStateType = newState;

        // Enter the new state
        currentState.Enter();
    }
}