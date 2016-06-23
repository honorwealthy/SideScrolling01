using UnityEngine;
using System.Collections;
using System;

public class SpikyStateBase : ActorState
{
    public override void FixedUpdate()
    {
        var direction = _behaviour.Direction;
        var speed = _entity.Speed;
        _avatar.rb2d.velocity = new Vector2(direction * speed, _avatar.rb2d.velocity.y);

        if (direction != 0)
            _avatar.SetDirection(direction > 0);
    }
}

public class SpikyRollingState : SpikyStateBase
{
}

public class SpikySlidingState : SpikyStateBase
{
}

public class SpikyLaydownState : SpikyStateBase
{
    public override void OnEnterState(IState prevState)
    {
        _avatar.anim.SetTrigger("Laydown");
    }

    protected override void OnAnimationEvent(string eventName)
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

    protected override void OnAnimationEvent(string eventName)
    {
        if (eventName == "SpikyRiseupOver")
            _stateMachine.GotoState("SpikyRollingState");
    }

    public override void FixedUpdate()
    {
        //cant move
    }
}