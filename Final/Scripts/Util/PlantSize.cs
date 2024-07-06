using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSize : MonoBehaviour
{
    [SerializeField] GameObject seed;
    [SerializeField] GameObject smallPlant;
    [SerializeField] GameObject medPlant;

    private GameObject _currentPlant;

    public void ShowSeed()
    {
        UnShow();
        SetCurrentPlant(seed);
    }

    public void ShowSmallPlant()
    {
        UnShow();
        SetCurrentPlant(smallPlant);
    }

    public void ShowMedPlant()
    {
        UnShow();
        SetCurrentPlant(medPlant);
    }

    void SetCurrentPlant(GameObject currentPlant)
    {
        _currentPlant = currentPlant;
        _currentPlant.SetActive(true);
    }
    public void UnShow()
    {
        _currentPlant?.SetActive(false);
    }
}
