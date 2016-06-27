using UnityEngine;
using System.Collections;

namespace SeafoodStudio
{
    public class SpikyBehaviour : Behaviour
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

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("edge"))
            {
                Direction *= -1;
            }
        }
    }
}