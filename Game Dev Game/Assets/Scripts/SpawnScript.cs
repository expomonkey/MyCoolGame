using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour {
    public GameObject[] obj;
    public float spawntime = 8.5f;
    public float CameraSpeed=0.02f;
    //public float thing = obj[0].x;
	// Use this for initialization
	void Start () {
        spawn();
	}
    private void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos.y += CameraSpeed;
        transform.position = pos;
    }
    
	void spawn()
    {
        GameObject thing = obj[Random.Range(0, obj.GetLength(0))];
        Vector3 pos = thing.transform.position;
        pos.y = pos.y+transform.position.y;
        Instantiate(thing, pos, Quaternion.identity);
        //GameObject thing =Instantiate(obj[Random.Range(0, obj.GetLength(0))],transform.position,Quaternion.identity);
        // Vector3 thingpos = thing.transform.position;
        // thingpos.x = 0.0f;
        // thingpos.y = 12.0f;
        //  thing.transform.position = thingpos;

        Invoke("spawn", spawntime);
    }
}
