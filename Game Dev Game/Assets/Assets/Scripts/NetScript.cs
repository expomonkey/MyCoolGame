using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetScript : MonoBehaviour {
	private Animator animator;
	public int jump=0;
	// Use this for initialization
	void Start () {
		animator=  GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (jump==1){
			animator.SetTrigger ("Jump");
	}
		jump = 0;
}
}
