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
    }

    private void AddState(PlayerStateBase state)
    {
        _stateMachine.AddState(state.StateName, state);
    }

    private void Start()
    {
        _stateMachine.GotoState("GroundState");
    }

    private void Update()
    {
        _stateMachine.CurrentState.CheckState();
    }
}

//public class OldStateMachine<TState> where TState : OldIState
//{
//    public TState CurrentState { get; private set; }
//    public string CurrentStateName { get; private set; }

//    private Dictionary<string, TState> _stateMap;

//    public OldStateMachine()
//    {
//        _stateMap = new Dictionary<string, TState>();
//    }

//    public void AddState(string key, TState state)
//    {
//        _stateMap.Add(key, state);
//    }

//    public void GotoState(string statename)
//    {
//        if (_stateMap.ContainsKey(statename))
//        {
//            var prevState = CurrentState;
//            CurrentState = _stateMap[statename];
//            CurrentStateName = statename;

//            if (prevState != null)
//                prevState.OnLeaveState();

//            CurrentState.OnEnterState(prevState);
//        }
//    }
//}

//public class Old2PlayerStateMachine : MonoBehaviour
//{
//    public Player Owner { get; private set; }

//    private OldStateMachine<Old2PlayerStateBase> StateMachine;
//    private Transform groundCheck;

//    public void AddState(string key, Old2PlayerStateBase state)
//    {
//        StateMachine.AddState(key, state);
//    }

//    public void GotoState(string statename)
//    {
//        StateMachine.GotoState(statename);
//    }

//    private void Awake()
//    {
//        Owner = GetComponent<Player>();
//        StateMachine = new OldStateMachine<Old2PlayerStateBase>();
//        groundCheck = Owner.GroundCheck;
//    }

//    private void Update()
//    {
//        if (StateMachine.CurrentStateName != "GroundState")
//        {
//            var grounded = Physics2D.Linecast(groundCheck.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
//            if (grounded == true)
//            {
//                StateMachine.GotoState("GroundState");
//            }
//        }
//    }
//}

//public class OldStateMachine
//{
//    private GameObject _owner;
//    private OldIState currentState;
//    private Dictionary<string, OldStateBase> stateMap;

//    public OldStateMachine(GameObject owner)
//    {
//        _owner = owner;
//        stateMap = new Dictionary<string, OldStateBase>();
//    }

//    public void AddState(string key, OldStateBase state)
//    {
//        stateMap.Add(key, state);
//        state.SetOwner(_owner);
//        state.InitState();
//    }

//    public void GotoState(string statename)
//    {
//        if (stateMap.ContainsKey(statename))
//        {
//            var prevState = currentState;
//            currentState = stateMap[statename];

//            if (prevState != null)
//                prevState.OnLeaveState();

//            currentState.OnEnterState(prevState);
//        }
//    }
//}