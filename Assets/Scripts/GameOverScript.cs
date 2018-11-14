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

    public void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            retryButton();
        }
    }

    public void retryButton() {
        SceneManager.LoadScene("MainScene");
    }

    public void menuButton() {
        SceneManager.LoadScene("MenuScene");
    }

    public void refreshGameState() {
        GameManager.IsInputEnabled = true;
        GameManager.pipeMovementSpeed = 3f;
    }

    public void showScore() {
        ScoreText.text = "SCORE     " + GameManager.score.ToString();

        if (GameManager.score > GameManager.highscore) {
            FindObjectOfType<AudioManager>().Play("Victory");
            GameManager.highscore = GameManager.score;

            GameObject gameStateObject = GameObject.Find("GameState");
            GameState gameState = gameStateObject.GetComponent<GameState>();
            gameState.Save();

            HighScoreText.text = "HIGHSCORE     " + GameManager.highscore.ToString() + "\n                             NEW!!!";
        }
        else {
            HighScoreText.text = "HIGHSCORE     " + GameManager.highscore.ToString();
        }
    }
}
