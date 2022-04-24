using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem: MonoBehaviour {
    public static SaveSystem Instance{set;get;}
    public List<GameStat> sessionSavedGameStats = null;

    private void Awake() {
        Instance = this;
    }

    public bool IsSaveFile() {
        return Directory.Exists(Application.persistentDataPath + "/game_save");
    }

    private bool CheckForDirectory(string path) {
        if (!Directory.Exists(Application.persistentDataPath + "/" + path)) {
            Directory.CreateDirectory(Application.persistentDataPath + "/" + path);
            return false;
        }
        return true;
    }

    // ------------------------- Level Stats -------------------------
    public void SaveGameStat(GameStat gameStat) {
        CheckForDirectory("game_save");
        CheckForDirectory("game_save/game_stats_data");
        string directoryName = "games";
        CheckForDirectory("game_save/game_stats_data/" + directoryName);

        string filePath = Application.persistentDataPath + "/game_save/game_stats_data/" + directoryName + "/game.jai";
        GameStat data = gameStat;

        if (File.Exists(filePath)) {
            data = MergeGameStats(gameStat);
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(filePath);

        formatter.Serialize(file, data);
        file.Close();
        LoadGameStats();
    }

    public List<GameStat> LoadGameStats() {
        Debug.Log(Application.persistentDataPath + "/game_save/game_stats_data/games");
        List<GameStat> savedGameStats = new List<GameStat>();
        
        if (!CheckForDirectory("game_save/game_stats_data/games")) {
            return savedGameStats;
        }
        
        BinaryFormatter formatter = new BinaryFormatter();

        // Get directory file names and order them
        string[] gameStatFiles = Directory.GetFiles(Application.persistentDataPath + "/game_save/game_stats_data/games");
        if (gameStatFiles.Length == 0) { return savedGameStats; }

        foreach (string fileName in gameStatFiles)
        {
            FileStream file = File.Open(fileName, FileMode.Open);

            GameStat gameStat = formatter.Deserialize(file) as GameStat;

            savedGameStats.Add(gameStat);
            file.Close();
        }
        sessionSavedGameStats = savedGameStats;
        return savedGameStats;
    }


    private GameStat MergeGameStats(GameStat currentGameStat) {
        if (sessionSavedGameStats == null) {
            sessionSavedGameStats = new List<GameStat>();
            return currentGameStat;
        }

        GameStat bestGameStat = sessionSavedGameStats[0];
        GameStat resultingGameStat = currentGameStat;
        if (bestGameStat.time == 0) {
            return currentGameStat;
        }

        resultingGameStat.time = bestGameStat.time < currentGameStat.time ? bestGameStat.time : currentGameStat.time; 
        sessionSavedGameStats.Add(resultingGameStat);
        return resultingGameStat;
    }
}