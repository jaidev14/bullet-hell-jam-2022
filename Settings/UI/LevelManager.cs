using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance{set;get;}

    private float time = 0;
    private int killedShadows = 0;

    // public FadeController blackScreen = null;
    private bool levelActive = false;


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
        killedShadows = 0;
        StartCoroutine(StartLevelCoroutine());
    }

    public void Die() {
        Debug.Log("Dying");
        StartCoroutine(DieCoroutine());
    }

    public void Win() {
        GameManager.Instance.time = time;
        GameManager.Instance.killedShadows = killedShadows;
        GameManager.Instance.deaths++;

        StartCoroutine(WinFadeIn());
    }

    public void IsActive(bool active) {
        levelActive = active;
    }

    IEnumerator DieCoroutine() {
        levelActive = false;
        GameManager.Instance.time = time;
        GameManager.Instance.killedShadows = killedShadows;
        GameManager.Instance.deaths += 1;
        // AudioManager.StopMusicAudio();
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.EndGame();
    }

    IEnumerator StartLevelCoroutine() {
        // AudioManager.StartLevelAudio();
        yield return new WaitForSeconds(0.5f);
        levelActive = true;
    }

    IEnumerator WinFadeIn() {
        levelActive = false;
        // AudioManager.StopMusicAudio();
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.EndGame();
    }
}
