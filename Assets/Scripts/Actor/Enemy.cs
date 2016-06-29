using UnityEngine;
using System.Collections;

namespace SeafoodStudio
{
    public abstract class Enemy : Actor
    {
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
            Destroy(gameObject, 0.1f);
        }
    }
}