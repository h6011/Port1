using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


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
        Fps30,
        Fps60,
        Fps120, 
        Fps144,
        Fps160,
        Fps165,
        Fps180,
        Fps200,
        Fps240,
        Fps360,
        NoLimit,
    }

    public PlayerKeySettings playerKeySettings;
    public ePlayerSettingsFpsType fpsType = ePlayerSettingsFpsType.NoLimit;
}

public class PlayerSettingsManager : MonoBehaviour
{
    public static PlayerSettingsManager Instance;

    GameManager gameManager;

    string fileName = "__PlayerSettings";
    string path = "";


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            path = Application.persistentDataPath + $"/{fileName}";
            Debug.Log(path);
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
        /*
         * Fps30,
        Fps60,
        Fps120, 
        Fps144,
        Fps160,
        Fps165,
        Fps180,
        Fps200,
        Fps240,
        Fps360,
        NoLimit,
         */
        PlayerSettings playerSettings = gameManager.playerSettings;
        if (playerSettings.fpsType == PlayerSettings.ePlayerSettingsFpsType.Fps30) setFps(30);
        else if (playerSettings.fpsType == PlayerSettings.ePlayerSettingsFpsType.Fps60) setFps(60);
        else if (playerSettings.fpsType == PlayerSettings.ePlayerSettingsFpsType.Fps120) setFps(120);
        else if (playerSettings.fpsType == PlayerSettings.ePlayerSettingsFpsType.Fps144) setFps(160);
        else if (playerSettings.fpsType == PlayerSettings.ePlayerSettingsFpsType.Fps160) setFps(240);
        else if (playerSettings.fpsType == PlayerSettings.ePlayerSettingsFpsType.Fps180) setFps(180);
        else if (playerSettings.fpsType == PlayerSettings.ePlayerSettingsFpsType.Fps200) setFps(200);
        else if (playerSettings.fpsType == PlayerSettings.ePlayerSettingsFpsType.Fps240) setFps(240);
        else if (playerSettings.fpsType == PlayerSettings.ePlayerSettingsFpsType.Fps360) setFps(360);
        else if (playerSettings.fpsType == PlayerSettings.ePlayerSettingsFpsType.NoLimit) setFps(-1);
        else setFps(-1);
    }





    public void ChangeFpsType(PlayerSettings.ePlayerSettingsFpsType fpsType)
    {
        gameManager.playerSettings.fpsType = fpsType;
        string jsoned = JsonConvert.SerializeObject(gameManager.playerSettings);
        setFileText(jsoned);
    }



    public PlayerSettings GetPlayerSettings()
    {
        //JsonConvert.DeserializeObject()
        PlayerSettings _playerSettings = new PlayerSettings();

        string fileText = getFileText();


        if (fileText == string.Empty)
        {
            SetPlayerSettings(_playerSettings);
        }
        else
        {
            _playerSettings = JsonConvert.DeserializeObject<PlayerSettings>(fileText);
        }


        return _playerSettings;

    }




    public void SetPlayerSettings(PlayerSettings _playerSettings)
    {
        gameManager.playerSettings = _playerSettings;
        string jsoned = JsonConvert.SerializeObject(_playerSettings);
        setFileText(jsoned);
    }


    private string getFileText()
    {
        bool isExist = File.Exists(path);
        if (isExist)
        {
            return File.ReadAllText(path);
        }
        else
        {
            return string.Empty;
        }
    }

    private void setFileText(string text)
    {
        File.WriteAllText(path, text);
    }














}
