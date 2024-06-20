using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Parcel : MonoBehaviour
{
    [SerializeField] GameObject visualField;
    [SerializeField] GameObject lockField;
    [SerializeField] ParcelInfoSO parcelInfo;
    public ParcelInfoSO ParcelInfo=>parcelInfo;

    [Header("CONTRAINT")]
    public List<GameObject> constraintGO;

    private enum State
    {
        LOCK,
        UNLOCK,
        BOUGHT,
        COUNT,
    }

    void ChangeState(State state)
    {
        switch (state)
        {
            case State.LOCK:
                HideParcel();
                break;
            case State.UNLOCK:
                UnlockParcel();
                break;
            case State.BOUGHT:
                BoughtParcel();
                break;
        }
    }
    public void OnBoughtThisParcel()
    {
        BoughtParcel();
    }
    public void OnUnlockThisParcel()
    {
        UnlockParcel();
    }
    public void InitByLevel(int playerLevel)
    {
        int levelUnlock = parcelInfo.LevelUnlock;
        if (levelUnlock > playerLevel)
        {
            ChangeState(State.LOCK);
        }
        else
        {
            if (parcelInfo.Value > 0)
            {
                ChangeState(State.UNLOCK);
            }
            else
            {
                ChangeState(State.BOUGHT);
            }
        }
    }
    public void HideParcel()
    {
        visualField.SetActive(false);
        lockField.SetActive(false);
        SetActiveContraintGO(false);
    }
    [Button]
    public void UnlockParcel()
    {
        visualField.SetActive(false);
        lockField.SetActive(true);
        SetActiveContraintGO(false);
    }

    public void BoughtParcel()
    {
        visualField.SetActive(true);
        lockField.SetActive(false);
        SetActiveContraintGO(true);
    }
    public void SetActiveContraintGO(bool active)
    {
        foreach(var go in constraintGO)
        {
            go.SetActive(active);
        }
    }
#if UNITY_EDITOR
    [Button]
    public void InitSO()
    {
        string pattern = @"Parcel\s*(\d+)-(\d+)";
        Regex regex = new Regex(pattern);
        string name = gameObject.name;
        Match match = regex.Match(name);
        string folderPath = "Assets/_Dev/SO/Data/Parcel";
        if (match.Success)
        {
            int x = int.Parse(match.Groups[1].Value);
            int y = int.Parse(match.Groups[2].Value);
            Debug.Log(x+"-"+y);
            ParcelInfoSO parcelInfoSO = AssetDatabase.LoadAssetAtPath<ParcelInfoSO>(folderPath +$"/p{x}{y}.asset");
            if (parcelInfoSO == null)
            {
                ParcelInfoSO newP = ScriptableObject.CreateInstance<ParcelInfoSO>();
                newP.name = "p" + x + y;
                AssetDatabase.CreateAsset(newP, folderPath +$"/{newP.name}.asset");
                AssetDatabase.SaveAssets();
                parcelInfoSO = newP;
            }
            parcelInfo = parcelInfoSO   ;
        }
    }
#endif
}
