using UnityEngine;
using System.Collections;
using System;

public class SpikyStateBase : ActorState
{
    public override string StateName { get { return this.GetType().Name; } }

    protected ActorStateController _stateMachine;
    protected Spiky _enemy;
    protected IAvatar _avatar;

    public SpikyStateBase(ActorStateController stateMachine)
    {
        _stateMachine = stateMachine;
        _enemy = _stateMachine.Owner as Spiky;
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

public class SpikyRollingState : SpikyStateBase
{
    public SpikyRollingState(ActorStateController stateMachine) : base(stateMachine) { }
}

public class SpikySlidingState : SpikyStateBase
{
    public SpikySlidingState(ActorStateController stateMachine) : base(stateMachine) { }
}

public class SpikyLaydownState : SpikyStateBase
{
    public override void OnEnterState(IState<string> prevState)
    {
        _avatar.anim.SetTrigger("Laydown");
    }

    public SpikyLaydownState(ActorStateController stateMachine) : base(stateMachine)
    {
        _avatar.OnAnimationEvent += OnAnimationEvent;
    }

    public void OnAnimationEvent(string eventName)
    {
        if (eventName == "SpikyLaydownOver")
            _stateMachine.GotoState("SpikySlidingState");
    }

    public override void FixedUpdate()
    {
        //cant move
    }
}

public class SpikyRiseupState : SpikyStateBase
{
    public override void OnEnterState(IState<string> prevState)
    {
        _avatar.anim.SetTrigger("Riseup");
    }

    public SpikyRiseupState(ActorStateController stateMachine) : base(stateMachine)
    {
        _avatar.OnAnimationEvent += OnAnimationEvent;
    }

    public void OnAnimationEvent(string eventName)
    {
        if (eventName == "SpikyRiseupOver")
            _stateMachine.GotoState("SpikyRollingState");
    }

    public override void FixedUpdate()
    {
        //cant move
    }
}