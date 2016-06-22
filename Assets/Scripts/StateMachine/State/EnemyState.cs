using UnityEngine;
using System.Collections;
using System;

public class EnemyStateBase : ActorState
{
    public override string StateName { get { return this.GetType().Name; } }

    protected ActorStateController _stateMachine;
    protected Enemy _enemy;
    protected IAvatar _avatar;

    public EnemyStateBase(ActorStateController stateMachine)
    {
        _stateMachine = stateMachine;
        _enemy = _stateMachine.Owner as Enemy;
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

public class SpikyRollingState : EnemyStateBase
{
    public SpikyRollingState(ActorStateController stateMachine) : base(stateMachine) { }
}

public class SpikySlidingState : EnemyStateBase
{
    public SpikySlidingState(ActorStateController stateMachine) : base(stateMachine) { }
}

public class SpikyLaydownState : EnemyStateBase
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

public class SpikyRiseupState : EnemyStateBase
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