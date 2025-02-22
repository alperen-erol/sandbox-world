using UnityEngine;

public class Chase : AIState
{
    public void Enter()
    {
        Debug.Log("Entering Chase State");
        // Initialize chase behavior (e.g., target player)
    }

    public void Update()
    {
        Debug.Log("Chasing...");
        // Implement chase logic (e.g., move towards target)
    }

    public void Exit()
    {
        Debug.Log("Exiting Chase State");
        // Clean up chase behavior (e.g., stop chasing)
    }
}