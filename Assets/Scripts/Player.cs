using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    private StateMachine playerStateController;

    private void Start()
    {
        InitPlayerState();
    }

    private void InitPlayerState()
    {
        playerStateController = new StateMachine(gameObject);
        playerStateController.AddState("GroundState", new GroundState());

        playerStateController.GotoState("GroundState");
    }
}