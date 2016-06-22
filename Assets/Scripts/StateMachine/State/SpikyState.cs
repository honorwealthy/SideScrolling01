using UnityEngine;
using System.Collections;
using System;

public class SpikyStateBase : ActorState
{
    public SpikyStateBase(ActorStateController stateMachine) : base(stateMachine) { }

    public override void FixedUpdate()
    {
        var direction = ((Spiky)_entity).Direction;
        var speed = _entity.Speed;
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
    public override void OnEnterState(IState prevState)
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
    public override void OnEnterState(IState prevState)
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