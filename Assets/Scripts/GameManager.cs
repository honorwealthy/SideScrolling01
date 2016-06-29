using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SeafoodStudio
{
    public class GameManager : MonoBehaviour
    {
        private Dictionary<string, EventObject> eventMap = new Dictionary<string, EventObject>();

        public void AddObserver(string eventName, Action<object> action)
        {
            if (!eventMap.ContainsKey(eventName))
                eventMap.Add(eventName, new EventObject());

            eventMap[eventName].Handler += action;
        }

        public void PostNotificationName(string eventName, object userInfo)
        {
            if (eventMap.ContainsKey(eventName))
            {
                eventMap[eventName].Notify(userInfo);
            }
        }

        public class EventObject
        {
            public event Action<object> Handler;

            public void Notify(object userInfo)
            {
                if (Handler != null)
                    Handler(userInfo);
            }
        }
    }
}