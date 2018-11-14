using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public static class SaveLoadManager {
    private static string saveFilePath = Application.persistentDataPath + "/gameState.sav";

    public static void SaveGameState(GameState state) {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(saveFilePath, FileMode.Create);

        GameStateData stateData = new GameStateData(state);

        formatter.Serialize(stream, stateData);
        stream.Close();
    }

    public static int[] LoadGameState() {
        if (File.Exists(saveFilePath)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(saveFilePath, FileMode.Open);

            GameStateData stateData = formatter.Deserialize(stream) as GameStateData;

            stream.Close();
            return stateData.data;
        }
        else {
            Debug.LogWarning("Save files does not exist.");
            return null;
        }
    }
}

[Serializable]
public class GameStateData {
    public int[] data;

    public GameStateData(GameState state) {
        data = new int[1];

        data[0] = state.stateHighscore;
    }
}