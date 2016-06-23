using UnityEngine;
using System.Collections;

public class MettoolBehaviour : Behaviour
{
    private bool _rethink = true;

    protected override void Awake()
    {
        base.Awake();
        Direction = -1;
    }

    private void FixedUpdate()
    {
        if (_rethink)
        {
            _entity.StartCoroutine(Think());
            _rethink = false;
        }
    }

    private IEnumerator Think()
    {
        yield return new WaitForSeconds(1f);
        _rethink = true;

        var rand = Random.Range(0, 5);
        if (_stateController.CurrentStateName == "MettoolMoveState")
        {
            if (rand < 1)
            {
                _stateController.GotoState("MettoolShootState");
            }
            else if (rand < 2)
            {
                _stateController.GotoState("MettoolHideState");
            }
        }
        else if (_stateController.CurrentStateName == "MettoolHideState")
        {
            if (rand < 2)
            {
                _stateController.GotoState("MettoolMoveState");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("edge"))
        {
            Direction *= -1;
        }
    }

    protected override void OnAnimationEvent(string eventName)
    {
        if (eventName == "MettoolShootBuster")
        {
            var mettool = _entity as Mettool;
            var buster = Instantiate(mettool.Buster, mettool.transform.Find("Spurt").position, Quaternion.identity) as GameObject;
            buster.GetComponent<MettoolBuster>().BeginFly(Direction);
        }
    }
}
