using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateMachine<TKey, TState> where TState : IState<TKey>
{
    public TState CurrentState { get; private set; }

    private Dictionary<TKey, TState> _stateMap;

    public StateMachine()
    {
        _stateMap = new Dictionary<TKey, TState>();
    }

    public void AddState(TKey key, TState state)
    {
        _stateMap.Add(key, state);
    }

    public void GotoState(TKey statename)
    {
        if (_stateMap.ContainsKey(statename))
        {
            var prevState = CurrentState;
            CurrentState = _stateMap[statename];

            if (prevState != null)
                prevState.OnLeaveState();

            CurrentState.OnEnterState(prevState);
        }
    }
}