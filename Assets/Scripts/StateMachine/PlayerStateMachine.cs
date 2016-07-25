using UnityEngine;
using System.Collections;

namespace SeafoodStudio
{
    public class PlayerStateMachine : MonoBehaviour
    {
        public Player Owner { get; private set; }

        private StateMachine<string, PlayerStateBase> _stateMachine;

        public string CurrentStateName { get { return _stateMachine.CurrentState.StateName; } }

        public void GotoState(string statename)
        {
            StartCoroutine(GotoStateNextFrame(statename));
        }

        private IEnumerator GotoStateNextFrame(string statename)
        {
            yield return new WaitForEndOfFrame();
            _stateMachine.GotoState(statename);
        }

        private void Awake()
        {
            Owner = GetComponent<Player>();
            InitStateMachine();
        }

        private void InitStateMachine()
        {
            _stateMachine = new StateMachine<string, PlayerStateBase>();
            AddState(new GroundState(this));
            AddState(new JumpState(this));
            AddState(new AirState(this));
            AddState(new GroundAttackState(this));
            AddState(new HurtState(this));
            AddState(new ReadyState(this));
            AddState(new EndState(this));
        }

        private void AddState(PlayerStateBase state)
        {
            _stateMachine.AddState(state.StateName, state);
        }

        private void Start()
        {
            _stateMachine.GotoState("ReadyState");
        }

        private void FixedUpdate()
        {
            _stateMachine.CurrentState.FixedUpdate();
        }

        private void Update()
        {
            if (Time.timeScale != 0)
                _stateMachine.CurrentState.Update();
        }

        private void LateUpdate()
        {
            if (Time.timeScale != 0)
                _stateMachine.CurrentState.LateUpdate();
        }
    }
}