using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IField 
{
    int GetBotCost();
    FarmProductType GetPlantType();
    void Hire();
    void SetNextCrop(FarmProductType type);
    void SetPlantType(FarmProductType plantType);
}
