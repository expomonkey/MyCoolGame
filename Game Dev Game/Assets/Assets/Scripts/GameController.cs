using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public GUIText scoretext;
    int score;
    public GameObject player;
    float highest = 0.0f;
    public int highScoreTemp = 0;
    // Use this for initialization
    
    void Start()
    {
        highest = player.transform.position.y;
        Debug.Log("Started");
    }

    // Update is called once per frame
    void Update()
    {
    
        if (score > PlayerPrefs.GetInt("High Score"))
        {
            PlayerPrefs.SetInt("High Score", score);
        }

            if (player.transform.position.y > highest)
        {
            highest = player.transform.position.y;
            AddScore(1);
        }
    }
    public void AddScore (int newScoreValue)
    {
        score += newScoreValue;
        
        UpdateScore();

    }
    public void UpdateScore()
    {

        scoretext.text = "Score: " + score;
       // Debug.Log("Current Score" + score);
    }

}
