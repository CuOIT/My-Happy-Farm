using Assets._Dev.SO._CustomEvent;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;

[CreateAssetMenu(fileName = "NewParcelInfo", menuName = "Data/Parcel")]
public class ParcelInfoSO : LocalData<int>
{

    [SerializeField] int _levelUnlock=100; 

    public int LevelUnlock=>_levelUnlock;


}
