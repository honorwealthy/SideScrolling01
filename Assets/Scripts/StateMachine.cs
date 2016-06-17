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

    public string CurrentStateName { get { return _stateMachine.CurrentState.StateName; } }

    public void GotoState(string statename)
    {
        StartCoroutine(GotoStateNextFrame(statename));
    }

    private IEnumerator GotoStateNextFrame(string statename)
    {
        yield return new WaitForEndOfFrame();
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
        AddState(new GroundState(this));
        AddState(new JumpState(this));
        AddState(new AirState(this));
    }

    private void AddState(PlayerStateBase state)
    {
        _stateMachine.AddState(state.StateName, state);
    }

    private void Start()
    {
        _stateMachine.GotoState("GroundState");
    }

    private void FixedUpdate()
    {
        _stateMachine.CurrentState.FixedUpdate();
    }

    private void Update()
    {
        _stateMachine.CurrentState.Update();
    }

    private void LateUpdate()
    {
        _stateMachine.CurrentState.LateUpdate();
    }
}