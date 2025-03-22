using System;
using UnityEngine;
public enum AiStateId
{
    PatrolState,
    FixedPatrol,
    RunToFixedWaypointState,
    RunAwayPatrol,
    RunAwayFromPlayerState,
    StunnedState,
    AirborneState,
    ChasePlayer
}
public enum StunType
{
    HammerKnockback,
    ScepterKnockback,
    StoneKnockBack,
    SoulKnockback,
    ObstacleDestruction
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