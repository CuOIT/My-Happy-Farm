using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXFarmer : MonoBehaviour
{
    [SerializeField] SFXSupport seed;
    [SerializeField] SFXSupport water;
    [SerializeField] SFXSupport collect;

    public void SeedSFX()
    {
        seed.PlaySound();
    }

    public void WaterSFX()
    {
        water.PlaySound();
    }

    public void CollectSFX()
    {
        collect.PlaySound();
    }
}
