using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerScript : MonoBehaviour {
    public GameObject MainCamera;
    public float CameraSpeed =.02f;

    void OnTriggerEnter2D(Collider2D thing)
    {
        if (thing.tag == "Player")
        {
			//If the death quad hits the player, game over
            Application.LoadLevel("Game Over");
            return;
        }
		//Otherwise, destroy it (Which isn't currently working)
        if (thing.gameObject.transform.parent)
        {
            Destroy(thing.gameObject.transform.parent.gameObject);
        }
        else
        {
            Destroy(thing.gameObject);
        }
    }
    private void LateUpdate()
    {
		//Move the death quad up, CameraSpeed should be the same as the world's camera speed
		//As well as the spawner's cameraSpeed
        Vector3 deathos=transform.position;
        deathos.y += CameraSpeed;
        transform.position = deathos;
    }
}
