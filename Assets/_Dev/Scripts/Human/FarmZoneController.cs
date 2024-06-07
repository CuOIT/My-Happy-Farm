using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmZoneController : MonoBehaviour
{
    [SerializeField] GameObject _growZone;
    [SerializeField] GameObject _waterZone;
    [SerializeField] GameObject _collectZone;

    [SerializeField] GameObject _seedPack;
    [SerializeField] GameObject _seedBucket;
    [SerializeField] GameObject _waterBucket;
    [SerializeField] GameObject _hammer;

    public void Awake()
    {
        TurnOffAll();
    }
    public void TurnOnGrowZone()
    {
        TurnOffAll();
        _seedBucket.SetActive(true);
        _seedPack.SetActive(true);
        _growZone.SetActive(true);
    }
    public void TurnOnWaterZone()
    {
        TurnOffAll();
        _waterBucket.SetActive(true);
        _waterZone.SetActive(true);
    }
    public void TurnOnCollectZone()
    {
        TurnOffAll();
        _hammer.SetActive(true);    
        _collectZone.SetActive(true);
    }

    public void TurnOffAll()
    {
        _growZone.SetActive(false);
        _waterZone.SetActive(false);
        _collectZone.SetActive(false);
        _seedBucket.SetActive(false);
        _seedPack.SetActive(false);
        _waterBucket.SetActive(false);
        _hammer.SetActive(false);
    }
}

