using System.Collections.Generic;
using UnityEngine;

public class AiStateMachine
{
    public List<AiState> states = new List<AiState>();
    public AiAgent agent;
    public AiStateId currentState;


    public AiStateMachine(AiAgent agent)
    {
        this.agent = agent;
    }


    public AiState GetState(AiStateId stateId)
    {
        return states.Find(x => x.id == stateId);
    }


    public void Update()
    {
        GetState(currentState)?.Tick(agent);
    }


    public void ChangeState(AiStateId newState)
    {
        GetState(currentState)?.Exit(agent);
        currentState = newState;
        GetState(currentState)?.Enter(agent);
    }
}
