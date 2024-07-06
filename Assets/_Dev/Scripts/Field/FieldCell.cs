using Assets._Dev.SO._CustomEvent;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class FieldCell : MonoBehaviour
{

    public const int COLLECT = -1; 
    public const int NONE = 0;
    public const int GROW = 1;
    public const int WATER = 2;
    private int id;
    private FarmProductType         type;
    private int                     _plantDuration;
    private FieldSquare             fieldSqr;
    [SerializeField] int            _state;
    [SerializeField] GameObject     _waterField;
    [SerializeField] CellUI         _cellUI;
    private PlantSize               _plantSize;

    [SerializeField]List<PlantInfo> plantInfos;

    public DateTime start;
    [Serializable]
    private struct PlantInfo
    {
        public FarmProductType type;
        public PlantSize plantSize;
    }
    [SerializeField] GrowTime growTime;
    [SerializeField] ParticleSystem part;
    public void InitPlantType(FarmProductType type)
    {
        this.type = type;
        foreach(var plantInfo in plantInfos)
        {
            if (plantInfo.type == type)
            {
                plantInfo.plantSize.gameObject.SetActive(true);
                _plantSize = plantInfo.plantSize;
                _plantDuration=growTime.GetPlantDurationInSec(type);
            }
            else
            {
                plantInfo.plantSize.gameObject.SetActive(false);
            }
        }
    }

    const string FORMAT = "yyyy/MM/dd HH:mm:ss";
    public void InitWaterTime(string time)
    {
        try
        {
            start = DateTime.ParseExact(time, FORMAT, CultureInfo.InvariantCulture);
        }
        catch
        {
            start = DateTime.MinValue;
        }
    }
    public void SetId(FieldSquare sqr,int id)
    {
        fieldSqr = sqr;
        this.id=id;
    }

    public void SetState(int state)
    {
        if (type == FarmProductType.NONE)
        {
            _state = NONE;
        }
        if (!_plantSize) return;
        _state=state;
        switch (_state)
        {
            case NONE:
                _plantSize.UnShow();
                _cellUI.UnShow();
                UnWater();
                break;
            case GROW:
                _plantSize.transform.localRotation = RandomRotate();
                _plantSize.ShowSeed();
                _cellUI.OnWater();
                UnWater();
                break;
            case WATER:
                _plantSize.ShowSmallPlant();
                _cellUI.OnTime();
                Water();
                break;
            case COLLECT:
                _cellUI.OnCollect();
                _plantSize.ShowMedPlant();
                UnWater();
                break;
            
        }
        fieldSqr?.UpdateState(id, _state);
    }
    public void SetWaterTime(DateTime dateTime)
    {
        start = dateTime;
        fieldSqr.UpdateWaterTime(id,dateTime.ToString(FORMAT));
    }
    
    public void InitState(int num)
    {
        SetState(num);
    }
    private Quaternion RandomRotate()
    {
        float X = Random.Range(0,10);
        float Y = Random.Range(0,360);
        float Z = Random.Range(0,10);
        return Quaternion.Euler(X, Y, Z);
    }

    public int GetState()
    {
        return (int)_state;
    }

    [ContextMenu("GROW")]
    public void GrowPlant() {
        if (_state != NONE) return;
        part.Play();
        SetWaterTime(DateTime.Now);
        SetState(GROW);
    }

    [ContextMenu("WATER")]
    public void WaterPlant()
    {
        if (_state != GROW) return;
        SetState(WATER);
    }

    void GrowUp()
    {
        SetState(COLLECT);
    }
    [ContextMenu("COLLECT")]
    public void CollectPlant(IHarvest farmer)
    {
        if(_state != COLLECT) return;
        SetState(NONE);
        int num = 1;
        GameObject product= GameManager.Instance.pooler.SpawnFromPool(type.ToString(), transform.position, Quaternion.identity);
        ProductNum productNum  = new ProductNum(type, num);
        farmer?.Harvest(product,productNum);
    }
    void Water()
    {
        _waterField.SetActive(true);
    }
    void UnWater()
    {
        _waterField.SetActive(false);
    }

    void Update()
    {
        if (_state == WATER)
        {
            TimeSpan span = DateTime.Now - start;
            long totalSeconds = (long)span.TotalSeconds;
            long remainTime =  _plantDuration - totalSeconds;
            _cellUI.SetTime(remainTime);
            if (remainTime < 0)
            {
                GrowUp();
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WATERZONE"))
        {
            WaterPlant();
        }
        else if (other.CompareTag("GROWZONE"))
        {
            GrowPlant();
        }
        else if (other.CompareTag("COLLECTZONE")) {
            IHarvest farmer = other.GetComponentInParent<IHarvest>();
            CollectPlant(farmer);
        };
    }

   
}
