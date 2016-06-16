using UnityEngine;
using System.Collections;

public abstract class PlayerStateBase : MonoBehaviour, IState<string>
{
    public abstract string StateName { get; }

    protected PlayerStateMachine _stateMachine;
    protected IAvatar _avatar;
    protected Transform _groundCheck;

    public virtual void OnEnterState(IState<string> prevState) { }

    public virtual void OnLeaveState() { }

    protected virtual void Awake()
    {
        _stateMachine = GetComponent<PlayerStateMachine>();
        _avatar = _stateMachine.Owner.Avatar;
        _groundCheck = _stateMachine.Owner.GroundCheck;
    }

    public virtual void CheckState() { }
}

public class GroundState : PlayerStateBase
{
    public override string StateName { get { return "GroundState"; } }
    public float Speed = 5.0f;

    public override void OnEnterState(IState<string> prevState)
    {
        enabled = true;
    }

    public override void OnLeaveState()
    {
        _avatar.anim.SetFloat("GroundSpeed", 0);
        enabled = false;
    }

    protected override void Awake()
    {
        base.Awake();
        enabled = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
            _stateMachine.GotoState("JumpState");
    }

    private void FixedUpdate()
    {
        var direction = Input.GetAxisRaw("Horizontal");
        var x = Mathf.Abs(Input.GetAxis("Horizontal"));
        _avatar.anim.SetFloat("GroundSpeed", x);
        _avatar.rb2d.velocity = new Vector2(direction * x * Speed, _avatar.rb2d.velocity.y);

        if (direction != 0)
            _avatar.SetDirection(direction > 0);
    }
}

public class JumpState : PlayerStateBase
{
    public override string StateName { get { return "JumpState"; } }
    public float Speed = 5.0f;
    public float JumpForce = 1.0f;

    public override void OnEnterState(IState<string> prevState)
    {
        _avatar.anim.SetTrigger("Jump");
        enabled = true;
    }

    public override void OnLeaveState()
    {
        enabled = false;
    }

    protected override void Awake()
    {
        base.Awake();
        enabled = false;
    }

    private void FixedUpdate()
    {
        var direction = Input.GetAxisRaw("Horizontal");
        var x = Mathf.Abs(Input.GetAxis("Horizontal"));
        _avatar.rb2d.velocity = new Vector2(direction * x * Speed, _avatar.rb2d.velocity.y);

        if (direction != 0)
            _avatar.SetDirection(direction > 0);

        var y = Mathf.Abs(Input.GetAxis("Jump"));
        _avatar.rb2d.AddForce(new Vector2(0, JumpForce));
    }

    public override void CheckState()
    {
        var grounded = Physics2D.Linecast(_groundCheck.position, _groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        if (grounded == true)
        {
            _stateMachine.GotoState("GroundState");
        }
    }
}

//public class Old2PlayerStateBase : OldIState
//{
//    public string StateName { get; protected set; }

//    protected Old2PlayerStateMachine _stateMachine;
//    protected BasePlayerController _controller;

//    public virtual void OnEnterState(OldIState prevState) { }

//    public virtual void OnLeaveState() { }

//    public Old2PlayerStateBase(Old2PlayerStateMachine stateMachine)
//    {
//        _stateMachine = stateMachine;
//    }
//}

//public class Old2GroundState : Old2PlayerStateBase
//{
//    public Old2GroundState(Old2PlayerStateMachine stateMachine)
//        : base(stateMachine)
//    {
//        StateName = "GroundState";
//        var controller = stateMachine.Owner.gameObject.AddComponent<GroundController>();
//        controller.TheAvatar = stateMachine.Owner.Avatar;
//        controller.State = this;
//        controller.enabled = false;
//        _controller = controller;
//    }

//    public override void OnEnterState(OldIState prevState)
//    {
//        _controller.enabled = true;
//    }

//    public override void OnLeaveState()
//    {
//        _controller.TheAvatar.anim.SetFloat("GroundSpeed", 0);
//        _controller.enabled = false;
//    }

//    public void Jump()
//    {
//        _stateMachine.GotoState("JumpState");
//    }
//}

//public class Old2JumpState : Old2PlayerStateBase
//{
//    public Old2JumpState(Old2PlayerStateMachine stateMachine)
//        : base(stateMachine)
//    {
//        StateName = "JumpState";
//        _controller = stateMachine.Owner.gameObject.AddComponent<JumpController>();
//        _controller.TheAvatar = stateMachine.Owner.Avatar;
//        _controller.enabled = false;
//    }

//    public override void OnEnterState(OldIState prevState)
//    {
//        _controller.TheAvatar.anim.SetTrigger("Jump");
//        _controller.enabled = true;
//    }

//    public override void OnLeaveState()
//    {
//        _controller.enabled = false;
//    }
//}

//public class OldGroundState : OldStateBase
//{
//    private OldGroundController groundController;

//    public OldGroundState()
//    {
//        StateName = "GroundState";
//    }

//    public override void InitState()
//    {
//        groundController = _owner.AddComponent<OldGroundController>();
//        groundController.enabled = false;
//    }

//    public override void OnEnterState(OldIState prevState)
//    {
//        groundController.enabled = true;
//    }

//    public override void OnLeaveState()
//    {
//        groundController.enabled = false;
//    }
//}

//public class OldJumpState : OldStateBase
//{
//    private OldJumpController jumpController;

//    public OldJumpState()
//    {
//        StateName = "JumpState";
//    }

//    public override void InitState()
//    {
//        jumpController = _owner.AddComponent<OldJumpController>();
//        jumpController.enabled = false;
//    }

//    public override void OnEnterState(OldIState prevState)
//    {
//        jumpController.enabled = true;
//    }

//    public override void OnLeaveState()
//    {
//        jumpController.enabled = false;
//    }
//}