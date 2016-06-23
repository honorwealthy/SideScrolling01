using UnityEngine;
using System.Collections;
using System;

public abstract class ActorState : IState
{
    public string StateName { get { return this.GetType().Name; } }

    protected ActorStateController _stateMachine;
    protected Actor _entity;
    protected IAvatar _avatar;

    public ActorState(ActorStateController stateMachine)
    {
        _stateMachine = stateMachine;
        _entity = _stateMachine.Owner;
        _avatar = _entity.Avatar;
        _avatar.OnAnimationEvent += OnAnimationEvent;
    }

    public virtual void OnEnterState(IState prevState) { }
    public virtual void OnLeaveState() { }

    public virtual void FixedUpdate() { }
    public virtual void Update() { }
    public virtual void LateUpdate() { }

    public virtual void OnAnimationEvent(string eventName) { }
}
