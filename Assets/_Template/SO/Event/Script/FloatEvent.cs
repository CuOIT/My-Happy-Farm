using _Template.Event;
using UnityEngine;

[CreateAssetMenu(fileName ="NewFloatEvent",menuName ="Event/Float")]
public class FloatEvent : BaseSOEvent<float>
{
}

public class FloatEventListener: BaseEventListener<float>
{

}
