using UnityEngine;
using System.Collections;

namespace SeafoodStudio
{
    public class SpikyBehaviour : EnemyBehaviour
    {
        public GameObject ForwardChecker;
        public GameObject WallChecker;

        protected override void Awake()
        {
            base.Awake();
            Direction = gameObject.transform.localScale.x > 0 ? 1 : -1;
        }

        protected override void FixedUpdate()
        {
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

        protected override void Think()
        {
            var rand = Random.Range(0, 8);
            if (_stateController.CurrentStateName == "SpikyMoveState")
            {
                if (rand < 1)
                {
                    _stateController.GotoState("SpikyChangeState");
                }
            }
        }
    }
}