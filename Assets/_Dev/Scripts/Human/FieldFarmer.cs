using Assets._Dev.Scripts;
using Assets._Dev.SO._CustomEvent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldFarmer : MonoBehaviour,IHarvest
{
    protected Animator _animator;
    [SerializeField] protected ProductData _productData;
    [SerializeField] protected FarmZoneController _zoneController;
    [SerializeField] protected ProductNumEvent _collectProductEvent;
    [SerializeField] protected GameObjectEvent _collectProductGOEvent;

    [SerializeField]protected FieldController currentField;
    public void Awake()
    {
        Init();
    }
    public virtual void Init()
    {
        _animator = GetComponent<Animator>();

    }
    public void GrowPlant(FarmProductType type = FarmProductType.NONE)
    {
        if(type != FarmProductType.NONE && currentField.GetPlantType()==FarmProductType.NONE) currentField.SetPlantType(type);
        EnterField();
        _zoneController.TurnOnGrowZone();
        _animator.SetTrigger("grow");
    }
    public void SetCurrentField(FieldController field)
    {
        currentField= field;
    }
    public void WaterPlant()
    {
        EnterField();
        _animator.SetTrigger("water");
        _zoneController.TurnOnWaterZone();
    }
    public void CollectPlant()
    {
        EnterField();
        _animator.SetTrigger("collect");
        _zoneController.TurnOnCollectZone();
    }
    public void Harvest(GameObject product,ProductNum productNum)
    {
        _productData.Add(productNum);
        _collectProductEvent?.RaiseEvent(productNum);
        if(product!=null)
            _collectProductGOEvent?.RaiseEvent(product);
    }
    public void Consume(ProductNum productNum)
    {
        _productData.Consume(productNum);
    }
    void EnterField()
    {
        _animator.SetLayerWeight(_animator.GetLayerIndex("Leg"), 1);
    }
    public void LeaveField()
    {
        _animator.SetLayerWeight(_animator.GetLayerIndex("Leg"), 0);
        _animator.SetTrigger("leaveField");
        _zoneController.TurnOffAll();
    }
    public void SetFieldPlantType(FarmProductType type)
    {
        currentField.SetPlantType(type);
    }
    public FarmProductType GetFieldType()
    {
        return currentField.GetPlantType();
    }
}

public interface IHarvest
{
    void Harvest(GameObject product, ProductNum productNum);
}

public interface IFieldFarmer
{
    void GrowPlant();
    void WaterPlant();
    void CollectPlant();
}
