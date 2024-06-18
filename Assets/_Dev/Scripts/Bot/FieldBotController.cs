using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FieldBotController : MonoBehaviour
{
    private FieldController fieldController;
    private BotMovement botMovement;
    [SerializeField] private float checkInterval = 5f; // Time in seconds between each check

    private List<FieldCell> actionQueue = new List<FieldCell>();
    private bool isIdle = false;

    private void Awake()
    {
        fieldController = GetComponent<FieldController>();
        botMovement = GetComponent<BotMovement>();
    }

    private void Start()
    {
        StartCoroutine(FieldCheckRoutine());
    }

    private IEnumerator FieldCheckRoutine()
    {
        while (true)
        {
            yield return StartCoroutine(TakeAction());
        }
    }

    private IEnumerator TakeAction()
    {
        EnqueueActionsBasedOnFieldState();
        if (actionQueue.Count == 0)
        {
            if (!isIdle)
            {
                isIdle = true;
                Idle();
            yield break;
            }
        }
        FieldCell nextCell = actionQueue[0];
        actionQueue.RemoveAt(0);
        yield return StartCoroutine(MoveAndAct(nextCell)); 
    }

    private void Idle()
    {
        Debug.Log("IDle");
    }

    private void EnqueueActionsBasedOnFieldState()
    {
        actionQueue.Clear(); // Clear previous actions to avoid duplicates
        actionQueue = fieldController.FieldCells
            .Where(cell => cell.GetState() != FieldCell.WATER) // Filter out empty cells
            .OrderBy(cell => cell.GetState()) // Sort by priority
            .OrderBy(cell => GetDistance(cell.transform))
            .ToList();
    }

    private float GetDistance(Transform target)
    {
        return Vector3.Distance(transform.position, target.position);   
    }

    private void ProcessNextAction()
    {
        if (isIdle || actionQueue.Count == 0) return;
        isIdle = false;
        FieldCell nextCell = actionQueue[0];
        actionQueue.RemoveAt(0);
        StartCoroutine(MoveAndAct(nextCell));
    }

    private IEnumerator MoveAndAct(FieldCell cell)
    {
        botMovement.MoveTo(cell.transform.position);

        while (!botMovement.IsAtDestination())
        {
            yield return null;
        }

        TakeAction(cell);
    }
    [SerializeField] Farmer farmer;
    private void TakeAction(FieldCell cell)
    {
        var state = cell.GetState();
        switch (state)
        {
            case FieldCell.NONE:
                farmer.GrowPlant();
                break;
            case FieldCell.GROW:
                farmer.WaterPlant();
                break;
            case FieldCell.COLLECT:
                farmer.CollectPlant();
                break;
        }
    }
}

