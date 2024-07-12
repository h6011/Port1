using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text ScreenText;


    void Start()
    {

        float targeRatio = 9.0f / 16.0f;


        float ratio = (float)Screen.width / (float)Screen.height;

        Debug.Log("ratio: " + ratio);

        float scaleHeight = ratio / targeRatio;

        Debug.Log("scaleHeight: " + scaleHeight);

        float fixedWidth = (float)Screen.width / scaleHeight;


        Screen.SetResolution((int)fixedWidth, Screen.height, true);

        ScreenText.text = $"Screen Width = {fixedWidth}, ScreenHeight = {Screen.height}";
    }

    private void Update()
    {
        GameExit();
    }

    private void GameExit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
