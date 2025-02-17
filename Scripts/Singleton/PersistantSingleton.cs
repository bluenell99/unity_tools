// CREDIT: GitAmend 

using System;
using UnityEngine;

namespace Jamie.Framework
{
    public class PersistantSingleton<T> : MonoBehaviour where T : Component
    {
        public bool AutoUnparentOnAwake = true;

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
            if (!Application.isPlaying) return;

            if (AutoUnparentOnAwake)
            {
                transform.SetParent(null);
            }

            if (Instance == null)
            {
                instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                if (instance != this)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}