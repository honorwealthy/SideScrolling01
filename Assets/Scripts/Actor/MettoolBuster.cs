using UnityEngine;
using System.Collections;

namespace SeafoodStudio
{
    public class MettoolBuster : MonoBehaviour
    {
        public float Speed = 10f;

        private void Start()
        {
            Destroy(gameObject, 2);
        }

        public void OnFire(float direction)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(direction * Speed, 0);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && !other.isTrigger)
            {
                Destroy(gameObject);

                bool fromRight = (transform.position.x > other.transform.position.x);
                other.gameObject.GetComponent<Player>().Hurt(1, fromRight);
            }
        }
    }
}