using _Template.Script.UI;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(IncreaseNumberText))]
public class ItemNumUI : MonoBehaviour
{
    [SerializeField]Image image;
    Sprite sprite;
    TextMeshProUGUI itemNum;
    IncreaseNumberText textSupport;
    Animation anim;
    [SerializeField]Transform target;
    [SerializeField]GameObject container;
    public void Awake()
    {
        sprite = image.sprite;
        itemNum = GetComponentInChildren<TextMeshProUGUI>();    
        textSupport = GetComponent<IncreaseNumberText>();
        textSupport.Initm_TextProUGUI(itemNum);
        anim = GetComponent<Animation>();
    }

    public void OnEnable()
    {
        if(anim != null)
        {
            anim.Play("slideLeftOnEnableAnimUI");
        }
    }
    [Button]
    public void SetNum(int num)
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
            StartCoroutine(ShowThisUI(num));
        }
        else
        {
            textSupport.SetNum(num);
        }
    }

    [Button]
    public void SetNumToZero()
    {
        textSupport.SetNum(0);
        StartCoroutine(Hide());
    }
    public void InitNum(int num)
    {
        itemNum.SetText(num.ToString());
        gameObject.SetActive(num>0);
    }

    [Button]
    public IEnumerator ShowThisUI(int num)
    {
        yield return new WaitForSeconds(0.5f);
        textSupport.SetNum(num);
    }

    public IEnumerator Hide()
    {
        yield return new WaitForSeconds(0.2f);
        anim.Play("slideRightOnDisableAnimUI");
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
    public Sprite GetSprite()
    {
        return sprite;
    }
    public Transform TargetPos()
    {
        return target;
    }
}
