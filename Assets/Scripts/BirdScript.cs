using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour {
    Rigidbody2D body;
    SpriteRenderer birdSprite;
    Sprite normalSprite;

    public float jumpSpeed;
    [HideInInspector] public bool dead = false;
    public Sprite flappingSprite;


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
            if (Input.GetKeyDown(KeyCode.Space)) {
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
        Debug.Log("morri");
    }

    public bool isDead() {
        if (dead) return true;
        if (transform.position.y < -4.5f) return true;

        return false;
    }
}
