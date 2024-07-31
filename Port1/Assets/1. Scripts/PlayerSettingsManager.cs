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
        //CheckFps();
        //if (Input.anyKeyDown)
        //{
        //    Debug.Log(Input.inputString);
        //}
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void setFps(int _fps)
    {
        Application.targetFrameRate = _fps;
    }

    public void CheckFps()
    {
        PlayerSettings playerSettings = gameManager.playerSettings;
        if (playerSettings.fpsType == PlayerSettings.ePlayerSettingsFpsType.NoLimit) setFps(-1);
        else if (playerSettings.fpsType == PlayerSettings.ePlayerSettingsFpsType.Fps30) setFps(30);
        else if (playerSettings.fpsType == PlayerSettings.ePlayerSettingsFpsType.Fps60) setFps(60);
        else if (playerSettings.fpsType == PlayerSettings.ePlayerSettingsFpsType.Fps120) setFps(120);
        else if (playerSettings.fpsType == PlayerSettings.ePlayerSettingsFpsType.Fps240) setFps(240);
        else if (playerSettings.fpsType == PlayerSettings.ePlayerSettingsFpsType.Fps300) setFps(300);
        else if (playerSettings.fpsType == PlayerSettings.ePlayerSettingsFpsType.Fps500) setFps(500);
    }






}
