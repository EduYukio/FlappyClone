using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void playButton() {
        SceneManager.LoadScene("MainScene");
    }

    public void quitButton() {
        Application.Quit();
    }
}
