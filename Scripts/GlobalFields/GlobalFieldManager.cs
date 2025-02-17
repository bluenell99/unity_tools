using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

namespace Jamie.Framework
{
    public static class GlobalFieldManager
    {
        private class FieldData
        {
            public object Value { get; set; }
            public bool AlwaysFire { get; set; }
        }

        private static readonly ConcurrentDictionary<string, FieldData> _fields = new();
        private static readonly ConcurrentDictionary<string, List<Delegate>> _observers = new();

        /// <summary>
        /// Set the value of a global field. First time set will initialise the value and notify observers.
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="alwaysFire"></param>
        /// <typeparam name="T"></typeparam>
        public static void SetField<T>(GlobalField<T> field, T value, bool alwaysFire = false)
        {
            _fields.AddOrUpdate(field.Name,
                _ =>
                {
                    Debug.Log($"Initializing {field.Name} with value {value}");
                    NotifyObservers(field, value);
                    return new FieldData { Value = value, AlwaysFire = alwaysFire };
                },
                (_, existing) =>
                {
                    bool hasChanged = !Equals(existing.Value, value) || existing.AlwaysFire;

                    Debug.Log($"Setting Field {field.Name} - Old: {existing.Value}, New: {value}, hasChanged: {hasChanged}");

                    if (hasChanged)
                    {
                        NotifyObservers(field, value);
                    }

                    existing.Value = value;
                    existing.AlwaysFire = alwaysFire;
                    return existing;
                });
        }

        /// <summary>
        /// Returns the value of a global field
        /// </summary>
        public static T? GetField<T>(GlobalField<T> field)
        {
            return _fields.TryGetValue(field.Name, out var fieldData) ? (T)fieldData.Value : default;
        }

        /// <summary>
        /// Observe a field and invoke the callback on value change
        /// </summary>
        /// <param name="field"></param>
        /// <param name="observer"></param>
        public static void ObserveField<T>(GlobalField<T> field, Action<T> observer)
        {
            _observers.AddOrUpdate(field.Name,
                _ =>
                {
                    Debug.Log($"Observer added for {field.Name}");
                    return new List<Delegate> { observer };
                },
                (_, existing) =>
                {
                    existing.Add(observer);
                    Debug.Log($"Additional observer added for {field.Name}");
                    return existing;
                });
        }


        private static void NotifyObservers<T>(GlobalField<T> field, T value)
        {
            if (_observers.TryGetValue(field.Name, out var observerList))
            {
                Debug.Log($"Notifying {observerList.Count} observers for {field.Name} with value {value}");

                foreach (var observer in observerList)
                {
                    try
                    {
                        (observer as Action<T>)?.Invoke(value);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"Error notifying observer for {field.Name}: {ex}");
                    }
                }
            }
            else
            {
                Debug.LogWarning($"No observers found for {field.Name}");
            }
        }
    }
}