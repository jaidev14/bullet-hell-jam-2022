using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RankingManager : MonoBehaviour
{
    public GameObject[] medals = null;
    public GameObject scoreDialog = null;
    public TMP_Text dialogText = null;
    public List<GameStat> gameStats = null;
    private List<int> achievedEnds = new List<int>();
    // 0 - Pacifist, 1 - Assassin, 2 - Mediocre, 3 - Genocide

    void Start() {
        // Get from save system
        SetupRanking();
    }

    public void SetupRanking()
    {
        scoreDialog.SetActive(false);
        foreach (GameObject medal in medals) {
            medal.SetActive(false);
        }
        gameStats = GameManager.Instance.gameStats;
        foreach (GameStat gameStat in gameStats) {
            achievedEnds.Add(gameStat.end);
        }
        foreach (int achievedEnd in achievedEnds) {
            medals[achievedEnd].SetActive(true);
        }
    }

    public void OpenScoreDialog(int endType) {
        scoreDialog.SetActive(true);
        GameStat dialogStat = new GameStat();
        foreach (GameStat gameStat in gameStats) {
            if (gameStat.end == endType) {
                dialogStat = gameStat;
            }
        }
        string scoreText = "Time: " + GetTime(dialogStat.time) + 
                            "\nScore: " + dialogStat.score.ToString() + 
                            "\nDeaths: " + dialogStat.deaths.ToString();
        dialogText.text = scoreText;
    }

    public void CloseScoreDialog() {
        scoreDialog.SetActive(false);
    }

    private string GetTime(float time) {
        int minutes = (int) Mathf.Floor(time / 60);
        int seconds = (int) (time % 60);
        int milliseconds = (int) (time * 1000f) % 1000;
        return minutes.ToString ("D2") + ":" + seconds.ToString ("D2") + "." + milliseconds.ToString("D2");
    }
}
