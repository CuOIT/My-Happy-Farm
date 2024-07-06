using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IField 
{
    FarmProductType GetPlantType();
    void SetPlantType(FarmProductType plantType);
}
