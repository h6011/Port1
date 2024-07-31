using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyMainCanvasManger : MonoBehaviour
{
    GameManager gameManager;
    RankingManager rankingManager;
    PlayerSettingsManager playerSettingsManager;


    [Header("Ranking")]
    [SerializeField] GameObject rankingItemPrefab;

    [SerializeField] Transform rankingContenTrs;




    [Header("Button")]

    [SerializeField] private Button playBtn;
    [SerializeField] private Button rankingBtn;
    [SerializeField] private Button settingsBtn;
    [SerializeField] private Button exitGameBtn;

    [Header("Settings UI")]

    [Space]
    [SerializeField] TMP_Dropdown fpsDropdown;


    private void Awake()
    {
        Tool.isLoadMainScene = true;
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        rankingManager = RankingManager.Instance;
        playerSettingsManager = PlayerSettingsManager.Instance;

        buttonClickAction();
        autoBackBtnAction();
        visibleOnlyUI("Main");
        settingsUIAction();
    }

    private void settingsUIAction()
    {
        fpsDropdown.ClearOptions();

        Array _fpsArray = Enum.GetValues(typeof(PlayerSettings.ePlayerSettingsFpsType));
        int count = _fpsArray.Length;
        List<string> fpsList = new List<string>();

        for (int i = 0; i < count; i++)
        {
            fpsList.Add(_fpsArray.GetValue(i).ToString());
        }

        fpsDropdown.AddOptions(fpsList);

        PlayerSettings.ePlayerSettingsFpsType savedFpsType = playerSettingsManager.GetPlayerSettings().fpsType;

        fpsDropdown.SetValueWithoutNotify((int)savedFpsType);





        fpsDropdown.onValueChanged.AddListener((int call) => {
            PlayerSettings.ePlayerSettingsFpsType selectedFpsType = (PlayerSettings.ePlayerSettingsFpsType)call;
            playerSettingsManager.ChangeFpsType(selectedFpsType);
            playerSettingsManager.CheckFps();
        });

    }


    private void autoBackBtnAction()
    {
        int count = transform.childCount;
        for (int iNum = 0; iNum < count; iNum++)
        {
            Transform child = transform.GetChild(iNum);
            if(child.name != "Main")
            {
                Transform findBackBtn = child.Find("BackBtn");
                if (findBackBtn)
                {
                    Button _btn = findBackBtn.GetComponent<Button>();
                    addListenerToBtn(_btn, () => {
                        visibleOnlyUI("Main");
                    });
                }
            }
        }
    }

    private void visibleUI(string _name, bool _boolean = true)
    {
        Transform FindUI = transform.Find(name);
        if (FindUI) FindUI.gameObject.SetActive(_boolean);
    }

    private void toggleUI(string _name)
    {
        Transform FindUI = transform.Find(name);
        if (FindUI) FindUI.gameObject.SetActive(FindUI.gameObject.activeSelf);
    }

    private void visibleOnlyUI(string _name)
    {
        int count = transform.childCount;
        for (int iNum = 0; iNum < count; iNum++)
        {
            Transform child = transform.GetChild(iNum);
            if (child.name == _name)
            {
                child.gameObject.SetActive(true);
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }
    }




    private void addListenerToBtn(Button _btn, UnityEngine.Events.UnityAction action)
    {
        _btn.onClick.RemoveAllListeners();
        _btn.onClick.AddListener(action);
    }
    
    private void buttonClickAction()
    {
        addListenerToBtn(playBtn, () => {
            SceneManager.LoadScene("Game");
        });
        addListenerToBtn(rankingBtn, () => {
            visibleOnlyUI("Ranking");

            resetRanking();

        });
        addListenerToBtn(settingsBtn, () => {
            visibleOnlyUI("Settings");
        });
        addListenerToBtn(exitGameBtn, () => {
            gameManager.GameExit();
        });
    }

    private void clearRanking()
    {
        int count = rankingContenTrs.childCount;
        for (int i = 0; i < count; i++)
        {
            Transform child = rankingContenTrs.GetChild(i);
            Destroy(child.gameObject);
        }
    }

    private void displayRanking()
    {
        List<RankingManager.RankInfo> currentRanks = rankingManager.GetRanking();
        int count = currentRanks.Count;

        for (int i = 0; i < count; i++)
        {
            RankingManager.RankInfo _rankInfo = currentRanks[i];

            GameObject newRankingPrefab = Instantiate(rankingItemPrefab, rankingContenTrs);
            TMPro.TMP_Text text = newRankingPrefab.GetComponent<TMPro.TMP_Text>();
            if (_rankInfo.name == "")
            {
                text.text = $"#{i + 1}";
            }
            else
            {
                text.text = $"#{i+1} {_rankInfo.name} : {_rankInfo.score}";
            }
        }


    }

    private void resetRanking()
    {
        clearRanking();
        displayRanking();
    }






}
