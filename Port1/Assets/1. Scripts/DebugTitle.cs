using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugTitle : MonoBehaviour
{
    public static DebugTitle Instance;

    [SerializeField] TMP_Text title1;
    [SerializeField] TMP_Text title2;
    [SerializeField] TMP_Text title3;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        resetAllTitle();
    }

    private void resetAllTitle()
    {
        title1.text = string.Empty;
        title2.text = string.Empty;
        title3.text = string.Empty;
    }


    public void SetTitleTitle1(string text)
    {
        title1.text = text;
    }

    public void SetTitleTitle2(string text)
    {
        title2.text = text;
    }

    public void SetTitleTitle3(string text)
    {
        title3.text = text;
    }






}
