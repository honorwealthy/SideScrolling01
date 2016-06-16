using UnityEngine;
using System.Collections;

public interface IState<TName>
{
    TName StateName { get; }

    void OnEnterState(IState<TName> prevState);
    void OnLeaveState();
}

//public interface OldIState
//{
//    string StateName { get; }

//    void OnEnterState(OldIState prevState);
//    void OnLeaveState();
//}

//public class OldStateBase : OldIState
//{
//    public string StateName { get; protected set; }
//    protected GameObject _owner;

//    public virtual void OnEnterState(OldIState prevState) { }

//    public virtual void OnLeaveState() { }

//    public void SetOwner(GameObject owner)
//    {
//        _owner = owner;
//    }

//    public virtual void InitState() { }
//}