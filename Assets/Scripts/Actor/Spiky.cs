using UnityEngine;
using System.Collections;

public class Spiky : Actor
{
    protected override void InitStateController()
    {
        base.InitStateController();
        _stateController.AddState(new SpikyRollingState());
        _stateController.AddState(new SpikyLaydownState());
        _stateController.AddState(new SpikySlidingState());
        _stateController.AddState(new SpikyRiseupState());

        _stateController.InitState("SpikyRollingState");
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
