using UnityEngine;
using System.Collections;

namespace SeafoodStudio
{
    public class Player : MonoBehaviour
    {
        public GroundChecker GroundChecker;
        public IAvatar Avatar { get; private set; }
        public float Speed = 9f;
        public float JumpVelocity = 28f;
        public bool HurtFromRight = true;
        public float ImmortalDuration = 2f;
        public GameObject explosion;
        public int HP = 5;

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
                HP -= damage;

                if (HP > 0)
                {
                    _playerControlStateMachine.GotoState("HurtState");
                    HurtFromRight = fromRight;
                    StartCoroutine(Immortal());
                }
                else
                {
                    Die();
                }
            }
        }

        private IEnumerator Immortal()
        {
            gameObject.layer = LayerMask.NameToLayer("Ignore Entity");
            yield return new WaitForSeconds(ImmortalDuration);
            gameObject.layer = LayerMask.NameToLayer("Player");
        }

        public void Die()
        {
            Collider2D[] cols = GetComponents<Collider2D>();
            foreach (Collider2D c in cols)
            {
                c.isTrigger = true;
            }
            OnExplode();
            Destroy(gameObject, 0.1f);
        }

        private void OnExplode()
        {
            // Create a quaternion with a random rotation in the z-axis.
            Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

            // Instantiate the explosion where the rocket is with the random rotation.
            var expl = Instantiate(explosion, transform.position, randomRotation);
            Destroy(expl, 1f);
        }
    }
}