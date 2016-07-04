using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SeafoodStudio
{
    public class Menu : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene(1);
        }

        public void CloseGame()
        {
            Application.Quit();
        }

        private void Update()
        {
            if (Input.GetButtonDown("Esc"))
            {
                CloseGame();
            }
        }
    }
}