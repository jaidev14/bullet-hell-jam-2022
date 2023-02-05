using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance{set;get;}

    private float time = 0;
    private int killedShadows = 0;

    public bool levelActive = false;

    private CameraCinematicController camFX;
    public FadeController fc;


    void Awake() {
        Instance = this;
    }

    void Start() {
        camFX = GetComponent<CameraCinematicController>();
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

    public void Finish() {
        Debug.Log("Finishing");
        StartCoroutine(FinishCoroutine());
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

    IEnumerator FinishCoroutine() {
        levelActive = false;
        GameManager.Instance.time = time;
        GameManager.Instance.killedShadows = killedShadows;
        GameManager.Instance.deaths += 1;
        AudioManager.StopMusicAudio();
        fc.FadeBlack(3f);
        yield return new WaitForSeconds(3f);
        Debug.Log("lesgo");
        GameManager.Instance.EndGame();
    }

    IEnumerator StartLevelCoroutine() {
        AudioManager.StartLevelAudio();
        camFX.GetComponent<CameraCinematicController>().StartCinematic();
        fc.FadeTransparent(1f);
        yield return new WaitForSeconds(5f);
        camFX.GetComponent<CameraCinematicController>().StopCinematic();
        levelActive = true;
    }

    IEnumerator WinFadeIn() {
        levelActive = false;
        // AudioManager.StopMusicAudio();
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.EndGame();
    }
}