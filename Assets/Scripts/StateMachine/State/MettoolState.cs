using UnityEngine;
using System.Collections;
using System;

public class MettoolStateBase : ActorState
{
    public override string StateName { get { return this.GetType().Name; } }

    protected ActorStateController _stateMachine;
    protected Mettool _enemy;
    protected IAvatar _avatar;

    public MettoolStateBase(ActorStateController stateMachine)
    {
        _stateMachine = stateMachine;
        _enemy = _stateMachine.Owner as Mettool;
        _avatar = _enemy.Avatar;
    }

    public override void FixedUpdate()
    {
        var direction = _enemy.Direction;
        var speed = _enemy.Speed;
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
}

public class MettoolShootState : MettoolStateBase
{
    public MettoolShootState(ActorStateController stateMachine) : base(stateMachine) { }
}