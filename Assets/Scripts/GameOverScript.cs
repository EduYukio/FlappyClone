using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour {

    public void retryButton() {
        SceneManager.LoadScene("MainScene");
    }

    public void menuButton() {
        SceneManager.LoadScene("MenuScene");
    }
}
