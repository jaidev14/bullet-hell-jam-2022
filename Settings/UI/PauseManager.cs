// using System.Collections;
// using System.Collections.Generic;
// using TMPro;
// using UnityEngine;
// using UnityEngine.UI;

// public class PauseManager : MonoBehaviour
// {
//     public static PauseManager Instance{set;get;}

//     [SerializeField] private TMP_Text scoreText = null;
//     [SerializeField] private TMP_Text timeText = null;

//     [SerializeField] private GameObject pausePanel = null;
//     public bool paused = false;

//     void Start() {
//         pausePanel.SetActive(false);
//         paused = false;
//         Time.timeScale = 1f;
//     }

//     void Update() {
//         if (Input.GetButtonDown("Cancel"))
//         {
//             TogglePause();
//         }
//     }

//     public void TogglePause() {
//         if (paused) {
//             paused = false;
//             Time.timeScale = 1f;
//         } else {
//             paused = true;
//             Time.timeScale = 0f;
//             scoreText.text = LevelManager.Instance.score.ToString();
//             timeText.text = GetTime(LevelManager.Instance.time);
//         }
//         pausePanel.SetActive(paused);
//         LevelManager.Instance.SetIsActive(!paused);
//     }

//     public void RestartGame() {
//         if (paused) {
//             TogglePause();
//             LevelManager.Instance.RestartLevel();
//         }
//     }

//     public void BackToMenu() {
//         GameManager.Instance.BackToMenu();
//     }

//     private string GetTime(float time) {
//         int minutes = (int) Mathf.Floor(time / 60);
//         int seconds = (int) (time % 60);
//         int milliseconds = (int) (time * 1000f) % 1000;
//         return minutes.ToString ("D2") + ":" + seconds.ToString ("D2") + "." + milliseconds.ToString("D2");
//     }
// }
