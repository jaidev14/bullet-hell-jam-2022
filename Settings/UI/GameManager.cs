using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance{set;get;}

    public int score = 0;
    public int deaths = 0;
    public float time = 0;

    public int killedRabbits = 0;
    public int killedShadows = 0;
    [SerializeField] private int maxRabbits = 0;

    public bool finished = false;
    public List<GameStat> gameStats = null;

    private int curLevel;

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
        StartCoroutine(LoadSceneAsync("Level" + curLevel));
    }

    void EndGame() {
        finished = true;
        SaveFinalScore();
        StartCoroutine(LoadSceneAsync("Menu"));
    }

    void SaveFinalScore() {
        int end = 2;
        if (killedRabbits == 0) {
            // Pacifist
            end = 0;
        } else if (killedRabbits == maxRabbits) {
            if (killedShadows == maxRabbits) {
                // Genocide
                end = 3;
            } else {
                // Assassin
                end = 1;
            }
        } else {
            // Mediocre
            end = 2;
        }
        GameStat gameStat = new GameStat(time, score, deaths, end);
        SaveSystem.Instance.SaveGameStat(gameStat);
    }

    public void SelectLevel(int level) {
        curLevel = level;
        StartCoroutine(LoadSceneAsync("Lvl0-NeonCityNicte"));
    }

    public void PlayMuna() {
        StartCoroutine(LoadSceneAsync("Lvl0-NeonCityMuna"));
    }

    public void PlayNicte() {
        StartCoroutine(LoadSceneAsync("Lvl0-NeonCityNicte"));
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
        // if (finished) {
        //     finished = false;
        //     GameObject.FindGameObjectWithTag("MainMenu").GetComponent<MenuManager>().OpenRankingScreen();
        // }
    }

}
