using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [SerializeField] Image img;
    [SerializeField] GameObject greenBackground;
    [SerializeField] TextMeshProUGUI numTxt;


    public void Init(Sprite sprite, int num)
    {
        img.gameObject.SetActive(true);
        img.sprite = sprite;
        numTxt.SetText(num.ToString());
        Highlight(false);
    }
    public void ResetThis()
    {
        img.gameObject.SetActive(false);
        numTxt.SetText("");
        Highlight(false);
    }

    public void Highlight(bool highlight)
    {
        greenBackground.SetActive(highlight);
    }
    public void AddListener(UnityAction action)
    {
        GetComponent<Button>().onClick.AddListener(action);
    }
}
