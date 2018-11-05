using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour {

    // OnTriggerEnter2D is called when the Collider2D other enters the trigger (2D physics only)
    public void OnTriggerEnter2D(Collider2D collision) {
        GameObject objectCollided = collision.gameObject;
        if(objectCollided.tag == "Bird") {
            BirdScript.dead = true;
        }
    }
}
