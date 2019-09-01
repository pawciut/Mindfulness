﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewHighscore : MonoBehaviour
{

    [SerializeField()]
    TextMeshProUGUI TimeText;
    [SerializeField()]
    TextMeshProUGUI ScoreText;

    // Start is called before the first frame update
    void Start()
    {
        TimeText.text = Constants.FormatTime( PlayerPrefs.GetInt(Constants.Player_Pref_Time));
        ScoreText.text = PlayerPrefs.GetInt(Constants.Player_Pref_Score).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
