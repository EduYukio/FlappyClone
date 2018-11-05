using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScript : MonoBehaviour {

    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI HighScoreText;

    void Start() {
        refreshGameState();
        showScore();
    }

    public void retryButton() {
        SceneManager.LoadScene("MainScene");
    }

    public void menuButton() {
        SceneManager.LoadScene("MenuScene");
    }

    public void refreshGameState() {
        GameManager.IsInputEnabled = true;
        ObstacleArrayScript.obstaclesSpeed = 3f;
    }

    public void showScore() {
        ScoreText.text = "SCORE     " + GameManager.score.ToString();

        if (GameManager.score > GameManager.highscore) {
            FindObjectOfType<AudioManager>().Play("Victory");
            GameManager.highscore = GameManager.score;
            HighScoreText.text = "HIGHSCORE     " + GameManager.highscore.ToString() + "\n                             NEW!!!";

        }
        else {
            HighScoreText.text = "HIGHSCORE     " + GameManager.highscore.ToString();
        }
    }
}
