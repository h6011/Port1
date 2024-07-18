using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainCanvasManager : MonoBehaviour
{
    public static MainCanvasManager Instance;

    GameManager gameManager;
    PlayerController playerController;

    [Header("Game UI Text")]
    [SerializeField] TMP_Text coinText;
    [SerializeField] TMP_Text ScoreText;
    [SerializeField] TMP_Text MeterText;

    [Header("Pase UI Button")]
    [SerializeField] Button resumeBtn;
    [SerializeField] Button exitGameBtn;
    [SerializeField] Button retryGameBtn;
    [SerializeField] Button registerBtn;

    [Header("GameOver Screen UI")]
    [SerializeField] TMP_Text scoreTitle;
    [SerializeField] TMP_Text meterTitle;
    [SerializeField] TMP_Text moneyTitle;

    [Header("InputField")]
    [SerializeField] TMP_InputField nameInputField;

    [Header("List")]
    [SerializeField] List<Button> listBackBtn;

    private void Awake()
    {
        if (Tool.isLoadMainScene == false)
        {
            SceneManager.LoadScene("Lobby");
        }

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        playerController = PlayerController.Instance;

        SetVisibleUIFromName("InGame", true);
        SetVisibleUIFromName("Pause", false);
        SetVisibleUIFromName("GameOver", false);
        ClickActions();
        gameManager.UnpauseGame();
        gameManager.GameStart();
    }

    private void Update()
    {
        displayGameUIAction();
        pauseUIAction();
    }

    private void ClickActions()
    {
        Tool.AddListenerToBtn(resumeBtn, () => {
            gameManager.UnpauseGame();
            ToggleVisibleUIFromName("Pause");
        });

        int count1 = listBackBtn.Count;
        for (int i = 0; i < count1; i++)
        {
            Button backBtn = listBackBtn[i];
            Tool.AddListenerToBtn(backBtn, () => {
                gameManager.BackToLobby();
            });
        }

        Tool.AddListenerToBtn(retryGameBtn, () => {
            gameManager.RetryGame();
        });

        Tool.AddListenerToBtn(exitGameBtn, () => {
            gameManager.GameExit();
        });

        Tool.AddListenerToBtn(registerBtn, () => {
            
        });

    }

    private void pauseUIAction()
    {
        KeyCode pauseKeyCode = playerController.GetKeySettings().pauseKey;
        if (Input.GetKeyDown(pauseKeyCode) && !gameManager.IsGameOver)
        {
            bool _bool = GetVisibleUIFromName("Pause");
            if (_bool)
            {
                _bool = false;
                gameManager.UnpauseGame();
                SetVisibleUIFromName("Pause", false);
            }
            else
            {
                _bool = true;
                gameManager.PauseGame();
                SetVisibleUIFromName("Pause", true);
            }
        }
    }


    private void displayGameUIAction()
    {
        coinText.text = gameManager.Money.ToString();
        ScoreText.text = gameManager.Score.ToString();
        MeterText.text = $"{(int)gameManager.Meter}M";
    }

    public void SetGameOverScreen()
    {
        scoreTitle.text = $"Score: {gameManager.Score}";
        meterTitle.text = $"{(int)gameManager.Meter}M";
        moneyTitle.text = $"{gameManager.Money}";
    }

    


    public bool GetVisibleUIFromName(string _name)
    {
        Transform findUI = transform.Find(_name);
        if (findUI)
        {
            return findUI.gameObject.activeSelf;
        }
        throw new System.Exception();
    }

    public void SetVisibleUIFromName(string _name, bool _boolean)
    {
        Transform findUI = transform.Find(_name);
        if (findUI)
        {
            findUI.gameObject.SetActive(_boolean);
        }
    }

    public void ToggleVisibleUIFromName(string _name)
    {
        Transform findUI = transform.Find(_name);
        if (findUI)
        {
            findUI.gameObject.SetActive(!findUI.gameObject.activeSelf);
        }
    }


}
