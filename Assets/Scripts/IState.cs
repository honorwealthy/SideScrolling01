using UnityEngine;
using System.Collections;

public interface IState
{
    string StateName { get; }

    void OnEnterState(IState prevState);
    void OnLeaveState();
}

public class StateBase : IState
{
    public string StateName { get; protected set; }
    protected GameObject _owner;

    public virtual void OnEnterState(IState prevState) { }

    public virtual void OnLeaveState() { }

    public void SetOwner(GameObject owner)
    {
        _owner = owner;
    }

    public virtual void InitState() { }
}