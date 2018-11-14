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

    [HideInInspector] public static bool dead = false;
    public Sprite flappingSprite;
    float floorYCoordiante = -4.5f;
    GameObject textManagerObject;
    TextManagerScript textManagerScript;

    void Start () {
        FindObjectOfType<AudioManager>().Play("Start");
        GameManager.score = 0;

        textManagerObject = GameObject.Find("TextManager");
        textManagerScript = textManagerObject.GetComponent<TextManagerScript>();

        body = GetComponent<Rigidbody2D>();
        birdSprite = GetComponent<SpriteRenderer>();

        normalSprite = birdSprite.sprite;

        if (!GameManager.pressedSpace) {
            body.gravityScale = 0.05f;
        }
    }

    void Update () {
        if (isDead()) {
            StartCoroutine(dieAnimation(1f));
        }
        else {
            if (GameManager.IsInputEnabled && (Input.GetKeyDown(KeyCode.Space) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))) {
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
        FindObjectOfType<AudioManager>().Play("Flap");
        body.velocity = new Vector2(0f, GameManager.birdJumpHeight);
        StartCoroutine(flappingAnimation(0.1f));

        if (!GameManager.pressedSpace) {
            body.gravityScale = GameManager.birdGravityScale;
            GameManager.pressedSpace = true;
            GameManager.pipeMovementSpeed = 3.2f;
            textManagerScript.removePressSpaceText();
        }
    }

    public void die() {
        FindObjectOfType<AudioManager>().Play("Baque");
        dead = false;
        GameManager.pipeMovementSpeed = 0;
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
            FindObjectOfType<AudioManager>().Play("Plim");
            GameManager.score += 1;
            textManagerScript.updateScore();
            Destroy(other);
        }
    }

    IEnumerator dieAnimation(float waitTime) {
        die();
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("GameOverScene");
    }
}
