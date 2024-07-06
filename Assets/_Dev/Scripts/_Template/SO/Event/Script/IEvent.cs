
namespace _Template.Event{
    
    public interface IEvent 
    {
        public void RaiseEvent();

        public void AddListener(IEventListener listener);

        public void RemoveListener(IEventListener listener);

        public void RemoveAllListeners();
    }
    
    public interface IEvent<T>
    {
        public void RaiseEvent(T t);

        public void AddListener(IEventListener<T> listener);

        public void RemoveListener(IEventListener<T> listener);
        public void RemoveAllListeners();
    }
    public interface IEvent<T1,T2>
    {
        public void RaiseEvent(T1 t1,T2 t2);

        public void AddListener(IEventListener<T1,T2> listener);

        public void RemoveListener(IEventListener<T1,T2> listener);
        public void RemoveAllListeners();
    }
}
