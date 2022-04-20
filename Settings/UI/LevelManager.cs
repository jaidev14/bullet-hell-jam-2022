using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance{set;get;}

    public float time = 0;
    public int score = 0;
    private int deaths = 0;

    [SerializeField] private Transform spawnPosition = null;
    [SerializeField] private GameObject playerPrefab = null;
    // public FadeController blackScreen = null;
    public GameObject curPlayer = null;

    public bool levelActive = false;

    GameObject[] coinSpawners = null;
    GameObject[] switchs = null;

    void Awake() {
        Instance = this;
    }

    void Start() {
        StartLevel();
    }

    void Update() {
        if (levelActive) {
            time += Time.deltaTime;
        }
    }

    public void StartLevel() {
        time = 0f;
        StartCoroutine(StartLevelCoroutine());
    }

    public void Lose() {
        StartCoroutine(LoseCoroutine());
    }

    public void Win() {
        GameManager.Instance.time += time;
        GameManager.Instance.score += score;
        GameManager.Instance.deaths += deaths;

        // blackScreen.FadeIn(1f);
        StartCoroutine(WinFadeIn());
    }

    public void AddScore(int otherScore) {
        score += otherScore;
    }

    public void IsActive(bool active) {
        levelActive = active;
    }

    IEnumerator LoseCoroutine() {
        levelActive = false;
        AudioManager.StopMusicAudio();
        yield return new WaitForSeconds(0.5f);

        Destroy(curPlayer);
    }

    IEnumerator StartLevelCoroutine() {
        levelActive = true;
        AudioManager.StartLevelAudio();
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator WinFadeIn() {
        levelActive = false;
        AudioManager.StopMusicAudio();
        yield return new WaitForSeconds(0.5f);
    }
}
