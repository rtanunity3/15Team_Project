using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class DisplayMainPanel : MonoBehaviour
{
    [SerializeField] private Text highScoreText;
    [SerializeField] private Text currentScoreText;
    [SerializeField] private Text timerText;
    //[SerializeField] private Text countTanghuluText;

    //public bool isStart;
    public float limitTime=60f;
    private float timer;

    void Update()
    {
        if (GameManager.Instance.currentState == GameState.Playing)
        {
            timer -= Time.deltaTime;
            timer = Math.Max(timer, 0f);
        }
        else
        {
            timer = limitTime;
        }
        UpdateMainSceneMenuDisplay();
        if (timer <= 0f)
        {
            GameManager.Instance.GameOver();
        }
    }
    public void UpdateMainSceneMenuDisplay()
    {
        highScoreText.text = $"최고점수 {GameManager.Instance.highScore}";
        currentScoreText.text = $"현재점수 {GameManager.Instance.score}";
        timerText.text = $"남은시간\n{Math.Max(0f, (int)timer)}";
        //countTanghulu.text = $"탕후루\n{GameManager.Instance.tanghuluMade} / 10";
    }
}
