using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BirdScript : MonoBehaviour {
    Rigidbody2D body;
    SpriteRenderer birdSprite;
    Sprite normalSprite;

    public float jumpSpeed;
    [HideInInspector] public static bool dead = false;
    public Sprite flappingSprite;
    float floorYCoordiante = -4.5f;
    
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI PressSpaceText;

    void Start () {
        GameManager.score = 0;
        body = GetComponent<Rigidbody2D>();
        birdSprite = GetComponent<SpriteRenderer>();

        normalSprite = birdSprite.sprite;

        if (!GameManager.pressedSpace) {
            body.gravityScale = 0.4f;
            PressSpaceText.enabled = true;
        }
    }

    void Update () {
        if (isDead()) {
            StartCoroutine(dieAnimation(1f));
        }
        else {
            if (GameManager.IsInputEnabled && Input.GetKeyDown(KeyCode.Space)) {
                jump();
            }
        }
    }

    IEnumerator flappingAnimation(float waitTime) {
        birdSprite.sprite = flappingSprite;
        yield return new WaitForSeconds(waitTime);
        birdSprite.sprite = normalSprite;
    }

    void jump() {
        body.velocity = new Vector2(0f, jumpSpeed);
        StartCoroutine(flappingAnimation(0.1f));

        if (!GameManager.pressedSpace) {
            body.gravityScale = 1.5f;
            GameManager.pressedSpace = true;
            PressSpaceText.enabled = false;
        }
    }

    public void die() {
        dead = false;
        ObstacleArrayScript.obstaclesSpeed = 0;
        body.velocity = new Vector2(-4f, 5f);
        body.angularVelocity = +120f;
        body.gravityScale = 1f;
        GameManager.IsInputEnabled = false;

    }

    public bool isDead() {
        if (dead) return true;
        if (transform.position.y < floorYCoordiante) return true;

        return false;
    }

    // OnTriggerEnter2D is called when the Collider2D other enters the trigger (2D physics only)
    public void OnTriggerEnter2D(Collider2D other) {
        GameObject objectCollided = other.gameObject;
        if (objectCollided.tag == "ScoreArea") {
            GameManager.score += 1;
            ScoreText.text = GameManager.score.ToString();
        }
    }

    IEnumerator dieAnimation(float waitTime) {
        die();
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("GameOverScene");
    }


}
