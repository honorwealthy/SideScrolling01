using UnityEngine;
using System.Collections;

namespace SeafoodStudio
{
    public class Mettool : Enemy
    {
        public bool IsImmortal { get; set; }

        protected override void Init()
        {
            base.Init();
            _stateController.AddState(new MettoolMoveState());
            _stateController.AddState(new MettoolHideState());
            _stateController.AddState(new MettoolShootState());
            _stateController.AddState(new MettoolHurtState());
            _stateController.AddState(new MettoolDeadState());

            _stateController.InitState("MettoolMoveState");

            IsImmortal = false;
        }

        public override void Hurt(int damage)
        {
            if (!IsImmortal)
            {
                HP -= damage;

                if (HP > 0)
                    _stateController.GotoState("MettoolHurtState");
                else
                {
                    _stateController.GotoState("MettoolDeadState");
                    Die();
                }
            }
        }
    }
}