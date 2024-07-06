using _Template.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(fileName = "NewVector3Event", menuName = "Event/Vector3Event")]

public class Vector3Event : BaseEvent<Vector3>
{
}
#if UNITY_EDITOR
[CustomEditor(typeof(BaseEvent<>), true)]
public class Vector3EventEditor : Editor
{
    Vector3Event soEvent;

    public void OnEnable()
    {
        soEvent = target as Vector3Event;
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