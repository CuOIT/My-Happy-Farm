using _Template.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlantTypeEvent", menuName = "Event/PlantTypeEvent")]
public class PlantTypeAndNumEvent : BaseEvent<FarmProductType,int>
{
}

#if UNITY_EDITOR
[CustomEditor(typeof(BaseEvent<>), true)]
public class PlantTypeAndNumEventEditor : Editor
{
    PlantTypeAndNumEvent soEvent;

    public void OnEnable()
    {
        soEvent = target as PlantTypeAndNumEvent;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("RaiseEvent"))
        {
            soEvent.RaiseTestEvent();
        }
    }
}


#endif
