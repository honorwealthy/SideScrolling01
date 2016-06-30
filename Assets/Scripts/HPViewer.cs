using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace SeafoodStudio
{
    public class HPViewer : SingletonObject<HPViewer>
    {
        public Image[] hearts;

        private void Awake()
        {
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