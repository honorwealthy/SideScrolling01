using UnityEngine;
using System.Collections;

public interface IState<TName>
{
    TName StateName { get; }

    void OnEnterState(IState<TName> prevState);
    void OnLeaveState();
}