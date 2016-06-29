using UnityEngine;
using System.Collections;

namespace SeafoodStudio
{
    public class DeathZone : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
                Destroy(other.gameObject, 2);
            else if (other.CompareTag("Player"))
            {
                other.gameObject.GetComponent<Player>().Die();
            }
        }
    }
}