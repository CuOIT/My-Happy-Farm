using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParcelController : MonoBehaviour
{
    public List<Parcel> parcels;
    [SerializeField] IntData Level;
    [Button]
    public void InitParcel()
    {
        parcels = GetComponentsInChildren<Parcel>().ToList();
    }
    public void Awake()
    {
        int level = Level.Value;
        foreach (var parcel in parcels)
        {
            parcel.InitByLevel(level);
        }
    }

    public void OnLevelUp(int level)
    {
        foreach (var parcel in parcels)
        {
            parcel.InitByLevel(level);
        }
    }
}
