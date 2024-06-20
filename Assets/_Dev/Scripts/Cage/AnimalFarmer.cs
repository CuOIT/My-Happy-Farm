using Assets._Dev.SO._CustomEvent;
using Cage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalFarmer : MonoBehaviour
{
    [SerializeField] ProductData productData;
    [SerializeField]AnimalUI animalUI;
    [SerializeField] GameObjectEvent collectGOEvent;        
    private ICage currentCage;

    public void Feed()
    {
        if(currentCage == null) return;
        productData.Consume(currentCage.GetFoodType());
        currentCage.Feed();
        ProductNum productNum = currentCage.GetProductType();
        productData.Add(productNum);
        for(int i = 0; i < productNum.num; i++)
        {
            GameObject go = GameManager.Instance.pooler.SpawnFromPool(productNum.type.ToString(),currentCage.GetPos(),Quaternion.identity);
            collectGOEvent.RaiseEvent(go);
        }
    }

    public bool EnoughBarn()
    {
        ProductNum foodRequire = currentCage.GetFoodType();
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
            currentCage.OnHumanComing();
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
