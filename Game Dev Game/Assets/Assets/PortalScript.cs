using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour {
    public GameObject target;
    public Vector2 dir;
    public float dirx=0.0f;
    public float diry=0.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        dir = target.transform.position - transform.position;
         dirx = dir.x;
         diry = dir.y;

	}
}
