using UnityEngine;
using System.Collections;

namespace SeafoodStudio
{
    public class Player : MonoBehaviour
    {
        public Transform GroundCheckLeft;
        public Transform GroundCheckRight;
        public IAvatar Avatar { get; private set; }
        public float Speed = 9f;
        public float JumpVelocity = 28f;
        public bool HurtFromRight = true;
        public float ImmortalDuration = 2f;

        private PlayerStateMachine _playerControlStateMachine;

        private void Awake()
        {
            this.Avatar = GetComponent<Avatar>();
            _playerControlStateMachine = gameObject.AddComponent<PlayerStateMachine>();
        }

        public string currentname = "";
        private void Update()
        {
            currentname = _playerControlStateMachine.CurrentStateName;
        }

        public void Hurt(int damage, bool fromRight)
        {
            if (gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                _playerControlStateMachine.GotoState("HurtState");
                HurtFromRight = fromRight;
                StartCoroutine(Immortal());
            }
        }

        private IEnumerator Immortal()
        {
            gameObject.layer = LayerMask.NameToLayer("Ignore Enemy");
            yield return new WaitForSeconds(ImmortalDuration);
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }
}