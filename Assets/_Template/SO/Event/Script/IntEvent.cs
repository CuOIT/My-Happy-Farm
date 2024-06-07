using _Template.Event;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName ="NewIntEvent",menuName ="Event/IntEvent")]
public class IntEvent : BaseSOEvent<int>
{
}
#if UNITY_EDITOR
[CustomEditor(typeof(IntEvent), true)]
public class IntEventEditor : Editor
{
    IntEvent soEvent;

    public void OnEnable()
    {
        soEvent = target as IntEvent;
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

