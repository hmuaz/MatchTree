﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelCounter
{
    Timer,
    Moves
}

public abstract class LevelGoal : Singleton<LevelGoal>
{
    public int scoreStars = 0;

    public int[] scoreGoals = new int[3] { 1000, 2000, 3000 };

    public int movesLeft = 30;

    public int timeLeft = 60;

    public LevelCounter levelCounter = LevelCounter.Moves;

    int m_maxTime;

    public virtual void Start()
    {
        Init();

        if (levelCounter == LevelCounter.Timer)
        {
            m_maxTime = timeLeft;

            if (UIManager.Instance != null && UIManager.Instance.timer != null)
            {
                UIManager.Instance.timer.InitTimer(timeLeft);
            }
        }
    }

    public void Init()
    {

        scoreStars = 0;
        
        for (int i = 1; i < scoreGoals.Length; i++)
        {
            if (scoreGoals[i] < scoreGoals[i - 1])
            {
                Debug.LogWarning("LEVELGOAL Setup score goals in increasing order!");
            }
        }
    }

    public int UpdateScore(int score)
    {
        for (int i = 0; i < scoreGoals.Length; i++)
        {
            if (score < scoreGoals[i])
            {
                return i;
            }
        }
        return scoreGoals.Length;

    }

    public void UpdateScoreStars(int score)
    {
        scoreStars = UpdateScore(score);
    }
        
    public abstract bool IsWinner();
    public abstract bool IsGameOver();


    public void StartCountdown()
    {
        StartCoroutine(CountdownRoutine());
    }

    IEnumerator CountdownRoutine()
    {
        while (timeLeft > 0)
        {
            yield return new WaitForSeconds(1f);
            timeLeft--;

            if (UIManager.Instance != null && UIManager.Instance.timer != null)
            {
                UIManager.Instance.timer.UpdateTimer(timeLeft);
            }
        }
    }

    public void AddTime(int timeValue)
    {
        timeLeft += timeValue;
        timeLeft = Mathf.Clamp(timeLeft, 0, m_maxTime);

        if (UIManager.Instance != null && UIManager.Instance.timer != null)
        {
            UIManager.Instance.timer.UpdateTimer(timeLeft);
        }
    }

}
