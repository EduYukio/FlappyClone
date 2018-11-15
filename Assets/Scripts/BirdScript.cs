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

    public Sprite flappingSprite;
    readonly float floorYCoordiante = -4.5f;
    readonly float ceilingYCoordiante = 4.7f;
    bool alreadyDied = false;
    bool hasHitPipe = false;
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
            body.gravityScale = 0f;
        }
    }

    void Update () {
        if (!alreadyDied && MustDie()) {
            StartCoroutine(DeathAnimation(1f));
        }
        else {
            ProcessInputs();
        }
    }

    IEnumerator FlappingAnimation(float waitTime) {
        birdSprite.sprite = flappingSprite;
        yield return new WaitForSeconds(waitTime);
        birdSprite.sprite = normalSprite;
    }

    void jump() {
        FindObjectOfType<AudioManager>().Play("Flap");
        body.velocity = new Vector2(0f, GameManager.birdJumpHeight);
        StartCoroutine(FlappingAnimation(0.1f));

        if (!GameManager.pressedSpace) {
            body.gravityScale = GameManager.birdGravityScale;
            GameManager.pressedSpace = true;
            GameManager.pipeMovementSpeed = 3.2f;
            textManagerScript.removePressSpaceText();
        }
    }

    public void Die() {
        FindObjectOfType<AudioManager>().Play("Baque");
        GameManager.pipeMovementSpeed = 0;
        body.velocity = new Vector2(-4f, 5f);
        body.angularVelocity = +120f;
        body.gravityScale = 1f;
        GameManager.IsInputEnabled = false;
    }

    public bool MustDie() {
        if (hasHitPipe) return true;
        if (transform.position.y < floorYCoordiante) return true;
        if (transform.position.y > ceilingYCoordiante) return true;

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

        if (objectCollided.tag == "Obstacle") {
            hasHitPipe = true;
        }
    }

    IEnumerator DeathAnimation(float waitTime) {
        Die();
        alreadyDied = true;
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("GameOverScene");
    }

    public void ProcessInputs() {
        if (GameManager.IsInputEnabled) {
            #if UNITY_ANDROID || UNITY_IOS
                if (Input.touchCount > 0) {
                    TouchPhase touchPhase = Input.GetTouch(0).phase;

                    if(touchPhase == TouchPhase.Began){
                        jump();
                    }
                }
            #else
                if (Input.GetKeyDown(KeyCode.Space)) {
                    jump();
                }
            #endif
        }
    }
}
