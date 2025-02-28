using System;
using UnityEngine;
public enum AiStateId
{
    PatrolState,
    FixedPatrol,
    RunToFixedWaypointState,
    ChasePlayer
}


public class AiState : MonoBehaviour
{

    public AiStateId id;

    public virtual void Enter(AiAgent agent)
    {

    }
    public virtual void Tick(AiAgent agent)
    {

    }
    public virtual void Exit(AiAgent agent)
    {

    }
}