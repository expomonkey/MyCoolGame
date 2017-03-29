using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreNavigatorScript : MonoBehaviour {

    private void OnMouseDown()
    {
        Application.LoadLevel("High Scores");
    }
}
