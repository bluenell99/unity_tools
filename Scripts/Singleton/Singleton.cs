// Credit: GitAmend

using System;
using UnityEngine;

namespace Jamie.Framework
{


    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (Instance == null)
                {
                    instance = FindAnyObjectByType<T>();
                    if (instance == null)
                    {
                        GameObject go = new GameObject(typeof(T).Name);
                        instance = go.AddComponent<T>();

                    }
                }

                return instance;
            }
        }

        public static bool HasInstance => Instance != null;
        public static T TryGetInstance() => HasInstance ? Instance : null;

        protected virtual void Awake()
        {
            if (!Application.isPlaying)
            {
                return;
            }

            instance = this as T;
        }
    }
}
