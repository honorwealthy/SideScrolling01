using UnityEngine;
using System.Collections;
using System;

namespace SeafoodStudio
{
    public class SpikyStateBase : ActorState
    {
        public override void FixedUpdate()
        {
            var direction = _behaviour.Direction;
            var speed = _entity.Speed;
            _avatar.rb2d.velocity = new Vector2(direction * speed, _avatar.rb2d.velocity.y);

            if (direction != 0)
                _avatar.SetDirection(direction > 0);
        }
    }

    public class SpikyMoveState : SpikyStateBase
    {
    }

    public class SpikyChangeState : SpikyStateBase
    {
        public override void OnEnterState(IState prevState)
        {
            _avatar.anim.SetTrigger("Change");
        }

        protected override void OnAnimationEvent(string eventName)
        {
            if (eventName == "SpikyLaydownOver" || eventName == "SpikyRiseupOver")
            {
                _stateMachine.GotoState("SpikyMoveState");
            }
        }

        public override void FixedUpdate()
        {
            //cant move
        }
    }

    public class SpikyHurtState : SpikyStateBase
    {
        public override void OnEnterState(IState prevState)
        {
            _avatar.anim.SetTrigger("Hurt");
            _entity.StopCoroutine(EndHurt());
            _entity.StartCoroutine(EndHurt());
        }

        public override void OnLeaveState()
        {
            _avatar.anim.SetTrigger("Move");
        }

        private IEnumerator EndHurt()
        {
            yield return new WaitForSeconds(0.5f);
            _stateMachine.GotoState("SpikyMoveState");
        }

        public override void FixedUpdate()
        {
            //cant move
        }
    }

    public class SpikyDeadState : SpikyStateBase
    {
        public override void FixedUpdate()
        {
            //cant move
        }
    }
}