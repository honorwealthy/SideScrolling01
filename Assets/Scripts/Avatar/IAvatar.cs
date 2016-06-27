using UnityEngine;
using System.Collections;
using System;

namespace SeafoodStudio
{
    public interface IAvatar
    {
        Animator anim { get; }
        Rigidbody2D rb2d { get; }
        event Action<string> OnAnimationEvent;

        void SetDirection(bool isRight);
    }
}