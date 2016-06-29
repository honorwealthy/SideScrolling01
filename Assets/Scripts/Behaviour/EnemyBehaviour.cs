using UnityEngine;
using System.Collections;

namespace SeafoodStudio
{
    public class EnemyBehaviour : Behaviour
    {
        private bool _rethink = true;
        
        public float ThinkingTime = 1f;

        protected override void Awake()
        {
            base.Awake();
            gameObject.GetComponent<Avatar>().OnAnimationEvent += OnAnimationEvent;
        }

        protected virtual void FixedUpdate()
        {
            if (_rethink)
            {
                _entity.StartCoroutine(Thinking());
                _rethink = false;
            }
        }

        private IEnumerator Thinking()
        {
            yield return new WaitForSeconds(ThinkingTime);
            _rethink = true;
            Think();
        }

        protected virtual void Think() { }

        protected virtual void OnAnimationEvent(string eventName) { }
    }
}