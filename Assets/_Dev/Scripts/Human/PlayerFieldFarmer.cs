using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFieldFarmer : FieldFarmer
{
    [SerializeField] FieldUI _fieldUI;

    public void Hire()
    {
        currentField.Hire();
    }
    public int GetFieldBotCost()
    {
        return currentField.GetBotCost();
    }
    public void SetPlantType(FarmProductType type)
    {
        currentField.SetNextCrop(type);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Field"))
        {
            currentField = other.GetComponent<IField>();
            if (currentField != null)
            {
                _fieldUI.InitBotStatus(currentField.GetBotCost());
                _fieldUI.ShowAction();
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Field"))
        {
            currentField = other.GetComponent<IField>();
            if (currentField != null)
            {
                _fieldUI.UnShow();
            }
        }
    }
}
