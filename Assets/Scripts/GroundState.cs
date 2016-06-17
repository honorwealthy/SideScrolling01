using UnityEngine;
using System.Collections;
using System;

public abstract class PlayerStateBase : MonoBehaviour, IState<string>
{
    public abstract string StateName { get; }
    public float Speed = 5.0f;

    protected PlayerStateMachine _stateMachine;
    protected IAvatar _avatar;
    protected Transform _groundCheck;

    public virtual void OnEnterState(IState<string> prevState)
    {
        enabled = true;
    }

    public virtual void OnLeaveState()
    {
        enabled = false;
    }

    protected virtual void Awake()
    {
        _stateMachine = GetComponent<PlayerStateMachine>();
        _avatar = _stateMachine.Owner.Avatar;
        _groundCheck = _stateMachine.Owner.GroundCheck;
        enabled = false;
    }

    protected virtual void FixedUpdate()
    {
        var direction = Input.GetAxisRaw("Horizontal");
        var x = Mathf.Abs(Input.GetAxis("Horizontal"));
        _avatar.rb2d.velocity = new Vector2(direction * x * Speed, _avatar.rb2d.velocity.y);

        if (direction != 0)
            _avatar.SetDirection(direction > 0);
    }

    protected virtual void Update() { }

    protected virtual void LateUpdate() { }
}

public class GroundState : PlayerStateBase
{
    public override string StateName { get { return "GroundState"; } }

    public override void OnLeaveState()
    {
        base.OnLeaveState();
        _avatar.anim.SetFloat("GroundSpeed", 0);
    }

    protected override void Update()
    {
        if (Input.GetButtonDown("Jump"))
            _stateMachine.GotoState("JumpState");
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        var x = Mathf.Abs(Input.GetAxis("Horizontal"));
        _avatar.anim.SetFloat("GroundSpeed", x);
    }
}

public class JumpState : PlayerStateBase
{
    public override string StateName { get { return "JumpState"; } }

    [SerializeField]
    private float JumpVelocity = 18.0f;

    private float _prePressValue = 0.0f;

    [SerializeField]
    private float pressValue = 0.0f;

    public override void OnEnterState(IState<string> prevState)
    {
        base.OnEnterState(prevState);
        _avatar.anim.SetTrigger("Jump");
        _avatar.rb2d.velocity = new Vector2(_avatar.rb2d.velocity.x, JumpVelocity);
    }

    public override void OnLeaveState()
    {
        _prePressValue = 0.0f;
        base.OnLeaveState();
    }

    protected override void Update()
    {
        pressValue = Input.GetAxis("Jump");
        float deltaPressValue = (pressValue - _prePressValue);
        _prePressValue = pressValue;
        if (deltaPressValue < 0.0f)
        {
            _avatar.rb2d.velocity = new Vector2(_avatar.rb2d.velocity.x, 0);
            _stateMachine.GotoState("AirState");
            return;
        }

        CheckJumpEnd();
    }

    private void CheckJumpEnd()
    {
        if (_avatar.rb2d.velocity.y < 0)
            _stateMachine.GotoState("AirState");
    }
}

public class AirState : PlayerStateBase
{
    public override string StateName { get { return "AirState"; } }

    public override void OnEnterState(IState<string> prevState)
    {
        base.OnEnterState(prevState);
        _avatar.anim.SetTrigger("FallBegin");
    }

    protected override void Update()
    {
        var grounded = Physics2D.Linecast(_groundCheck.position, _groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        if (grounded == true)
        {
            _stateMachine.GotoState("GroundState");
            _avatar.anim.SetTrigger("Landing");
        }
    }
}