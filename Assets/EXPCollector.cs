using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPCollector : MonoBehaviour 
{
    [SerializeField] IntEvent collectEXPEvent;
    public void IncreaseEXP(int num)
    {
        collectEXPEvent.RaiseEvent(num);
    }
}
