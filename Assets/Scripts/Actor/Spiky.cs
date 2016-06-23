using UnityEngine;
using System.Collections;

public class Spiky : Actor
{
    public float Direction { get; set; }

    protected override void InitStateController()
    {
        base.InitStateController();
        _stateController.AddState(new SpikyRollingState());
        _stateController.AddState(new SpikyLaydownState());
        _stateController.AddState(new SpikySlidingState());
        _stateController.AddState(new SpikyRiseupState());
    }

    private void Start()
    {
        _stateController.InitState("SpikyRollingState");
        Direction = 1f;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("edge"))
        {
            Direction *= -1;
            if (_stateController.CurrentStateName == "SpikyRollingState")
                _stateController.GotoState("SpikyLaydownState");
            else if (_stateController.CurrentStateName == "SpikySlidingState")
                _stateController.GotoState("SpikyRiseupState");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bool fromRight = (transform.position.x > other.transform.position.x);
            other.gameObject.GetComponent<Player>().Hurt(1, fromRight);
        }
    }

    public override void Hurt(int damage)
    {
        Die();
    }
}
