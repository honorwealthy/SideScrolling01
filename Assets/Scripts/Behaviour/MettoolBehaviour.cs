using UnityEngine;
using System.Collections;

namespace SeafoodStudio
{
    public class MettoolBehaviour : EnemyBehaviour
    {
        public GameObject Buster;
        public Transform Spurt;
        public GameObject ForwardChecker;

        protected override void Awake()
        {
            base.Awake();
            Direction = gameObject.transform.localScale.x > 0 ? -1 : 1;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            if (!CheckGround())
            {
                Direction *= -1;
            }
        }

        private bool CheckGround()
        {
			return ForwardChecker.GetComponent<GroundChecker> ().CheckGround();
        }

        protected override void Think()
        {
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

        protected override void OnAnimationEvent(string eventName)
        {
            if (eventName == "MettoolShootBuster")
            {
                var buster = Instantiate(Buster, Spurt.position, Quaternion.identity) as GameObject;
                buster.GetComponent<MettoolBuster>().OnFire(Direction);
            }
        }
    }
}