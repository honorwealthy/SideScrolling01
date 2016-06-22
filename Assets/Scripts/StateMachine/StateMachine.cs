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
        else
        {
            Debug.Log("Goto wrong state name : " + statename);
        }
    }
}

public class StateMachine<TState> where TState : IState
{
    public TState CurrentState { get; private set; }

    private Dictionary<string, TState> _stateMap;

    public StateMachine()
    {
        _stateMap = new Dictionary<string, TState>();
    }

    public void AddState(string key, TState state)
    {
        _stateMap.Add(key, state);
    }

    public void GotoState(string statename)
    {
        if (_stateMap.ContainsKey(statename))
        {
            var prevState = CurrentState;
            CurrentState = _stateMap[statename];

            if (prevState != null)
                prevState.OnLeaveState();

            CurrentState.OnEnterState(prevState);
        }
        else
        {
            Debug.Log("Goto wrong state name : " + statename);
        }
    }
}