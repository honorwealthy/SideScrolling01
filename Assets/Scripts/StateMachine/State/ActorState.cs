using UnityEngine;
using System.Collections;
using System;

public abstract class ActorState : IState
{
    public string StateName { get { return this.GetType().Name; } }

    protected Actor _entity;
    protected ActorStateController _stateMachine;
    protected Avatar _avatar;
    protected Behaviour _behaviour;

    public virtual void InitState(Actor entity)
    {
        _entity = entity;
        _stateMachine = entity.gameObject.GetComponent<ActorStateController>();
        _avatar = entity.gameObject.GetComponent<Avatar>();
        _avatar.OnAnimationEvent += OnAnimationEvent;
        _behaviour = entity.gameObject.GetComponent<Behaviour>();
    }

    public virtual void OnEnterState(IState prevState) { }
    public virtual void OnLeaveState() { }

    public virtual void FixedUpdate() { }
    public virtual void Update() { }
    public virtual void LateUpdate() { }

    public virtual void OnAnimationEvent(string eventName) { }
}
