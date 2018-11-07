using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static bool IsInputEnabled = true;
    public static bool pressedSpace = false;
    public static int score;
    public static int highscore;

    public bool debug = false;

    public float jumpHeight = 9.5f;
    public static float birdJumpHeight;

    public float gravityScale = 3f;
    public static float birdGravityScale;

    public float movementSpeed = 3.2f;
    public static float pipeMovementSpeed = 0;

    public float distance = 4.3f;
    public static float distanceBetweenObjects;


    public void Start() {
        birdJumpHeight = jumpHeight;
        birdGravityScale = gravityScale;
        if (pressedSpace) {
            pipeMovementSpeed = movementSpeed;
        }
        distanceBetweenObjects = distance;
    }

    public void Update() {
        if (debug) {
            birdJumpHeight = jumpHeight;
            birdGravityScale = gravityScale;
            pipeMovementSpeed = movementSpeed;
            distanceBetweenObjects = distance;
        }
    }
}
