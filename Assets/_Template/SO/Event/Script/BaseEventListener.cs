using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace _Template.Event
{

    public class BaseEventListener : MonoBehaviour,IEventListener
    {
        [SerializeField] BaseSOEvent _event;
        [SerializeField] UnityEvent _eventListener;
        public void OnEventRaise()
        {
            _eventListener?.Invoke();
        }

        public void OnEnable()
        {
            _event?.AddListener(this);
        }

        public void OnDisable()
        {
            _event?.RemoveListener(this);
        }
    }
    public class BaseEventListener<T> : MonoBehaviour, IEventListener<T>
    {
        [SerializeField] BaseSOEvent<T> _event;
        [SerializeField] UnityEvent<T> _eventListener;

        public void OnEventRaise(T t)
        {
            _eventListener?.Invoke(t);  
        }

        public void OnEnable()
        {
            _event?.AddListener(this);
        }

        public void OnDisable()
        {
            _event?.RemoveListener(this);
        }
    }
    public class BaseEventListener<T1,T2> : MonoBehaviour, IEventListener<T1,T2>
    {
        [SerializeField] BaseSOEvent<T1,T2> _event;
        [SerializeField] UnityEvent<T1,T2> _eventListener;

        public void OnEventRaise(T1 t1,T2 t2)
        {
            _eventListener?.Invoke(t1,t2);  
        }

        public void OnEnable()
        {
            _event?.AddListener(this);
        }

        public void OnDisable()
        {
            _event?.RemoveListener(this);
        }
    }

}
