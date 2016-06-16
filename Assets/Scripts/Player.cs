using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public Transform GroundCheck;
    public IAvatar Avatar { get; private set; }

    //private Old2PlayerStateMachine _playerStateMachine;
    private PlayerStateMachine _playerStateMachine;

    private void Awake()
    {
        InitAvatar();
        InitPlayerStateMachine();
        //Old2InitPlayerStateMachine();
    }

    private void InitAvatar()
    {
        var avatar = new Avatar();
        avatar.anim = GetComponent<Animator>();
        avatar.rb2d = GetComponent<Rigidbody2D>();
        avatar.transform = transform;
        this.Avatar = avatar;
    }

    private void InitPlayerStateMachine()
    {
        _playerStateMachine = gameObject.AddComponent<PlayerStateMachine>();
    }

    //private void Old2InitPlayerStateMachine()
    //{
    //    _playerStateMachine = gameObject.AddComponent<Old2PlayerStateMachine>();
    //    _playerStateMachine.AddState("GroundState", new Old2GroundState(_playerStateMachine));
    //    _playerStateMachine.AddState("JumpState", new Old2JumpState(_playerStateMachine));

    //    _playerStateMachine.GotoState("GroundState");
    //}
}

public interface IAvatar
{
    Animator anim { get; }
    Rigidbody2D rb2d { get; }

    void SetDirection(bool isRight);
}

public class Avatar : IAvatar
{
    public Animator anim { get; set; }
    public Rigidbody2D rb2d { get; set; }
    public Transform transform { get; set; }

    protected bool _facingRight = true;

    public void SetDirection(bool isRight)
    {
        if (_facingRight != isRight)
        {
            _facingRight = isRight;

            var theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}

//public class OldPlayer : MonoBehaviour
//{
//    private OldStateMachine playerStateController;

//    private void Start()
//    {
//        InitPlayerStateMachine();
//    }

//    private void InitPlayerStateMachine()
//    {
//        playerStateController = new OldStateMachine(gameObject);
//        playerStateController.AddState("GroundState", new OldGroundState());
//        playerStateController.AddState("JumpState", new OldJumpState());

//        playerStateController.GotoState("GroundState");
//    }

//    public void Jump()
//    {
//        playerStateController.GotoState("JumpState");
//    }
//}