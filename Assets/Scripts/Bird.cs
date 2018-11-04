using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {
    Rigidbody2D body;
    SpriteRenderer birdSprite;
    Sprite normalSprite;

    public float jumpSpeed;
    public Sprite flappingSprite;


    void Start () {
        body = GetComponent<Rigidbody2D>();
        birdSprite = GetComponent<SpriteRenderer>();

        normalSprite = birdSprite.sprite;
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            jump();
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
}
