using UnityEngine;

public class Patrol : AIState
{
    public void Enter()
    {
        Debug.Log("Entering Patrol State");
        // Initialize patrol behavior (e.g., set waypoints, start moving)
    }

    public void Update()
    {
        Debug.Log("Patrolling...");
        // Implement patrol logic (e.g., move between waypoints)
    }

    public void Exit()
    {
        Debug.Log("Exiting Patrol State");
        // Clean up patrol behavior (e.g., stop moving)
    }
}