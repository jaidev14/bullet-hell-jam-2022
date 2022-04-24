using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameStat {
        public float time;
        public int killedShadows;
        public int deaths;

        public GameStat() {
            this.deaths = 0;
            this.killedShadows = 0;
            this.time = 0;
        }

        public GameStat(float time, int killedShadows, int deaths) {
            this.time = time;
            this.killedShadows = killedShadows;
            this.deaths = deaths;
        }
}
