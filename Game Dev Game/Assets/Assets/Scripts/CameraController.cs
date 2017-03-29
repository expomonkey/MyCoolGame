using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject player;
    private Vector3 offset;
    public float CameraSpeed=.02f;
    // Use this for initialization
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
          Vector3 campos = transform.position;
        campos.y += CameraSpeed;
          //campos.y = player.transform.position.y+offset.y;
          transform.position = campos;
        //transform.position.y = player.transform.position.y + offset.y;
    }
}
