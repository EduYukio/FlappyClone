using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BirdScript : MonoBehaviour {
    Rigidbody2D body;
    SpriteRenderer birdSprite;
    Sprite normalSprite;

    public float jumpSpeed;
    [HideInInspector] public bool dead = false;
    public Sprite flappingSprite;
    float floorYCoordiante = -4.5f;

    float score = 0f;
    public TextMeshProUGUI ScoreText;

    void Start () {
        body = GetComponent<Rigidbody2D>();
        birdSprite = GetComponent<SpriteRenderer>();

        normalSprite = birdSprite.sprite;
    }

    void Update () {
        if (isDead()) {
            die();
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
    }

    public void die() {
        //Debug.Log("morri");
        dead = false;
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
            score += 1;
            ScoreText.text = score.ToString();
        }
    }


}
