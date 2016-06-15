using UnityEngine;
using System.Collections;

public class GroundState : StateBase
{
    private GroundController groundController;

    public GroundState()
    {
        StateName = "GroundState";
    }

    public override void InitState()
    {
        groundController = _owner.AddComponent<GroundController>();
        groundController.enabled = false;
    }

    public override void OnEnterState(IState prevState)
    {
        groundController.enabled = true;
    }

    public override void OnLeaveState()
    {
        groundController.enabled = false;
    }
}