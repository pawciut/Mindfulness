using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStateManager : MonoBehaviour
{
    public UnityEvent OnLevelEnded;

    float time = 0;
    int score;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt(Constants.Player_Pref_Score, 0);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
    }

    public void EndGame()
    {
        Time.timeScale = 0;
        PlayerPrefs.SetInt(Constants.Player_Pref_Time, Convert.ToInt32(time));
        
        
        StartCoroutine(GoToNewHighscore(3));
    }

    public IEnumerator GoToNewHighscore(int delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        OnLevelEnded?.Invoke();
    }

    public string GetTime()
    {
        return Constants.FormatTime(time);
    }
}
