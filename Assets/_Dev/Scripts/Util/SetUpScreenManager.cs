using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SetupScreenManager : MonoBehaviour
{
    public Button[] characterButtons;
    public TMP_InputField nameInputField;
    public Button playButton;
    public TextMeshProUGUI noticeTxt;
    public IntData characterNum;
    public IntData initData;
    private Button selectedButton = null;

    void Start()
    {
        // Add listener for each character button
        foreach (Button btn in characterButtons)
        {
            btn.onClick.AddListener(() => OnCharacterButtonClick(btn));
        }

        // Add listener for play button
        playButton.onClick.AddListener(OnPlayButtonClick);
    }

    void OnCharacterButtonClick(Button btn)
    {
        // Make the previously selected button white
        if (selectedButton != null)
        {
            selectedButton.image.color = Color.white;
        }

        // Make the clicked button green
        btn.image.color = Color.green;
        selectedButton = btn;
    }

    public void PickMale()
    {
        characterNum.Value = 1;
    }

    public void PickFemale()
    {
        characterNum.Value = 0;
    }
    void OnPlayButtonClick()
    {
        // Check if a character is selected
        if (selectedButton == null)
        {
            noticeTxt.text = "Pick up a character";
            return;
        }

        // Check if the name is filled correctly
        if (string.IsNullOrWhiteSpace(nameInputField.text))
        {
            noticeTxt.text = "Enter a valid name";
            return;
        }
        initData.Value = 1;
        // Change to GameScene
        SceneManager.LoadScene("GameScene");
    }
}