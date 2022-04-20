using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameStat {
        public float time;
        public int score;
        public int deaths;
        public int end;
        // 0 - Pacifist, 1 - Assassin, 2 - Mediocre, 3 - Genocide

        public GameStat() {
            this.deaths = 0;
            this.score = 0;
            this.time = 0;
            this.end = 0;
        }

        public GameStat(float time, int score, int deaths, int end) {
            this.time = time;
            this.score = score;
            this.deaths = deaths;
            this.end = end;
        }
}
