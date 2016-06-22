using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public Transform GroundCheckLeft;
    public Transform GroundCheckRight;
    public IAvatar Avatar { get; private set; }
    public float Speed = 9f;
    public float JumpVelocity = 28f;
    public bool HurtFromRight = true;

    private PlayerStateMachine _playerStateMachine;

    private void Awake()
    {
        this.Avatar = GetComponent<Avatar>();
        _playerStateMachine = gameObject.AddComponent<PlayerStateMachine>();
    }

    public string currentname = "";
    private void Update()
    {
        currentname = _playerStateMachine.CurrentStateName;
    }

    public void Hurt(int damage, bool fromRight)
    {
        _playerStateMachine.GotoState("HurtState");
        HurtFromRight = fromRight;
    }


}