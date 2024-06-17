using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CellUI : MonoBehaviour
{
    [SerializeField] GameObject water;
    [SerializeField] GameObject time;
    [SerializeField] GameObject collect;

    GameObject currentUI;

    private DateTime dateTime;
    private bool showTime = false;

    [SerializeField] TextMeshProUGUI txt;

    public void SetTime(long totalSeconds)
    {
        long min = totalSeconds/60;
        long sec = totalSeconds - min * 60;
        txt.SetText(min + "h" + sec + "s");
    }
    public void UnShow()
    {
        currentUI?.SetActive(false);
        showTime = false;
    }

    void SetCurrentUI(GameObject ui)
    {
        currentUI = ui;
        currentUI?.SetActive(true);
    }
    public void OnWater()
    {
        UnShow();
        SetCurrentUI(water);
        showTime = true;
    }

    public void OnTime()
    {
        UnShow();
        SetCurrentUI(time);
    }

    public void OnCollect()
    {
        UnShow();
        SetCurrentUI(collect);
    }
}
