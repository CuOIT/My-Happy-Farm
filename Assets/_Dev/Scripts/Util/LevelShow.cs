using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelShow : MonoBehaviour
{
    [SerializeField] UIShower uiShower;
    [SerializeField] TextMeshProUGUI txt;
    public void OnLevelUp(int level)
    {
        uiShower.Show();
        txt.SetText(level.ToString());
    }
    public void UnShow()
    {
        uiShower.UnShow();
    }
}
