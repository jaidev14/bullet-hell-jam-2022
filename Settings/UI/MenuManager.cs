using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel = null;
    public GameObject levelSelectionPanel = null;
    public GameObject settingsPanel = null;

    void Start()
    {
        AudioManager.StartMenuAudio();
        OpenMainMenuScreen();
    }

    public void OpenLevelSelectionScreen() {
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
}
