using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SeafoodStudio
{
    public class GameManager : MonoBehaviour
    {
        public Text GameOverText;
        public Text PauseText;
        public static GameManager Instance;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);

            GameOverText.text = "";
        }

        public void GameOver(bool win)
        {
            if (win)
            {
                GameOverText.text = "You Win";
            }
            else
            {
                GameOverText.text = "You Lose";
            }
            StartCoroutine(BackToMainMenu());
        }

        private IEnumerator BackToMainMenu()
        {
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(0);
        }

        private bool isPause = false;
        public void Pause()
        {
            isPause = !isPause;
            Time.timeScale = isPause ? 0 : 1;
            PauseText.text = isPause ? "~ Pause ~" : "";
        }

        private void Update()
        {
            if (Input.GetButtonDown("Esc"))
                Pause();
        }
    }
}