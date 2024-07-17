using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private EnemyManager enemyManager;
    public EnemyManager EnemyManager { set => enemyManager = value; }


    [Header("Game Static Stat")]



    [Header("Game Stats")]
    [SerializeField] private float gameTimer = 0f;
    [SerializeField] private float meter = 0f;
    [SerializeField] private int score = 0;
    [SerializeField] private int money = 0;

    public bool GameStarted = false;
    public bool GamePaused = false;
    public bool isBossStage = false;

    public float GameTimer => gameTimer;
    public float Meter => meter;
    public int Score => score;
    public int Money => money;

    [SerializeField] private float spawnedMeter;
    [SerializeField] private float spawnPerMeter = 1;

    [SerializeField] private int bossSpawnMeter = 15;

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
        GameStarted = false;
        //enemyManager = EnemyManager.Instance;
    }

    private void Update()
    {
        //checkEnemyMng();
        timerAction();
        waveCheckAction();
    }

    private void checkEnemyMng()
    {
        if (enemyManager == null)
        {
            enemyManager = EnemyManager.Instance;
        }
    }

    private void waveCheckAction()
    {
        if (spawnedMeter < meter)
        {
            float between = meter - spawnedMeter;
            if (between >= spawnPerMeter)
            {
                spawnedMeter = meter;
                if ((int)meter % bossSpawnMeter == 0)
                {
                    spawnBoss();
                }
                else
                {
                    spawnWave();
                }
            }
        }
    }


    private void spawnWave()
    {

        float gap = 1.1f;

        for (int iNum = -2; iNum <= 2; iNum++)
        {
            enemyManager.SpawnEnemy(eEnemyType.BasicEnemy, new Vector3(iNum * gap, 0, 0));
        }

    }

    private void spawnBoss()
    {
        Debug.Log("spawnBoss");
        isBossStage = true;
        enemyManager.SpawnEnemy(eEnemyType.Boss);
    }
    


    public void GameStart()
    {
        GameStarted = true;
        gameTimer = 0;
        meter = 0;
        score = 0;
        money = 0;
    }

    public void GameEnd()
    {
        GameStarted = false;
        gameTimer = 0;
    }


    public void GetMoney(Transform moneyTrs, eMoneyType _moneyType)
    {
        if (moneyTrs == null) return;

        money += (int)_moneyType;

        Destroy(moneyTrs.gameObject);
    }

    private void timerAction()
    {
        if (GameStarted)
        {

            if (isBossStage == false)
            {
                gameTimer += Time.deltaTime;

                meter += Time.deltaTime / 2f;
            }

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

    public void BackToLobby()
    {
        SceneManager.LoadScene("Lobby");
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
