using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace _Template.Event{      
    
    public class BaseSOEvent: ScriptableObject, IEvent
    {
        public void OnEnable()
        {
            Debug.Log("Hello"+name);
        }
        readonly List<IEventListener> eventListeners = new List<IEventListener>();
        public void AddListener(IEventListener listener)
        {
            eventListeners.Add(listener);
        }

        public void RaiseEvent() {
#if UNITY_EDITOR
            Debug.Log("<color=green>Event Raise :     </color>"+ name);
            float time = Time.time;
#endif
            foreach(IEventListener listener in eventListeners)
            {
                listener.OnEventRaise();
#if UNITY_EDITOR
                Debug.Log("Listen to "+ name +" at "+time+ " :    "+ listener.ToString());
#endif
            }
        }
        public void RemoveListener(IEventListener listener)
        {
            eventListeners.Remove(listener);
        }

        public void RemoveAllListeners()
        {
            eventListeners.Clear();
        }

    }

    public class BaseSOEvent<T> : ScriptableObject, IEvent<T>
    {
        [SerializeField] T testValueOnly;
        [SerializeField] readonly List<IEventListener<T>> eventListeners = new List<IEventListener<T>>();
        public void AddListener(IEventListener<T> listener)
        {
            eventListeners.Add(listener);
        }

        [ContextMenu("Raise")]
        [ContextMenu("Raise")]
        public void RaiseTestEvent()
        {
            if (testValueOnly!=null)
            {
                RaiseEvent(testValueOnly);
            }
        }
        public void RaiseEvent(T t)
        {
#if UNITY_EDITOR
            Debug.Log("<color=green>Event Raise :     </color>" + name);
            float time = Time.time;
#endif
            foreach (IEventListener<T> listener in eventListeners)
            {
#if UNITY_EDITOR
                Debug.Log("Listen to " + name + " at " + time + " :    " + listener.ToString());
#endif
                listener.OnEventRaise(t);
            }
        }
        public void RemoveListener(IEventListener<T> listener)
        {
            eventListeners.Remove(listener);
        }

        public void RemoveAllListeners()
        {
            eventListeners.Clear();
        }

    }
    public class BaseSOEvent<T1,T2> : ScriptableObject, IEvent<T1,T2>
    {
        [SerializeField] T1 testValueOnly1;
        [SerializeField] T2 testValueOnly2;
        [SerializeField] readonly List<IEventListener<T1,T2>> eventListeners = new List<IEventListener<T1,T2>>();
        public void AddListener(IEventListener<T1,T2> listener)
        {
            eventListeners.Add(listener);
        }

        [ContextMenu("Raise")]
        [ContextMenu("Raise")]
        public void RaiseTestEvent()
        {
            if (testValueOnly1!=null && testValueOnly2!=null)
            {
                RaiseEvent(testValueOnly1,testValueOnly2);
            }
        }
        public void RaiseEvent(T1 t1,T2 t2)
        {
#if UNITY_EDITOR
            Debug.Log("<color=green>Event Raise :     </color>" + name);
            float time = Time.time;
#endif
            foreach (IEventListener<T1,T2> listener in eventListeners)
            {
#if UNITY_EDITOR
                Debug.Log("Listen to " + name + " at " + time + " :    " + listener.ToString());
#endif
                listener.OnEventRaise(t1,t2);
            }
        }
        public void RemoveListener(IEventListener<T1,T2> listener)
        {
            eventListeners.Remove(listener);
        }

        public void RemoveAllListeners()
        {
            eventListeners.Clear();
        }

    }

#if UNITY_EDITOR
    [CustomEditor(typeof(BaseSOEvent),true)]
        public class BaseSOEventEditor : Editor
        {
            BaseSOEvent soEvent;

            public void OnEnable()
            {
                soEvent = target as BaseSOEvent;
            }
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();
                if (GUILayout.Button("RaiseEvent"))
                {
                    soEvent.RaiseEvent();
                }
            }
        }
        
        
    #endif
}
