using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public Transform GroundCheck;
    public IAvatar Avatar { get; private set; }
    public float Speed = 5f;

    private PlayerStateMachine _playerStateMachine;

    private void Awake()
    {
        InitAvatar();
        InitPlayerStateMachine();
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

    public string currentname = "";
    private void Update()
    {
        currentname = _playerStateMachine._stateMachine.CurrentState.StateName;
    }
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