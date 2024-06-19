using Assets._Dev.SO._CustomEvent;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class FieldBotController : FieldFarmer
{
    [SerializeField] FieldController fieldController;
    private BotMovement botMovement;
    [SerializeField] private float checkInterval; // Time in seconds between each check

    private List<FieldCell> actionQueue = new List<FieldCell>();
    private bool isIdle = false;
    [SerializeField] DateTimeData lastTime;
    [SerializeField] int performPerHour;
    [SerializeField] int maxContain;
    [SerializeField] FarmProductType defaultType;
    //[SerializeField] FieldFarmer farmer;
    [SerializeField] BarnCollector barn;
    [SerializeField] GameObject Info;
    [SerializeField] TextMeshProUGUI capacity;
    private void Start()
    {
        botMovement = GetComponent<BotMovement>();
        SetCurrentField(fieldController);
        TimeSpan timeSpan = DateTime.Now - lastTime.Value;
        long hour = (long)timeSpan.TotalHours;
        long numOfProduct = performPerHour * hour;
        numOfProduct = (long)Mathf.Clamp(numOfProduct, 0, maxContain);   
        if (defaultType != FarmProductType.NONE)
        {
            Harvest(null,new ProductNum(defaultType,(int)numOfProduct));
        }
        StartCoroutine(FieldCheckRoutine());
    }
    private IEnumerator FieldCheckRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkInterval);
            yield return StartCoroutine(TakeAction());
        }
    }

    private IEnumerator TakeAction()
    {
        if(_productData.GetCapacity()>maxContain)
        {
            yield return StartCoroutine(GoToBarn());
        }
        EnqueueActionsBasedOnFieldState();
        if (actionQueue.Count == 0)
        {
            if (!isIdle)
            {
                Idle();
            }
            yield break;
        }
        FieldCell nextCell = actionQueue[0];
        actionQueue.RemoveAt(0);
        yield return StartCoroutine(MoveAndAct(nextCell)); 
    }

    private IEnumerator GoToBarn()
    {
        botMovement.MoveTo(barn.transform.position);
        while(!botMovement.IsAtDestination())
            yield return null;
        var copy = new Dictionary<FarmProductType,int>(_productData.Value);
        foreach (var item in copy)
        {
            ProductNum productNum = new ProductNum(item.Key, item.Value);
            barn.AddProduct(productNum);
            _productData.Consume(productNum);
        }
    }
    private void Idle()
    {
        isIdle = true;
        _animator.SetBool("walk", false);

    }

    private void EnqueueActionsBasedOnFieldState()
    {
        actionQueue.Clear(); // Clear previous actions to avoid duplicates
        actionQueue = fieldController.FieldCells
            .Where(cell => cell.GetState() != FieldCell.WATER) // Filter out empty cells
            .OrderBy(cell => cell.GetState()) // Sort by priority
            .ThenBy(cell => GetDistance(cell.transform))
            .ToList();
    }

    private float GetDistance(Transform target)
    {
        return Vector3.Distance(transform.position, target.position);   
    }

    private IEnumerator MoveAndAct(FieldCell cell)
    {
        _animator.SetBool("walk",true);
        botMovement.MoveTo(cell.transform.position);
        int state = cell.GetState();
        while (!botMovement.IsAtDestination())
        {
            yield return null;
        }

        TakeAction(state);
    }
    private void TakeAction(int state)
    {
        switch (state)
        {
            case FieldCell.NONE:
                GrowPlant(defaultType);
                break;
            case FieldCell.GROW:
                WaterPlant();
                break;
            case FieldCell.COLLECT:
                CollectPlant();
                lastTime.Value = DateTime.Now;
                break;
        }
    }
    public void SetPlant(FarmProductType type)
    {
        defaultType = type;
    }

    public void OnTriggerEnter(Collider other)
    {
        
    }
}

