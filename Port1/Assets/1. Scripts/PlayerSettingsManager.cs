using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerSettings
{
    [System.Serializable]
    public class PlayerKeySettings
    {
        public KeyCode pauseKey = KeyCode.Escape;
        public KeyCode shotKey = KeyCode.Space;
    }

    public enum ePlayerSettingsFpsType
    {
        NoLimit,
        Fps30,
        Fps60,
        Fps120,
        Fps240,
        Fps300,
        Fps500,
    }

    public PlayerKeySettings playerKeySettings;
    public ePlayerSettingsFpsType fpsType;
}

public class PlayerSettingsManager : MonoBehaviour
{
    public static PlayerSettingsManager Instance;


    GameManager gameManager;


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


    private void Update()
    {
        checkFps();
        //if (Input.anyKeyDown)
        //{
        //    Debug.Log(Input.inputString);
        //}
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
    }


    private void checkFps()
    {
        PlayerSettings playerSettings = gameManager.playerSettings;
        if (playerSettings.fpsType == PlayerSettings.ePlayerSettingsFpsType.NoLimit) Application.targetFrameRate = -1;
        else if (playerSettings.fpsType == PlayerSettings.ePlayerSettingsFpsType.Fps30) Application.targetFrameRate = 30;
        else if (playerSettings.fpsType == PlayerSettings.ePlayerSettingsFpsType.Fps60) Application.targetFrameRate = 60;
        else if (playerSettings.fpsType == PlayerSettings.ePlayerSettingsFpsType.Fps120) Application.targetFrameRate = 120;
        else if (playerSettings.fpsType == PlayerSettings.ePlayerSettingsFpsType.Fps240) Application.targetFrameRate = 240;
        else if (playerSettings.fpsType == PlayerSettings.ePlayerSettingsFpsType.Fps300) Application.targetFrameRate = 300;
        else if (playerSettings.fpsType == PlayerSettings.ePlayerSettingsFpsType.Fps500) Application.targetFrameRate = 500;
    }






}
