using UnityEngine;
using System.Collections;

namespace SeafoodStudio
{
    public class SpikyBehaviour : Behaviour
    {
        private bool _rethink = true;
        public GameObject ForwardChecker;
        public GameObject WallChecker;

        protected override void Awake()
        {
            base.Awake();
            Direction = gameObject.transform.localScale.x > 0 ? 1 : -1;
        }

        private void FixedUpdate()
        {
            if (_rethink)
            {
                _entity.StartCoroutine(Think());
                _rethink = false;
            }

            if (!CheckGround() || CheckWall())
            {
                Direction *= -1;
            }
        }

        private bool CheckGround()
        {
			return ForwardChecker.GetComponent<GroundChecker>().CheckGround();
        }

        private bool CheckWall()
        {
            return WallChecker.GetComponent<GroundChecker>().CheckGround();
        }

        private IEnumerator Think()
        {
            yield return new WaitForSeconds(1f);
            _rethink = true;

            var rand = Random.Range(0, 8);
            if (_stateController.CurrentStateName == "SpikyRollingState")
            {
                if (rand < 1)
                {
                    _stateController.GotoState("SpikyLaydownState");
                }
            }
            else if (_stateController.CurrentStateName == "SpikySlidingState")
            {
                if (rand < 1)
                {
                    _stateController.GotoState("SpikyRiseupState");
                }
            }
        }
    }
}