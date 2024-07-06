using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPCollector : MonoBehaviour 
{
    private LevelController levelController;
    public void Awake()
    {
        levelController = GetComponent<LevelController>();
    }
    public void IncreaseEXP(int num)
    {
        levelController.CollectEXP(num);
    }
}
