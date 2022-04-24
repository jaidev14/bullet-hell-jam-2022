using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel = null;
    [SerializeField] private GameObject levelSelectionPanel = null;
    [SerializeField] private GameObject settingsPanel = null;

    [SerializeField] private TMP_Text deathsText = null;
    [SerializeField] private TMP_Text killedShadowsText = null;
    [SerializeField] private TMP_Text timeText = null;

    void Start()
    {
        AudioManager.StartMenuAudio();
        SetupStats();
        OpenMainMenuScreen();
    }

    void SetupStats() {
        Debug.Log("Setting up stats");
        deathsText.text = "deaths: " + GameManager.Instance.deaths;
        killedShadowsText.text = "kills: " + GameManager.Instance.killedShadows;
        timeText.text = GetTime(GameManager.Instance.time);
    }

    public void OpenLevelSelectionScreen() {
        SetupStats();
        // Set up panels
        mainMenuPanel.SetActive(false);
        levelSelectionPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void OpenSettingsScreen() {
        // Set up panels
        mainMenuPanel.SetActive(false);
        levelSelectionPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
    public void OpenMainMenuScreen() {
        // Set up panels
        mainMenuPanel.SetActive(true);
        levelSelectionPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void SelectPlayMuna() {
        // Set up panels
        mainMenuPanel.SetActive(false);
        levelSelectionPanel.SetActive(false);
        settingsPanel.SetActive(false);

        // Load Scene
        GameManager.Instance.PlayMuna();
    }

    public void SelectPlayNicte() {
        // Set up panels
        mainMenuPanel.SetActive(false);
        levelSelectionPanel.SetActive(false);
        settingsPanel.SetActive(false);

        // Load Scene
        GameManager.Instance.PlayNicte();
    }

    public void QuitGame() {
        // Load Scene
        GameManager.Instance.QuitGame();
    }

    private string GetTime(float time) {
        int minutes = (int) Mathf.Floor(time / 60);
        int seconds = (int) (time % 60);
        int milliseconds = (int) (time * 1000f) % 1000;
        if (minutes < 1) {
            return seconds.ToString ("D2") + "." + milliseconds.ToString("D2");
        }
        return minutes.ToString ("D2") + ":" + seconds.ToString ("D2") + "." + milliseconds.ToString("D2");
    }
}
