using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Cage
{

public abstract class CageState 
{
    private AnimalCage cage;
    public void InitCage(AnimalCage cage)
    {
        this.cage = cage;
    }
    public abstract void EnterState();

    public abstract void UpdateState();

    public abstract void ExitState();
}

}