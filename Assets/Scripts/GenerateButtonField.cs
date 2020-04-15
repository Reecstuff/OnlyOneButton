using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GenerateButtonField : MonoBehaviour
{
    [SerializeField]
    GameObject ButtonPrefab;

    [SerializeField]
    Button nextButton;

    [SerializeField]
    TextMeshProUGUI updateTextfield;

    [SerializeField]
    int rows = 10;

    [SerializeField]
    int columns = 10;

    [SerializeField]
    float timeUntilStart = 3;

    [SerializeField]
    Timer timer;

    [SerializeField]
    LevelChanger levelChanger;


    float width;
    float height;

    List<Button> buttonList; 
    Button randomButton;
    Color prefabColor;
    int levelStage = 0;
    float originTime;

    private void Start()
    {
        buttonList = new List<Button>();
        originTime = timeUntilStart;
        width = ButtonPrefab.GetComponent<RectTransform>().rect.width + 2;
        height = ButtonPrefab.GetComponent<RectTransform>().rect.height + 2;
        prefabColor = ButtonPrefab.GetComponent<Image>().color;
        nextButton.onClick.AddListener(() => StartGame());
        GenerateField();
        updateTextfield.gameObject.SetActive(false);
        
        
        levelChanger.setLevel(1);
    }


    public void StartGame()
    {
        SelectRandomButton();
        timer.StartTimer(timeUntilStart, this);
        nextButton.gameObject.SetActive(false);
        updateTextfield.gameObject.SetActive(false);
    }

    void GenerateField()
    {
        float screenWidthHalf = Screen.width * 0.5f;
        float screenHeightHalf = Screen.height * 0.5f;
        Vector3 bufferVector = Camera.main.WorldToViewportPoint(new Vector3(screenWidthHalf, screenHeightHalf, 0));
        float startX = bufferVector.x - (columns * width / 2);
        float startY = (rows * height / 2) - bufferVector.y;

        float currentX = startX;
        float currentY = startY;


        for (int i = 0; i < columns; i++)
        {

            for (int l = 0; l < rows; l++)
            {
                SetupObject(currentX, currentY);
                currentY -= height;

            }
            currentX += width;
            currentY = startY;
        }
    }

    void SetupObject(float currentX, float currentY)
    {

        GameObject bufferObject = Instantiate(ButtonPrefab);
        RectTransform bufferTransform = bufferObject.GetComponent<RectTransform>();

        bufferObject.transform.SetParent(transform, false);
        bufferTransform.anchoredPosition = new Vector3(currentX, currentY, 0);
        bufferTransform.sizeDelta = new Vector2(width, height);
        bufferObject.GetComponent<Button>().onClick.AddListener(() => Lost());

        buttonList.Add(bufferObject.GetComponent<Button>());
    }

    void SelectRandomButton()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (randomButton)
            randomButton.GetComponent<Image>().color = prefabColor;

        randomButton = buttonList[Random.Range(0, buttonList.Count - 1)];
        randomButton.GetComponent<Image>().color = Color.magenta;

        randomButton.onClick.RemoveAllListeners();
        randomButton.onClick.AddListener(() => Win());
    }

    public void DelselectRandomButton()
    {
        randomButton.GetComponent<Image>().color = prefabColor;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Win()
    {
        levelChanger.setLevel(1);

        nextButton.gameObject.SetActive(true);
        updateTextfield.gameObject.SetActive(true);

        updateTextfield.text = "WIN";
        updateTextfield.color = Color.green;

        if (levelChanger.currentLevel % 5 == 0 && timeUntilStart >= 0.1f)
        {
            levelStage++;
            timeUntilStart /= (1 + levelStage * 0.1f);
        }
        timer.setText(timeUntilStart);
        randomButton.onClick.RemoveAllListeners();
        randomButton.onClick.AddListener(() => Lost());
        randomButton = null;
    }

    public void Lost()
    {
        if(randomButton)
        {
            randomButton.GetComponent<Image>().color = Color.red;
            nextButton.gameObject.SetActive(true);
            updateTextfield.gameObject.SetActive(true);

            updateTextfield.text = "LOSS";
            updateTextfield.color = Color.red;

            if (levelChanger.currentLevel % 5 == 0)
            {
                timeUntilStart *= (1 + levelStage * 0.1f);
                levelStage--;
            }
            levelChanger.setLevel(-1);
            timer.setText(timeUntilStart);

            randomButton.onClick.RemoveAllListeners();
            randomButton.onClick.AddListener(() => Lost());
        }
    }
}
