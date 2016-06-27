using UnityEngine;
using System.Collections;

namespace SeafoodStudio
{
    public class ActorStateMachine : MonoBehaviour
    {
        private StateMachine<ActorState> _stateMachine;

        public string CurrentStateName { get { return _stateMachine.CurrentState.StateName; } }

        private void Awake()
        {
            _stateMachine = new StateMachine<ActorState>();
        }

        public void InitState(string statename)
        {
            _stateMachine.GotoState(statename);
        }

        public void GotoState(string statename)
        {
            StartCoroutine(GotoStateNextFrame(statename));
        }

        private IEnumerator GotoStateNextFrame(string statename)
        {
            yield return new WaitForEndOfFrame();
            _stateMachine.GotoState(statename);
        }

        public void AddState(ActorState state)
        {
            _stateMachine.AddState(state.StateName, state);
            state.InitState(gameObject.GetComponent<Actor>());
        }

        private void FixedUpdate()
        {
            _stateMachine.CurrentState.FixedUpdate();
        }

        private void Update()
        {
            _stateMachine.CurrentState.Update();
        }

        private void LateUpdate()
        {
            _stateMachine.CurrentState.LateUpdate();
        }
    }
}