using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStateManager : MonoBehaviour
{
    public UnityEvent OnLevelEnded;

    public UnityEvent OnScoreUpdated;
    public UnityEvent OnTimeUpdated;
    public UnityEvent OnLivesUpdated;

    /// <summary>
    /// Na potrzeby poziomów, w których nie zliczamy wyniku np. tutorial
    /// </summary>
    public bool ScoreDisabled = false;
    /// <summary>
    /// Na potrzeby poziomów, w których nie zliczamy czasu np. tutorial
    /// </summary>
    public bool TimeDisabled = false;

    bool GameEnded;

    float time = 0;
    int score;

    [SerializeField]
    [Range(1, MaxLives)]
    int initialLives = 2;
    [SerializeField]
    [Range(1, MaxLives)]
    int currentLives = 2;

    //TODO:przeniesc do const?
    public const int MaxLives = 3;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        time = 0;
        score = 0;
        GameEnded = false;

        InitializeLives();
        PlayerPrefs.SetInt(Constants.Player_Pref_Score, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameEnded)
        {
            time += Time.deltaTime;
            PlayerPrefs.SetInt(Constants.Player_Pref_Time, (int)Mathf.Round(time));
            if (OnTimeUpdated == null)
                Debug.LogError($"{nameof(OnTimeUpdated)} not set");
            else
                OnTimeUpdated?.Invoke();
        }
    }

    void InitializeLives()
    {
        currentLives = initialLives;
        PlayerPrefs.SetInt(Constants.Player_Pref_Lives, currentLives);
        if (OnLivesUpdated == null)
            Debug.LogError($"{nameof(OnLivesUpdated)} not set");
        else
            OnLivesUpdated.Invoke();
    }

    public void EndGame()
    {
        GameEnded = true;
        Time.timeScale = 0;
        PlayerPrefs.SetInt(Constants.Player_Pref_Time, Convert.ToInt32(time));


        StartCoroutine(GoToNewHighscore(3));
    }

    public IEnumerator GoToNewHighscore(int delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        OnLevelEnded?.Invoke();
    }

    public void AddScore(int scoreValue)
    {
        if (!ScoreDisabled && !GameEnded)
        {
            score += scoreValue;
            PlayerPrefs.SetInt(Constants.Player_Pref_Score, score);

            if (OnScoreUpdated == null)
                Debug.LogError($"{nameof(OnScoreUpdated)} not set");
            else
                OnScoreUpdated.Invoke();

            Debug.Log($"Added score {scoreValue}");
        }
    }


    public void AddLives(int lives)
    {
        var currentLives = PlayerPrefs.GetInt(Constants.Player_Pref_Lives, 0);
        PlayerPrefs.SetInt(Constants.Player_Pref_Lives, currentLives += lives);

        if (OnLivesUpdated == null)
            Debug.LogError($"{nameof(OnLivesUpdated)} not set");
        else
            OnLivesUpdated.Invoke();

        Debug.Log($"Added {currentLives} lives");
    }

    public void SubstractLives(int lives)
    {
        AddLives(-lives);
    }
}
