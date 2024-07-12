using _Template.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : ScriptableObject
{
    public string description;
    public int currentProgress;
    public int targetProgress;
    public int reward;
    public bool active;
    public event Action missionChange;
    public SimpleEvent completeEvent;

    public bool collected = false;

    public virtual void IncrementProgress(int num)
    {
        if (!active) return;
        if (collected) return;
        missionChange?.Invoke();
        currentProgress+=num;
        if (currentProgress >= targetProgress)
        {
            currentProgress = targetProgress;
            completeEvent.RaiseEvent();
        }
    }

    public virtual void ResetProgress()
    {
        active = true;
        currentProgress = 0;
        collected = false;
    }

    public void UnActive()
    {
        active = false;
    }
    public bool IsDone()
    {
        return currentProgress >= targetProgress;
    }

}
