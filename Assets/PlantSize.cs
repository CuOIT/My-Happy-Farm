using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSize : MonoBehaviour
{
    [SerializeField] GameObject smallPlant;
    [SerializeField] GameObject medPlant;

    public void ShowSmallPlant()
    {
        smallPlant?.SetActive(true);
        medPlant?.SetActive(false);
    }

    public void ShowMedPlant()
    {
        smallPlant?.SetActive(false);
        medPlant?.SetActive(true);
    }
    public void UnShow()
    {
        smallPlant?.SetActive(false);
        medPlant?.SetActive(false);
    }
}
