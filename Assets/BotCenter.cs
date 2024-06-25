using _Template.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotCenter : MonoBehaviour
{
    [SerializeField] List<IntData> botCosts;
    [SerializeField] List<GameObject> bots;

    public void Awake()
    {
        for(int  i = 0; i < bots.Count; i++)
        {
            if (botCosts[i].Value > 0) bots[i].SetActive(false);
            else bots[i].SetActive(true);
        }
    }
    public void HireBot(int index)
    {
        bots[index].SetActive(true);
    }

    [SerializeField] SimpleEvent showBotEvent;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            showBotEvent.RaiseEvent();
        }
    }
}
