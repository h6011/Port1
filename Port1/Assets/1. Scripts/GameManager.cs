using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private float gameTimer = 0f;
    private float meter = 0f;
    private int score = 0;
    private int coin = 0;

    private bool GameStarted = false;
    public bool GamePaused = false;

    public float GameTimer => gameTimer;
    public float Meter => meter;
    public int Score => score;
    public int Coin => coin;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        GameStarted = true;
    }

    private void Update()
    {
        //gameExitKeyAction();
        timerAction();
    }

    private void timerAction()
    {
        if (GameStarted)
        {
            gameTimer += Time.deltaTime;
        }
    }

    public void PauseGame()
    {
        GamePaused = true;
        Time.timeScale = 0f;
    }

    public void UnpauseGame()
    {
        GamePaused = false;
        Time.timeScale = 1f;
    }



    public void AddListenerToBtn(Button _btn, UnityEngine.Events.UnityAction action)
    {
        _btn.onClick.RemoveAllListeners();
        _btn.onClick.AddListener(action);
    }



    private void gameExitKeyAction()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameExit();
        }
    }

    public void GameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }




}
