using UnityEngine;
using System.Collections;

public class Mettool : Actor
{
    public float Direction { get; set; }

    protected override void InitStateController()
    {
        base.InitStateController();
        _stateController.AddState(new MettoolMoveState(_stateController));
        _stateController.AddState(new MettoolHideState(_stateController));
        _stateController.AddState(new MettoolShootState(_stateController));
    }

    private void Start()
    {
        _stateController.InitState("MettoolMoveState");
        Direction = -1f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("edge"))
        {
            Direction *= -1;
        }
    }

    public override void Hurt(int damage)
    {
        Destroy(gameObject, 0.1f);
    }
}
