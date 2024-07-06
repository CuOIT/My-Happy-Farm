using _Template.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarnTrigger : MonoBehaviour
{
    [SerializeField] SimpleEvent showBarnEvent;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            showBarnEvent.RaiseEvent();
        }
    }
}
