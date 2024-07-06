using _Template.Event;
using UnityEngine;

[CreateAssetMenu(fileName ="NewFloatEvent",menuName ="Event/Float")]
public class FloatEvent : BaseEvent<float>
{
}

public class FloatEventListener: EventListener<float>
{

}
