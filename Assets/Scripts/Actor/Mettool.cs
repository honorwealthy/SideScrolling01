using UnityEngine;
using System.Collections;

namespace SeafoodStudio
{
    public class Mettool : Actor
    {
        protected override void InitStateController()
        {
            base.InitStateController();
            _stateController.AddState(new MettoolMoveState());
            _stateController.AddState(new MettoolHideState());
            _stateController.AddState(new MettoolShootState());

            _stateController.InitState("MettoolMoveState");
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

        public void Immortal(bool on)
        {
            gameObject.layer = on ? LayerMask.NameToLayer("Ignore Entity") : LayerMask.NameToLayer("Enemy");
        }
    }
}