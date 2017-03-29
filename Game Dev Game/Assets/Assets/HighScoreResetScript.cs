using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreResetScript : MonoBehaviour {

    // Use this for initialization
    void Start() { }
    private void OnMouseDown()
    {
        PlayerPrefs.SetInt("High Score", 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
