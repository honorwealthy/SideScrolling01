using UnityEngine;
using System.Collections;

public class ActorStateController
{
    public Actor Owner { get; private set; }

    private StateMachine<string, ActorState> _stateMachine;

    public string CurrentStateName { get { return _stateMachine.CurrentState.StateName; } }

    public ActorStateController(Actor owner)
    {
        Owner = owner;
        _stateMachine = new StateMachine<string, ActorState>();
    }

    public void InitState(string statename)
    {
        _stateMachine.GotoState(statename);
    }

    public void GotoState(string statename)
    {
        Owner.StartCoroutine(GotoStateNextFrame(statename));
    }

    private IEnumerator GotoStateNextFrame(string statename)
    {
        yield return new WaitForEndOfFrame();
        _stateMachine.GotoState(statename);
    }

    public void AddState(ActorState state)
    {
        _stateMachine.AddState(state.StateName, state);
    }

    public void FixedUpdate()
    {
        _stateMachine.CurrentState.FixedUpdate();
    }

    public void Update()
    {
        _stateMachine.CurrentState.Update();
    }

    public void LateUpdate()
    {
        _stateMachine.CurrentState.LateUpdate();
    }
}