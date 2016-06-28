using UnityEngine;
using System.Collections;

namespace SeafoodStudio
{
    public abstract class Actor : MonoBehaviour
    {
        public float Speed = 9f;
        public float JumpVelocity = 28f;

        protected Avatar _avatar;
        protected ActorStateMachine _stateController;
        protected Behaviour _behaviour;

        protected virtual void Awake()
        {
            _avatar = GetComponent<Avatar>();
            _stateController = gameObject.GetComponent<ActorStateMachine>();
            _behaviour = gameObject.GetComponent<Behaviour>();
            Init();
        }

        protected virtual void Init() { }

        public virtual void Hurt(int damage) { }

        public virtual void Die()
        {
            Collider2D[] cols = GetComponents<Collider2D>();
            foreach (Collider2D c in cols)
            {
                c.isTrigger = true;
            }
            Destroy(gameObject, 0.1f);
        }
    }
}