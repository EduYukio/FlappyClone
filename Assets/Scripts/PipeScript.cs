using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // OnTriggerEnter2D is called when the Collider2D other enters the trigger (2D physics only)
    public void OnTriggerEnter2D(Collider2D collision) {
        GameObject objectCollided = collision.gameObject;
        if(objectCollided.tag == "Bird") {
            BirdScript birdScript = objectCollided.GetComponent<BirdScript>();
            birdScript.dead = true;
        }
    }
}
