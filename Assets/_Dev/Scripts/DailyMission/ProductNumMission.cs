using _Template.Event;
using Assets._Dev.SO._CustomEvent;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="ProductNumMission",menuName ="Mission/ProductNum")]
public class ProductNumMission : Mission,IEventListener<ProductNum>

{

    [SerializeField] ProductNumEvent productEvent;
    [SerializeField] ProductNum target;

    public void OnEnable()
    {
        productEvent.AddListener(this);
        targetProgress = target.num;
    }
    public void OnEventRaise(ProductNum productNum)
        {
            if(target.type==productNum.type)
        {
            IncrementProgress(productNum.num);
        }
        }
}
