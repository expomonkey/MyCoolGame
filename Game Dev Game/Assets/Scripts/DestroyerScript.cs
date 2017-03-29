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
            Application.LoadLevel("Game Over");
            //SceneManager.LoadScene("Main Level");
          //  Debug.Break();
            return;
        }
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
        Vector3 deathos=transform.position;
        deathos.y += CameraSpeed;
        transform.position = deathos;
    }
}
