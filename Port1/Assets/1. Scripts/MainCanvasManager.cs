using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainCanvasManager : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField] TMP_Text coinText;
    [SerializeField] TMP_Text ScoreText;
    [SerializeField] TMP_Text MeterText;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void Update()
    {
        coinText.text = gameManager.Coin.ToString();
        ScoreText.text = gameManager.Score.ToString();
        MeterText.text = gameManager.Meter.ToString();
    }


}
