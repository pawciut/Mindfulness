using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMusic : MonoBehaviour
{
    [SerializeField()]
    AudioSource MenuMusic;
    [SerializeField()]
    AudioSource LevelMusic;


    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public void PlayMenu()
    {
        LevelMusic.Stop();
        if (MenuMusic.isPlaying) return;
        MenuMusic.Play();
    }

    public void PlayLevelMusic()
    {
        MenuMusic.Stop();
        if (LevelMusic.isPlaying) return;
        LevelMusic.Play();

    }

    public void StopMusic()
    {
        MenuMusic.Stop();
        LevelMusic.Stop();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == Constants.Level1Scene)
            PlayLevelMusic();
    }
}