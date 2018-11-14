using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextManagerScript : MonoBehaviour {
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI PressSpaceText;

    // Use this for initialization
    void Start () {
        adjustTutorialMessage();
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

    public void adjustTutorialMessage() {
        #if UNITY_STANDALONE
            PressSpaceText.text = "PRESS SPACE BUTTON!!";
        #elif UNITY_WEBGL
            PressSpaceText.text = "PRESS SPACE BUTTON!!";
        #elif UNITY_ANDROID
            PressSpaceText.text = "TOUCH    ON THE SCREEN!!";
        #elif UNITY_IOS
            PressSpaceText.text = "TOUCH    ON THE SCREEN!!";
        #endif
    }
}
