using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace SeafoodStudio
{
    public class HPViewer : MonoBehaviour
    {
        public Image[] hearts;
        public static HPViewer Instance;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);

            foreach (var h in hearts)
            {
                h.enabled = false;
            }
        }

        public void SetHP(int hp)
        {
            for (int i = 0; i < hearts.Length; i++)
            {
                hearts[i].enabled = (i < hp);
            }
        }
    }
}