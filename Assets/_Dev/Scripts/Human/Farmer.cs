using Assets._Dev.Scripts;
using Assets._Dev.SO._CustomEvent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : MonoBehaviour,IHarvest
{
    [SerializeField] protected Animator _animator;
    [SerializeField] protected float _playerSpeed;
    [SerializeField] protected FarmZoneController _zoneController;
    [SerializeField] protected ProductNumEvent _collectProductEvent;
    [SerializeField] protected GameObjectEvent _collectProductGOEvent;
    [SerializeField] protected float _decreaseProductEvent;
    [SerializeField] protected GameObject collectorGO;
    [SerializeField] ICollector collector;
    public void Start()
    {
        Init();
    }
    public virtual void Init()
    {
        _animator = GetComponent<Animator>();
        collector=collectorGO.GetComponent<ICollector>();
    }

    public void GrowPlant()
    {
        EnterField();
        _zoneController.TurnOnGrowZone();
        _animator.SetTrigger("grow");
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

    private float delay=2;
    protected virtual IEnumerator AsyncHarvest(GameObject product,ProductNum productNum)
    {
        yield return new WaitForSeconds(delay);
        collector?.Collect(productNum);
        _collectProductEvent.RaiseEvent(productNum);
        _collectProductGOEvent.RaiseEvent(product);
    }
    public void Harvest(GameObject product,ProductNum productNum)
    {
        StartCoroutine(AsyncHarvest(product,productNum));
    }
    
    public bool HaveProduct(ProductNum productNum)
    {
        return collector.HaveProduct(productNum);
    }

    public void Consume(ProductNum productNum)
    {
        collector.Consume(productNum);
    }
    public void EnterField()
    {
        _animator.SetLayerWeight(_animator.GetLayerIndex("Leg"), 1);
    }
    public void LeaveField()
    {
        _animator.SetLayerWeight(_animator.GetLayerIndex("Leg"), 0);
        _animator.SetTrigger("leaveField");
        _zoneController.TurnOffAll();
    }
}

public interface IHarvest
{
    public void Consume(ProductNum productNum);
    bool HaveProduct(ProductNum productNum);
    void Harvest(GameObject product,ProductNum productNum);

}
