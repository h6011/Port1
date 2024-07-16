using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainCanvasManager : MonoBehaviour
{
    GameManager gameManager;
    PlayerController playerController;

    [Header("Game UI Text")]
    [SerializeField] TMP_Text coinText;
    [SerializeField] TMP_Text ScoreText;
    [SerializeField] TMP_Text MeterText;

    [Header("Pase UI Button")]
    [SerializeField] Button resumeBtn;
    [SerializeField] Button backBtn;
    [SerializeField] Button exitGameBtn;

    private void Awake()
    {
        if (Tool.isLoadMainScene == false)
        {
            SceneManager.LoadScene("Lobby");
        }
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        playerController = PlayerController.Instance;

        SetVisibleUIFromName("InGame", true);
        SetVisibleUIFromName("Pause", false);
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
        gameManager.AddListenerToBtn(resumeBtn, () => {
            gameManager.UnpauseGame();
            ToggleVisibleUIFromName("Pause");
        });

        gameManager.AddListenerToBtn(backBtn, () => {
            gameManager.BackToLobby();
        });

        gameManager.AddListenerToBtn(exitGameBtn, () => {
            gameManager.GameExit();
        });

    }

    private void pauseUIAction()
    {
        KeyCode pauseKeyCode = playerController.GetKeySettings().pauseKey;
        if (Input.GetKeyDown(pauseKeyCode))
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
        MeterText.text = string.Format("{0:N0}M", gameManager.Meter);
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
