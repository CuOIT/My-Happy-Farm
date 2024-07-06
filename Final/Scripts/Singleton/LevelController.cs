using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    //UI
    [SerializeField] TextMeshProUGUI levelNum;
    [SerializeField] Image processExp;

    //data
    [SerializeField] IntData level;
    [SerializeField] IntData Exp;
    [SerializeField] LevelEXPInfos expInfo;
    [SerializeField] int maxEXP;
    [SerializeField] IntEvent levelUp;

    [SerializeField] float minWidth;
    [SerializeField] float maxWidth;
    private float length;
    public void Start()
    {
        GetMaxEXP();
        length = maxWidth - minWidth;
        SetExp(Exp.Value);
        SetLevel(level.Value);
    }

    void GetMaxEXP()
    {
        maxEXP = expInfo.GetlevelEXP(level.Value);
    }
    void SetLevel(int level)
    {
        levelNum.SetText(level.ToString());
    }
    void SetExp(int exp)
    {
        Exp.Value=exp;
        float process = (float)exp / maxEXP;
        processExp.rectTransform.DOSizeDelta(new Vector2(minWidth+process*length, 0), 0.2f);
    }
    public void CollectEXP(int num)
    {
        GameObject go = GameManager.Instance.pooler.SpawnFromPool("EXP", Vector3.zero, Quaternion.identity);
        EXPUI exp = go.GetComponent<EXPUI>();
        exp.ShowEXP(num);
        int nextEXP = Exp.Value + num;
        if (nextEXP >= maxEXP)
        {
            SetExp(nextEXP-maxEXP);
            LevelUp();
        }
        else
        {
            SetExp(nextEXP);
        }
    }
    void LevelUp()
    {
        int nextLevel = level.Value + 1;
        level.Value = nextLevel;
        levelUp.RaiseEvent(nextLevel);
        maxEXP = expInfo.GetlevelEXP(nextLevel);
        SetLevel(nextLevel);
    }
}
