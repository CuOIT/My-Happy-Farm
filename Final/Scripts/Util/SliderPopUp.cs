using DG.Tweening;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderPopUp : MonoBehaviour
{
    [SerializeField] Image img;
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI maxNumTxt;
    [SerializeField] TextMeshProUGUI numTxt;
    private int curNum;
    private int maxNum;
    [SerializeField] Button cancelBtn;
    [SerializeField] Button confirmBtn;
    int price;


    private TaskCompletionSource<int> taskCompletionSource;

    public void Awake()
    {
        confirmBtn.onClick.AddListener(Confirm);
        cancelBtn.onClick.AddListener(Cancel);
    }
    public async Task<int> ShowSliderPopupAsync(ProductInfo product, int num)
    {

        gameObject.SetActive(true);

        FarmProductType type = product.type;
        maxNum = num;
        img.sprite = product.sprite;
        maxNumTxt.SetText(maxNum.ToString());
        slider.value = 1;
        SetNum(maxNum);
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.5f).SetEase(Ease.OutBack);
        // Initialize the TaskCompletionSource
        taskCompletionSource = new TaskCompletionSource<int>();

        // Await the task
        int result = await taskCompletionSource.Task;

        // Hide the popup panel after getting the result
        transform.DOScale(0, 0.2f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
        return result;
    }

    public void OnValueChange()
    {
        SetNum((int)(maxNum * slider.value));
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
    private void Confirm()
    {
        // Set the result of the task when the button is clicked
        if (taskCompletionSource != null)
        {
            taskCompletionSource.SetResult((int)(slider.value * maxNum));
        }
    }
    private void Cancel()
    {
        if (taskCompletionSource != null)
        {
            taskCompletionSource.SetResult(0);
        }
    }

    public virtual void SetNum(int num)
    {
        curNum = num;
        numTxt.SetText(num.ToString());
    }
}
