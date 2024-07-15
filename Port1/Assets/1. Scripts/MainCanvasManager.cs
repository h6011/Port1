using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] Button exitGameBtn;
 
    private void Start()
    {
        gameManager = GameManager.Instance;
        playerController = PlayerController.Instance;

        SetVisibleUIFromName("InGame", true);
        SetVisibleUIFromName("Pause", false);
        ClickActions();
    }

    private void Update()
    {
        displayGameUIAction();
        pauseUIAction();
    }

    private void ClickActions()
    {
        resumeBtn.onClick.RemoveAllListeners();
        resumeBtn.onClick.AddListener(() => {
            ToggleVisibleUIFromName("Pause");
        });

        exitGameBtn.onClick.RemoveAllListeners();
        exitGameBtn.onClick.AddListener(() => {
            gameManager.GameExit();
        });

    }

    private void pauseUIAction()
    {
        KeyCode pauseKeyCode = playerController.GetKeySettings().pauseKey;
        if (Input.GetKeyDown(pauseKeyCode))
        {
            ToggleVisibleUIFromName("Pause");
        }
    }


    private void displayGameUIAction()
    {
        coinText.text = gameManager.Coin.ToString();
        ScoreText.text = gameManager.Score.ToString();
        MeterText.text = gameManager.Meter.ToString();
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
