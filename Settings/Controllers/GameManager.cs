using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance{set;get;}

    public int deaths = 3;
    public float time = 0;
    public int killedShadows = 13;

    public bool finished = false;
    public List<GameStat> gameStats = null;


    void Awake() {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        gameStats = SaveSystem.Instance.LoadGameStats();
    }


    public void BackToMenu() {
        StartCoroutine(LoadSceneAsync("Menu"));
    }

    public void StartGame() {
        PlayMuna();
    }

    public void EndGame() {
        finished = true;
        SaveFinalScore();
        StartCoroutine(LoadSceneAsync("Menu"));
    }

    void SaveFinalScore() {
        GameStat gameStat = new GameStat(time, killedShadows, deaths);
        SaveSystem.Instance.SaveGameStat(gameStat);
    }

    public void PlayMuna() {
        StartCoroutine(LoadSceneAsync("Good"));
    }

    public void PlayNicte() {
        StartCoroutine(LoadSceneAsync("Good"));
    }

    public void QuitGame() {
        Application.Quit();
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Pre-loading Events
        while (!asyncLoad.isDone)
        {
            // Loading Effects
            yield return null;
        }

        // Post-loading Events
        if (finished) {
            finished = false;
            GameObject.FindGameObjectWithTag("MainMenu").GetComponent<MenuManager>().OpenLevelSelectionScreen();
        }
    }

}
