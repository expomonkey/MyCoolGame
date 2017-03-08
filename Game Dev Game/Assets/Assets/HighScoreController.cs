using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreController : MonoBehaviour {
    public GUIText highScore;
    public int highScoreNum = 0;
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        highScore.text =""+ PlayerPrefs.GetInt("High Score");
    }
}
