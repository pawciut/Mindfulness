using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UIPlayerManager : MonoBehaviour
{
    public UnityEvent ItemAdded;
    public UnityEvent ItemRemoved;

    public UnityEvent BerryAdded;
    public UnityEvent RockAdded;
    public UnityEvent StickAdded;
    public UnityEvent FocusAdded;

    public UnityEvent PlayerDied;

    public GameObject Focus;

    [SerializeField()]
    GameStateManager StateManager;

    /// <summary>
    /// TimeValuePresenter to jedyna kontrolka do prezentacji czasu wiec posluzy i do ukrywania i do wyswietlanai wartosci
    /// </summary>
    [Header("Time Settings")]
    [SerializeField()]
    TextMeshProUGUI TimeValuePresenter;

    /// <summary>
    /// Score prezenter to obiekt pod którym jest Etykieta oraz Wartość wyniku, posłuży nam do ukrywania tych informacji
    /// </summary>
    [Header("Score Settings")]
    [SerializeField()]
    GameObject ScorePresenter;
    /// <summary>
    /// Tylko kontrolka do prezentacji wartości wyniku
    /// </summary>
    [SerializeField()]
    TextMeshProUGUI ScoreValuePresenter;


    public UILifeControl[] Lives = new UILifeControl[GameStateManager.MaxLives];
    public int InitialLives = 3;

    public AudioSource PlayerDeathSound;

    // Start is called before the first frame update
    void Start()
    {
        Focus.SetActive(true);//Włącza obiekt efektu wizualnego Focus, bo inaczej by w edytorze zaslanial mape, a po rozpoczeciu musi byc wlaczony

        if (StateManager.TimeDisabled)
            HideTime();
        if (StateManager.ScoreDisabled)
            HideScore();
    }


    // Update is called once per frame
    void Update()
    {
    }

    private void ResetLives()
    {
        foreach(var liveUI in Lives)
        {
            liveUI.Add();
        }
    }

    public void UpdateLives()
    {
        var lives = PlayerPrefs.GetInt(Constants.Player_Pref_Lives, 0);
        for(int i=0;i<GameStateManager.MaxLives;++i)
        {
            if (lives > 0)
                Lives[i].Add();
            else
                Lives[i].Remove();
            --lives;
        }
    }

    public void SetItem(InteractableObject obj)
    {
        if (obj == null)
        {
            ItemRemoved?.Invoke();
        }
        else
        {
            ItemAdded.Invoke();
            switch (obj.PickupType)
            {
                case PickupType.Berry:
                    BerryAdded?.Invoke();
                    break;
                case PickupType.Rock:
                    RockAdded?.Invoke();
                    break;
                case PickupType.Stick:
                    StickAdded?.Invoke();
                    break;
                case PickupType.PieceOfMind:
                    FocusAdded?.Invoke();
                    break;
                default:
                    ItemRemoved?.Invoke();
                    break;
            }

        }
    }

    /// <summary>
    /// ta akcja powinna byc wywolana przez zdarzenie GameStateManagera.ScoreUpdated
    /// </summary>
    public void UpdateScoreText()
    {
        if (ScoreValuePresenter != null)
            ScoreValuePresenter.text = PlayerPrefs.GetInt(Constants.Player_Pref_Score, 0).ToString();
        else
            Debug.LogError($"{nameof(ScoreValuePresenter)} not set");
    }

    public void UpdateTimeText()
    {
        if(TimeValuePresenter != null)
            TimeValuePresenter.text = Constants.FormatTime(PlayerPrefs.GetInt(Constants.Player_Pref_Time, 0));
        else
            Debug.LogError($"{nameof(TimeValuePresenter)} not set");
    }

    public void ShowTime()
    {
        TimeValuePresenter.gameObject.SetActive(true);
    }
    public void HideTime()
    {
        TimeValuePresenter.gameObject.SetActive(false);
    }
    public void ShowScore()
    {
        TimeValuePresenter.gameObject.SetActive(true);
    }
    public void HideScore()
    {
        TimeValuePresenter.gameObject.SetActive(false);
    }




}
