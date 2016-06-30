using UnityEngine;
using System.Collections;

namespace SeafoodStudio
{
    public class Goal : MonoBehaviour
    {
        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                GameManager.Instance.GameOver(true);
                var lastPos = other.gameObject.transform.position;
                other.gameObject.transform.position = new Vector3(gameObject.transform.position.x, lastPos.y, lastPos.z);
                other.gameObject.GetComponent<Player>().EndGame();
            }
        }
    }
}
