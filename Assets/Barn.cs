using UnityEngine;

public class Barn : MonoBehaviour
{
    [SerializeField]    BaseContainer cooking;
    [SerializeField]    BaseContainer storage;


    public void OnCook()
    {
        cooking.Show();
        storage.UnShow();
    }

    public void OnStorage()
    {
        cooking.UnShow();
        storage.Show();
    }
}
