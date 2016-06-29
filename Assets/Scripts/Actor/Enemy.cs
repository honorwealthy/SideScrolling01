using UnityEngine;
using System.Collections;

namespace SeafoodStudio
{
    public abstract class Enemy : Actor
    {
        public GameObject explosion;

        protected virtual void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                bool fromRight = (transform.position.x > other.transform.position.x);
                other.gameObject.GetComponent<Player>().Hurt(1, fromRight);
            }
        }

        public override void Die()
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