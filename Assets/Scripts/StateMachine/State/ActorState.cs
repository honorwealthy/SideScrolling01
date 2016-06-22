using UnityEngine;
using System.Collections;
using System;

public abstract class ActorState : IState<string>
{
    public abstract string StateName { get; }

    public virtual void OnEnterState(IState<string> prevState) { }
    public virtual void OnLeaveState() { }

    public virtual void FixedUpdate() { }
    public virtual void Update() { }
    public virtual void LateUpdate() { }
}
