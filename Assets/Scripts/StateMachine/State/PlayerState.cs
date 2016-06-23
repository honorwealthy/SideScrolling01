using UnityEngine;
using System.Collections;
using System;

public class PlayerStateBase : IState<string>
{
    public string StateName { get { return this.GetType().Name; } }

    protected PlayerStateMachine _stateMachine;
    protected Player _player;
    protected IAvatar _avatar;
    protected Transform _groundCheckLeft;
    protected Transform _groundCheckRight;

    public virtual void OnEnterState(IState<string> prevState) { }

    public virtual void OnLeaveState() { }

    public PlayerStateBase(PlayerStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _player = _stateMachine.Owner;
        _avatar = _player.Avatar;
        _groundCheckLeft = _player.GroundCheckLeft;
        _groundCheckRight = _player.GroundCheckRight;
    }

    public virtual void FixedUpdate()
    {
        var direction = Input.GetAxisRaw("Horizontal");
        var x = Mathf.Abs(Input.GetAxis("Horizontal"));
        var speed = _player.Speed;
        _avatar.rb2d.velocity = new Vector2(direction * x * speed, _avatar.rb2d.velocity.y);

        if (direction != 0)
            _avatar.SetDirection(direction > 0);
    }

    public virtual void Update() { }

    public virtual void LateUpdate() { }

    protected RaycastHit2D CheckGrounded()
    {
        var grounded = Physics2D.Linecast(_groundCheckLeft.position, _groundCheckRight.position, 1 << LayerMask.NameToLayer("Ground"));
        _avatar.anim.SetBool("IsGrounded", grounded);
        return grounded;
    }
}

public class GroundState : PlayerStateBase
{
    public GroundState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void OnLeaveState()
    {
        _avatar.anim.SetFloat("GroundSpeed", 0);
    }

    public override void Update()
    {
        var grounded = CheckGrounded();
        if (grounded == false)
        {
            _stateMachine.GotoState("AirState");
        }
        else
        {
            if (Input.GetButtonDown("Jump"))
            {
                _stateMachine.GotoState("JumpState");
            }
            else if (Input.GetButtonDown("Attack"))
            {
                _stateMachine.GotoState("GroundAttackState");
            }
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        var x = Mathf.Abs(Input.GetAxis("Horizontal"));
        _avatar.anim.SetFloat("GroundSpeed", x);
    }
}

public class GroundAttackState : PlayerStateBase
{
    public GroundAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        _avatar.OnAnimationEvent += OnAnimationEvent;
    }

    public void OnAnimationEvent(string eventName)
    {
        if (eventName == "ZeroMeleeAttackEndOver")
            _stateMachine.GotoState("GroundState");
    }

    public override void OnEnterState(IState<string> prevState)
    {
        _avatar.anim.SetTrigger("Attack");
    }

    public override void Update()
    {
        var grounded = CheckGrounded();
        if (grounded == false)
        {
            _stateMachine.GotoState("AirState");
        }
        else
        {
            if (Input.GetButtonDown("Jump"))
            {
                _stateMachine.GotoState("JumpState");
            }
        }
    }

    public override void FixedUpdate()
    {
        //cant move
    }
}

public class JumpState : PlayerStateBase
{
    private float _prePressValue = 0.0f;

    public JumpState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void OnEnterState(IState<string> prevState)
    {
        _avatar.anim.SetTrigger("Jump");
        _avatar.anim.SetBool("IsGrounded", false);
        _avatar.rb2d.velocity = new Vector2(_avatar.rb2d.velocity.x, _player.JumpVelocity);
    }

    public override void OnLeaveState()
    {
        _prePressValue = 0.0f;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        float pressValue = Input.GetAxis("Jump");
        float deltaPressValue = (pressValue - _prePressValue);
        _prePressValue = pressValue;
        if (deltaPressValue < 0.0f)
        {
            _avatar.rb2d.velocity = new Vector2(_avatar.rb2d.velocity.x, 0);
            _stateMachine.GotoState("AirState");
            return;
        }

        if (_avatar.rb2d.velocity.y < 0)
        {
            _stateMachine.GotoState("AirState");
        }
    }
}

public class AirState : PlayerStateBase
{
    public AirState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void OnEnterState(IState<string> prevState)
    {
        _avatar.anim.SetTrigger("FallBegin");
    }

    public override void Update()
    {
        var grounded = CheckGrounded();
        if (grounded == true)
        {
            if (Input.GetButtonDown("Attack"))
                _stateMachine.GotoState("GroundAttackState");
            else
                _stateMachine.GotoState("GroundState");
        }
    }
}

public class HurtState : PlayerStateBase
{
    public HurtState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        _avatar.OnAnimationEvent += OnAnimationEvent;
    }

    public void OnAnimationEvent(string eventName)
    {
        if (eventName == "ZeroHurtOver")
        {
            var grounded = CheckGrounded();
            if (grounded == false)
            {
                _stateMachine.GotoState("AirState");
            }
            else
            {
                _stateMachine.GotoState("GroundState");
            }
        }
    }

    public override void OnEnterState(IState<string> prevState)
    {
        //_avatar.anim.SetTrigger("FallBegin");
        _player.StartCoroutine(Recover());
        _avatar.SetDirection(_player.HurtFromRight);
        var direction = _player.HurtFromRight ? -1 : 1;
        _avatar.rb2d.velocity = new Vector2(direction * 3, 7);
    }

    private IEnumerator Recover()
    {
        yield return new WaitForSeconds(0.5f);
        OnAnimationEvent("ZeroHurtOver");
    }

    public override void FixedUpdate()
    {
        // cant move
    }
}