using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] private float ratioX = 9.0f;
    [SerializeField] private float ratioY = 16.0f;

    public static ScreenManager Instance;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        SetGameScreenResolution(ratioX / ratioY);
    }

    public void SetGameScreenResolution(float _targeRatio)
    {
        float ratio = (float)Screen.width / (float)Screen.height;
        float scaleHeight = ratio / _targeRatio;
        float fixedWidth = (float)Screen.width / scaleHeight;

        Screen.SetResolution((int)fixedWidth, Screen.height, true);
    }


    public void SetGameScreenResolution(float _ratioX, float _ratioY)
    {
        float _targeRatio = _ratioX / _ratioY;
        SetGameScreenResolution(_targeRatio);
    }


}
