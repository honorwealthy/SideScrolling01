using UnityEngine;
using System.Collections;

namespace SeafoodStudio
{
    public interface IState<TName>
    {
        TName StateName { get; }

        void OnEnterState(IState<TName> prevState);
        void OnLeaveState();
    }


    public interface IState
    {
        string StateName { get; }

        void OnEnterState(IState prevState);
        void OnLeaveState();
    }
}