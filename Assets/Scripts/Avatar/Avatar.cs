using UnityEngine;
using System.Collections;
using System;

namespace SeafoodStudio
{
    public class Avatar : MonoBehaviour, IAvatar
    {
        public Animator anim { get; set; }
        public Rigidbody2D rb2d { get; set; }
        public event Action<string> OnAnimationEvent;

        [SerializeField]
        protected bool _facingRight = true;

        private void Awake()
        {
            anim = GetComponent<Animator>();
            rb2d = GetComponent<Rigidbody2D>();
        }

        public void SetDirection(bool isRight)
        {
            if (_facingRight != isRight)
            {
                _facingRight = isRight;

                var theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
            }
        }

        public void AnimationEventCallback(string eventName)
        {
            if (OnAnimationEvent != null)
                OnAnimationEvent(eventName);
        }
    }
}