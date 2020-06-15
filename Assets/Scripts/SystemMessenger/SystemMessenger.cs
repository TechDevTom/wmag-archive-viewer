using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Messenger
{
    /// <summary>
    /// System Messenger class which stores listeners and delegates messages to appropriate targets
    /// </summary>
    public class SystemMessenger : MonoBehaviour
    {
        static IList<GameObject> Targets = new List<GameObject>();

        /// <summary>
        /// Register a target object
        /// </summary>
        /// <param name="target"></param>
        public static void AddListener(GameObject target)
        {
            if (!Targets.Contains(target))
            {
                Targets.Add(target);
            }
        }

        /// <summary>
        /// Send a message to all targets with a handler of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Data"></param>
        /// <param name="functor"></param>
        public static void Send<T>(ExecuteEvents.EventFunction<T> functor) where T : ISystemMessageTarget
        {
            BaseEventData Data = new BaseEventData(EventSystem.current);
            foreach(var t in Targets)
            {
                if (ExecuteEvents.CanHandleEvent<T>(t))
                {
                    ExecuteEvents.Execute<T>(t, Data, functor);
                }
            }
        }

        /// <summary>
        /// Removes a target object from the messenger targets
        /// </summary>
        /// <param name="target"></param>
        public static void RemoveListener(GameObject target)
        {
            if (!Targets.Contains(target))
            {
                Targets.Remove(target);
            }
        }
    }
}
