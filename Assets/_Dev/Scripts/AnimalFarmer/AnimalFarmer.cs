using _Template.Event;
using Assets._Dev.SO._CustomEvent;
using Cage;
using UnityEngine;

public class AnimalFarmer : MonoBehaviour
{
    [SerializeField] ProductData productData;
    [SerializeField] GameObjectEvent collectGOEvent;        
    private ICage currentCage;
    [SerializeField] ProductNumEvent ShowUIAnimalEvent;
    [SerializeField] SimpleEvent UnShowEvent;
    [SerializeField] ProductNumEvent feedEvent;
    [SerializeField] ProductNumEvent collectProductEvent;
    public void Feed()
    {
        if(currentCage == null) return;
        ProductNum foodReq = currentCage.GetFoodType();
        productData.Consume(foodReq);
        feedEvent.RaiseEvent(foodReq);
        currentCage.Feed();
        ProductNum productNum = currentCage.GetProductType();
        productData.Add(productNum);
        collectProductEvent.RaiseEvent(productNum);
        for(int i = 0; i < productNum.num; i++)
        {
            GameObject go = GameManager.Instance.pooler.SpawnFromPool(productNum.type.ToString(),currentCage.GetPos(),Quaternion.identity);
            collectGOEvent.RaiseEvent(go);
        }
    }

    public bool EnoughBran()
    {
        ProductNum foodRequire = currentCage.GetFoodType();
        return foodRequire.num <= productData.Value[foodRequire.type];
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cage"))
        {
            currentCage = other.GetComponent<ICage>();
            if(currentCage != null)
            {
                currentCage.OnHumanComing();
                ShowUIAnimalEvent.RaiseEvent(currentCage.GetFoodType());
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cage"))
        {
            currentCage = other.GetComponent<ICage>();
            if(currentCage != null)
            {
                UnShowEvent.RaiseEvent();
            }
        }
    }
}
