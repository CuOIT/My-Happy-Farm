
namespace _Template.Script.Data
{
    public interface IData<T>
    {
        public T Value {
            get;set;
        }
        public void LoadData();

        public void SaveData();
    }
}
