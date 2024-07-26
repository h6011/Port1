using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private EnemyManager enemyManager;
    public EnemyManager EnemyManager { set => enemyManager = value; }
    private MeteorManager meteorManager;
    public MeteorManager MeteorManager { set => meteorManager = value; }

    [Header("Current Game Stat")]
    [SerializeField] private float gameTimer = 0f;
    [SerializeField] private float meter = 0f;
    [SerializeField] private int score = 0;
    [SerializeField] private int money = 0;

    [Header("Bool")]
    public bool GameStarted = false;
    public bool GamePaused = false;
    public bool IsGameOver = false;
    public bool isBossStage = false;

    
    public float GameTimer => gameTimer;
    public float Meter => meter;
    public int Score => score;
    public int Money => money;

    [Header("Meter")]
    [SerializeField] private float spawnedMeter;
    [SerializeField] private float spawnPerMeter = 1;

    [SerializeField] private int bossSpawnMeter = 15;

    [Header("Meteor")]
    [SerializeField] float minMeteorMeter = 10;
    [SerializeField] float defaultMeteorTime = 4f;
    [SerializeField] float increaseMeteorAmountPerMeter = 0.1f;
    //[SerializeField] float maxMeteorTime = 0;

    private float meteorTimer = 0f;




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
        if (spawnedMeter < meter && !IsGameOver)
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

        int picked = Random.Range(-2, 3);

        for (int iNum = -2; iNum <= 2; iNum++)
        {
            enemyManager.SpawnEnemy(eEnemyType.BasicEnemy, iNum == picked, new Vector3(iNum * gap, 0, 0));
        }

    }

    private void spawnBoss()
    {
        Debug.Log("spawnBoss");
        isBossStage = true;
        enemyManager.SpawnEnemy(eEnemyType.Boss, false);
    }

    public void GetScoreByEnemyType(eEnemyType enemyType)
    {
        if (enemyType == eEnemyType.BasicEnemy)
        {
            score += 200;
        }
        else if (enemyType == eEnemyType.Boss)
        {
            score += 2000;
        }
    }


    public void GameStart()
    {
        IsGameOver = false;
        GameStarted = true;
        gameTimer = 0;
        meter = 0;
        score = 0;
        money = 0;
        spawnedMeter = 0;

        isBossStage = false;
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
        if (GameStarted && !IsGameOver)
        {

            if (isBossStage == false)
            {
                gameTimer += Time.deltaTime;

                meter += Time.deltaTime / 2f;
            }

            meteorTimer += Time.deltaTime;

            if (meter >= minMeteorMeter)
            {
                float _meter = meter - minMeteorMeter;
                float currentTime = defaultMeteorTime *  (1 / (increaseMeteorAmountPerMeter * _meter)) ;

                if (meteorTimer >= currentTime)
                {
                    meteorTimer = 0f;
                    Debug.Log("currentTime: " + currentTime);
                    meteorManager.SpawnMeteor();

                }
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

    public void RetryGame()
    {
        GameStart();
        SceneManager.LoadScene("Game");
    }


    //public void AddListenerToBtn(Button _btn, UnityEngine.Events.UnityAction action)
    //{
    //    _btn.onClick.RemoveAllListeners();
    //    _btn.onClick.AddListener(action);
    //}



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
