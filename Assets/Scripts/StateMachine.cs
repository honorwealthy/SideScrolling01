using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateMachine
{
    private GameObject _owner;
    private IState currentState;
    private Dictionary<string, StateBase> stateMap;

    public StateMachine(GameObject owner)
    {
        _owner = owner;
        stateMap = new Dictionary<string, StateBase>();
    }

    public void AddState(string key, StateBase state)
    {
        stateMap.Add(key, state);
        state.SetOwner(_owner);
        state.InitState();
    }

    public void GotoState(string statename)
    {
        if (stateMap.ContainsKey(statename))
        {
            var prevState = currentState;
            currentState = stateMap[statename];

            if (prevState != null)
                prevState.OnLeaveState();

            currentState.OnEnterState(prevState);
        }
    }
}