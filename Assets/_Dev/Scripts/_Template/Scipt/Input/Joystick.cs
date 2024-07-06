using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerMoveHandler
{
    private Vector2 startPoint;
    [SerializeField] public Vector3 Direction;
    private bool down;
    public int no;

    public void OnEnable()
    {
        Direction = Vector3.zero;
        down = false;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        startPoint = eventData.position;
        down = true;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (down)
        {
            Vector2 curPoint = eventData.position;
            Direction = (curPoint - startPoint).normalized;
            Direction = new Vector3(Direction.x,0,Direction.y);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Direction = Vector3.zero;
        down = false;
    }
}
