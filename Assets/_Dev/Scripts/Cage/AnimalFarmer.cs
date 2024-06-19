using Assets._Dev.SO._CustomEvent;
using Cage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalFarmer : MonoBehaviour
{
    [SerializeField] ProductData productData;
    [SerializeField]AnimalUI animalUI;
        
    private ICage currentCage;

    public void Feed()
    {
        if(currentCage == null) return;
        productData.Consume(currentCage.GetFoodType());
    }

    public bool EnoughBarn()
    {
        ProductNum foodRequire = currentCage.GetProductType();
        return foodRequire.num <= productData.Value[foodRequire.type];
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cage"))
        {
            currentCage = other.GetComponent<ICage>();
        }
        if(currentCage != null)
        {
            animalUI.Show();
            animalUI.Init(currentCage.GetFoodType());
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cage"))
        {
            currentCage = other.GetComponent<ICage>();
        }
        if(currentCage != null)
        {
            animalUI.Hide();
        }
    }
}
