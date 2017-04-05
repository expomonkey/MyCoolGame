using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour {
    public GameObject[] obj;
    public float spawntime = 10.5f;
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
		//Spawns a prefab from a list at using it's original transform values
		//every "spawntime" seconds? ticks?
        GameObject thing = obj[Random.Range(0, obj.GetLength(0))];
        Vector3 pos = thing.transform.position;
        pos.y = pos.y+transform.position.y;
        Instantiate(thing, pos, Quaternion.identity);
        Invoke("spawn", spawntime);
    }
}
