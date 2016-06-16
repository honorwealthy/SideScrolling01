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

public class PlayerStateMachine : MonoBehaviour
{
    public Player Owner { get; private set; }

    private StateMachine<string, PlayerStateBase> _stateMachine;

    public void GotoState(string statename)
    {
        _stateMachine.GotoState(statename);
    }

    private void Awake()
    {
        Owner = GetComponent<Player>();
        InitStateMachine();
    }

    private void InitStateMachine()
    {
        _stateMachine = new StateMachine<string, PlayerStateBase>();
        AddState(gameObject.AddComponent<GroundState>());
        AddState(gameObject.AddComponent<JumpState>());
        AddState(gameObject.AddComponent<AirState>());
    }

    private void AddState(PlayerStateBase state)
    {
        _stateMachine.AddState(state.StateName, state);
    }

    private void Start()
    {
        _stateMachine.GotoState("GroundState");
    }
}