using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CropMover : MonoBehaviour
{
    Tween tween;
    private void OnTriggerEnter(Collider other)
    {
        tween?.Kill();
        Debug.Log(other.name);
        Vector3 point = other.ClosestPoint(transform.position);
        Vector3 dir = point - transform.position;
        dir = new Vector3(-dir.x, dir.y, -dir.z);
        Debug.DrawRay(transform.position, dir*100,Color.red,5);
        Quaternion quater = Quaternion.LookRotation(Vector3.up,dir);
        quater = Quaternion.Slerp(Quaternion.identity,quater,0.2f);
        tween = transform.DOLocalRotateQuaternion(quater,1f);
    }
    private void OnTriggerExit(Collider other)
    {
        tween.Kill();
        tween = transform.DOLocalRotateQuaternion(Quaternion.identity,1f);
    }
}
