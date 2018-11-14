using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {
    [HideInInspector]
    public int stateHighscore;

    public void Awake() {
        DontDestroyOnLoad(this.gameObject);

        Load();
    }

    public void Save() {
        stateHighscore = GameManager.highscore;
        SaveLoadManager.SaveGameState(this);
    }

    public void Load() {
        int[] loadedData = SaveLoadManager.LoadGameState();
        if(loadedData == null) {
            Save();
        }

        int loadedHighscore = loadedData[0];
        GameManager.highscore = loadedHighscore;
    }

    public void Reset() {
        GameManager.highscore = 0;
        Save();
    }
}
