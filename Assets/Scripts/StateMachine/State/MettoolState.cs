using UnityEngine;
using System.Collections;
using System;

public class MettoolStateBase : ActorState
{
    public MettoolStateBase(ActorStateController stateMachine) : base(stateMachine) { }

    public override void FixedUpdate()
    {
        var direction = ((Mettool)_entity).Direction;
        var speed = _entity.Speed;
        _avatar.rb2d.velocity = new Vector2(direction * speed, _avatar.rb2d.velocity.y);

        if (direction != 0)
            _avatar.SetDirection(direction > 0);
    }
}

public class MettoolMoveState : MettoolStateBase
{
    public MettoolMoveState(ActorStateController stateMachine) : base(stateMachine) { }
}

public class MettoolHideState : MettoolStateBase
{
    public MettoolHideState(ActorStateController stateMachine) : base(stateMachine) { }

    public override void OnEnterState(IState prevState)
    {
        _avatar.anim.SetTrigger("Hide");
    }

    public override void OnLeaveState()
    {
        _avatar.anim.SetTrigger("Move");
    }

    public override void FixedUpdate()
    {
        //cant move
    }
}

public class MettoolShootState : MettoolStateBase
{
    public MettoolShootState(ActorStateController stateMachine) : base(stateMachine) { }

    public override void OnEnterState(IState prevState)
    {
        _avatar.anim.SetTrigger("Shoot");
    }

    public override void OnAnimationEvent(string eventName)
    {
        if (eventName == "MettoolShootBuster")
        {
            var mettool = _entity as Mettool;
            var buster = GameObject.Instantiate(mettool.Buster, mettool.transform.Find("Spurt").position, Quaternion.identity) as GameObject;
            buster.GetComponent<MettoolBuster>().BeginFly(mettool.Direction);
        }
        if (eventName == "MettoolShootOver")
            _stateMachine.GotoState("MettoolMoveState");
    }

    public override void FixedUpdate()
    {
        //cant move
    }
}