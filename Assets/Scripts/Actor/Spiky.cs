using UnityEngine;
using System.Collections;

namespace SeafoodStudio
{
    public class Spiky : Enemy
    {
        protected override void Init()
        {
            base.Init();
            _stateController.AddState(new SpikyMoveState());
            _stateController.AddState(new SpikyChangeState());
            _stateController.AddState(new SpikyHurtState());

            _stateController.InitState("SpikyMoveState");
        }

        public override void Hurt(int damage)
        {
            HP -= damage;

            if (HP > 0)
                _stateController.GotoState("SpikyHurtState");
            else
                Die();
        }
    }
}