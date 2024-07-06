
namespace _Template.Event{
    
    public interface IEventListener 
    {
        public void OnEventRaise();
    }
    public interface IEventListener<T>
    {
        public void OnEventRaise(T t);
    }public interface IEventListener<T1,T2>
    {
        public void OnEventRaise(T1 t1,T2 t2);
    }
    
}
