using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{

    private void Update()
    {
        gameExitKeyAction();
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
