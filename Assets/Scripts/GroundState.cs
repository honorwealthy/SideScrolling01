using UnityEngine;
using System.Collections;
using System;

public abstract class PlayerStateBase : MonoBehaviour, IState<string>
{
    public abstract string StateName { get; }

    protected PlayerStateMachine _stateMachine;
    protected Player _player;
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
        _player = _stateMachine.Owner;
        _avatar = _player.Avatar;
        _groundCheck = _player.GroundCheck;
        enabled = false;
    }

    protected virtual void FixedUpdate()
    {
        var direction = Input.GetAxisRaw("Horizontal");
        var x = Mathf.Abs(Input.GetAxis("Horizontal"));
        var speed = _player.Speed;
        _avatar.rb2d.velocity = new Vector2(direction * x * speed, _avatar.rb2d.velocity.y);

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
        if (_avatar.rb2d.velocity.y < 0)
        {
            _stateMachine.GotoState("AirState");
            return;
        }

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

//public class DashState : PlayerStateBase
//{
//    public override string StateName { get { return "DashState"; } }

//    [SerializeField]
//    private float DashVelocity = 10f;

//    private float _prePressValue = 0.0f;

//    public override void OnEnterState(IState<string> prevState)
//    {
//        base.OnEnterState(prevState);
//        _avatar.anim.SetTrigger("Dash");
//        _avatar.rb2d.velocity = new Vector2(DashVelocity, _avatar.rb2d.velocity.y);
//    }

//    public override void OnLeaveState()
//    {
//        _prePressValue = 0.0f;
//        base.OnLeaveState();
//    }

//    protected override void FixedUpdate()
//    {
//        var direction = Input.GetAxisRaw("Horizontal");

//        if (direction != 0)
//            _avatar.SetDirection(direction > 0);
//    }

//    protected override void Update()
//    {
//        float pressValue = Input.GetAxis("Dash");
//        float deltaPressValue = (pressValue - _prePressValue);
//        _prePressValue = pressValue;
//        if (deltaPressValue < 0.0f)
//        {
//            _avatar.rb2d.velocity = new Vector2(_avatar.rb2d.velocity.x, 0);
//            _stateMachine.GotoState("GroundState");
//            return;
//        }
//    }
//}

public class JumpState : PlayerStateBase
{
    public override string StateName { get { return "JumpState"; } }

    [SerializeField]
    private float JumpVelocity = 18.0f;

    private float _prePressValue = 0.0f;

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
        float pressValue = Input.GetAxis("Jump");
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
        Collider2D cldr = gameObject.GetComponent<Collider2D>();
        var groundCheckLeft = new Vector3(cldr.bounds.min.x + 1f, cldr.bounds.min.y - 1f, 0);
        var groundCheckRight = new Vector3(cldr.bounds.max.x - 1f, cldr.bounds.min.y - 1f, 0);

        var grounded = Physics2D.Linecast(groundCheckLeft, groundCheckRight, 1 << LayerMask.NameToLayer("Ground"));
        _avatar.anim.SetBool("IsGrounded", grounded);
        if (grounded == true)
        {
            _stateMachine.GotoState("GroundState");
        }
    }
}