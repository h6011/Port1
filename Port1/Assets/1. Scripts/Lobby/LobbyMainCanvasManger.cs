using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyMainCanvasManger : MonoBehaviour
{
    GameManager gameManager;


    [Header("Button")]

    [SerializeField] private Button playBtn;
    [SerializeField] private Button rankingBtn;
    [SerializeField] private Button settingsBtn;
    [SerializeField] private Button exitGameBtn;


    private void Awake()
    {
        Tool.isLoadMainScene = true;
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        buttonClickAction();
        autoBackBtnAction();
        visibleOnlyUI("Main");
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
        });
        addListenerToBtn(settingsBtn, () => {
            visibleOnlyUI("Settings");
        });
        addListenerToBtn(exitGameBtn, () => {
            gameManager.GameExit();
        });
    }




}
