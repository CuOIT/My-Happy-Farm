using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public interface IFieldUI 
{
    void InitType(FarmProductType type);

    void SetType(FarmProductType type);
    /*void ShowGrowAction();*/


    void ShowAction();
    //void ShowPlantSeedAction();

    /*void ShowWaterAction();

    void ShowCollectAction();*/

    void UnShow();
    
}
