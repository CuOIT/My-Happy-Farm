using _Template.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFieldFarmer : FieldFarmer
{
    [SerializeField] SimpleEvent showFieldUIEvent;
    [SerializeField] SimpleEvent hideFieldUIEvent;




    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Field"))
        {
            currentField = other.GetComponent<FieldController>();
            if (currentField != null)
            {
                showFieldUIEvent.RaiseEvent();
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Field"))
        {
            currentField = other.GetComponent<FieldController>();
            if (currentField != null)
            {
                hideFieldUIEvent.RaiseEvent();
            }
        }
    }
}
