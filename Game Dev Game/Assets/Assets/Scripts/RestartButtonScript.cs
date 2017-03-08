using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButtonScript : MonoBehaviour {

    // Use this for initialization
    private void OnMouseDown()
    {
        Application.LoadLevel("Start Menu");
    }}
