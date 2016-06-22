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
        //_avatar.anim.SetTrigger("Laydown");
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
        //_avatar.anim.SetTrigger("Laydown");
    }

    public override void FixedUpdate()
    {
        //cant move
    }
}