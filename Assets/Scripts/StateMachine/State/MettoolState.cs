using UnityEngine;
using System.Collections;
using System;

namespace SeafoodStudio
{
    public class MettoolStateBase : ActorState
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

    public class MettoolMoveState : MettoolStateBase
    {
    }

    public class MettoolHideState : MettoolStateBase
    {
        public override void OnEnterState(IState prevState)
        {
            _avatar.anim.SetTrigger("Hide");
            ((Mettool)_entity).IsImmortal = true;
        }

        public override void OnLeaveState()
        {
            _avatar.anim.SetTrigger("Move");
            ((Mettool)_entity).IsImmortal = false;
        }

        public override void FixedUpdate()
        {
            //cant move
        }
    }

    public class MettoolShootState : MettoolStateBase
    {
        public override void OnEnterState(IState prevState)
        {
            _avatar.anim.SetTrigger("Shoot");
        }

        protected override void OnAnimationEvent(string eventName)
        {
            if (eventName == "MettoolShootOver")
                _stateMachine.GotoState("MettoolMoveState");
        }

        public override void FixedUpdate()
        {
            //cant move
        }
    }

    public class MettoolHurtState : MettoolStateBase
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
            _stateMachine.GotoState("MettoolMoveState");
        }

        public override void FixedUpdate()
        {
            //cant move
        }
    }

    public class MettoolDeadState : MettoolStateBase
    {
        public override void FixedUpdate()
        {
            //cant move
        }
    }
}