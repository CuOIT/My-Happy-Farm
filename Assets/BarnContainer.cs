using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarnContainer : MonoBehaviour
{
    [SerializeField] Button barnBtn;
    [SerializeField] Button backpackBtn;
    [SerializeField] GameObject barn;
    [SerializeField] GameObject backpack;
    [SerializeField] GameObject container;



    [SerializeField] Color32 color;
    [Button]
    public void ShowBarn()
    {
        container.SetActive(true);
        barnBtn.GetComponent<Image>().color = color;
        backpackBtn.GetComponent<Image>().color = Color.white;
        backpackBtn.transform.SetSiblingIndex(0);
        barn.SetActive(true);
        backpack.SetActive(false);
    }

    [Button]
    public void Close()
    {
        container.SetActive(false);
    }
    public void ShowBackpack()
    {
        barnBtn.GetComponent<Image>().color = Color.white;
        backpackBtn.GetComponent<Image>().color = color;
        barnBtn.transform.SetSiblingIndex(0);
        barn.SetActive(false);
        backpack.SetActive(true);
    }
}
