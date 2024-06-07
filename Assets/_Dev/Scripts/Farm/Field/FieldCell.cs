using Assets._Dev.SO._CustomEvent;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FieldCell : MonoBehaviour
{
    private int id;
    private FarmProductType type;
    [SerializeField] int                   _state;

    [SerializeField] Transform      _plantRoot;
    private FieldController         _fieldController;
    PlantSize _plantSize;
    [SerializeField] List<PlantType> plantTypes;

    [Serializable]
    private struct PlantType
    {
        public FarmProductType type;
        public PlantSize plantSize;
    }
    private Dictionary<FarmProductType, PlantSize> plantSize;
    public void InitPlantType(FarmProductType type)
    {
        this.type = type;
        foreach(var plantType in plantTypes)
        {
            if (plantType.type == type)
            {
                plantType.plantSize.gameObject.SetActive(true);
                _plantSize = plantType.plantSize;
            }
            else
            {
                plantType.plantSize.gameObject.SetActive(false);
            }
        }
    }
    private FieldSquare fieldSqr;
    public void SetId(FieldSquare sqr,int id)
    {
        fieldSqr = sqr;
        this.id=id;
    }
    public void SetState(int num)
    {
        _state = num;
        fieldSqr.UpdateState(id, num);
    }
    public void InitState(int num)
    {
        _state = num;
        if(type==FarmProductType.NONE)
        {
            _state = 0;
        }
        if (!_plantSize) return;
        if (_state == 0) _plantSize.UnShow();
        else if (_state == 1) _plantSize.ShowSmallPlant();
        else if (_state == 2) _plantSize.ShowMedPlant();
    }

    public void Awake()
    {
        _fieldController = GetComponentInParent<FieldController>(); 
    }

    private Quaternion RandomRotate()
    {
        float X = Random.Range(0,10);
        float Y = Random.Range(0,360);
        float Z = Random.Range(0,10);
        return Quaternion.Euler(X, Y, Z);
    }
    // Update is called once per frame


    public int GetState()
    {
        return (int)_state;
    }


    public void IncreaseFieldNumState()
    {
        _fieldController.IncreaseState();
    }
    [ContextMenu("GROW")]
    public void GrowPlant() {
        if (_state != 0) return;
        SetState(1);
        _plantSize.ShowSmallPlant();        
        _plantSize.transform.localRotation = RandomRotate();
        IncreaseFieldNumState();
        
    }

    [ContextMenu("WATER")]
    public void WaterPlant()
    {
        if (_state != 1) return;
        SetState(2);
        _plantSize.ShowMedPlant();
        IncreaseFieldNumState();
    }

    [ContextMenu("COLLECT")]
    public void CollectPlant(IHarvest farmer)
    {
        if(_state != 2) return;
        int num = 1;
        _plantSize.UnShow();
        SetState(0);
        GameObject product= GameManager.Instance.pooler.SpawnFromPool(type.ToString(), transform.position, Quaternion.identity);
        ProductNum productNum  = new ProductNum(type, num);
        farmer.Harvest(product,productNum);
        IncreaseFieldNumState();
    }

    public void OnTriggerEnter(Collider other)
    {
        IHarvest farmer = other.GetComponentInParent<IHarvest>();
        if (farmer==null) return;
        if (other.CompareTag("WATERZONE"))
        {
            WaterPlant();
        }
        else if (other.CompareTag("GROWZONE"))
        {
            GrowPlant();
        }
        else if (other.CompareTag("COLLECTZONE")) {
            CollectPlant(farmer);
        };
    }

   
}
