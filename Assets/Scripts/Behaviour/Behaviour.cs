using UnityEngine;
using System.Collections;

namespace SeafoodStudio
{
    public abstract class Behaviour : MonoBehaviour
    {
        protected Actor _entity;
        protected ActorStateMachine _stateController;

        public float Direction { get; protected set; }

        protected virtual void Awake()
        {
            _entity = gameObject.GetComponent<Actor>();
            _stateController = gameObject.GetComponent<ActorStateMachine>();

            gameObject.GetComponent<Avatar>().OnAnimationEvent += OnAnimationEvent;
        }

        protected virtual void OnAnimationEvent(string eventName) { }
    }
}