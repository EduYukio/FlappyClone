using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextManagerScript : MonoBehaviour {
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI PressSpaceText;

    // Use this for initialization
    void Start () {
        if (!GameManager.pressedSpace) {
            PressSpaceText.enabled = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void removePressSpaceText() {
        PressSpaceText.enabled = false;
    }

    public void updateScore() {
        ScoreText.text = GameManager.score.ToString();
    }
}
