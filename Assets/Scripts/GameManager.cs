using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace SeafoodStudio
{
    public class GameManager : SingletonObject<GameManager>
    {
        public Text GameOverText;

        private void Awake()
        {
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
        }



        //private Dictionary<string, EventObject> eventMap = new Dictionary<string, EventObject>();

        //public void AddObserver(string eventName, Action<object> action)
        //{
        //    if (!eventMap.ContainsKey(eventName))
        //        eventMap.Add(eventName, new EventObject());

        //    eventMap[eventName].Handler += action;
        //}

        //public void PostNotificationName(string eventName, object userInfo)
        //{
        //    if (eventMap.ContainsKey(eventName))
        //    {
        //        eventMap[eventName].Notify(userInfo);
        //    }
        //}

        //public class EventObject
        //{
        //    public event Action<object> Handler;

        //    public void Notify(object userInfo)
        //    {
        //        if (Handler != null)
        //            Handler(userInfo);
        //    }
        //}
    }
}