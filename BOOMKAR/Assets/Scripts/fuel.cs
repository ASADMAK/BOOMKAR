using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fuel : MonoBehaviour {

    public GameObject fuelprefab;
    float time;
    GameObject fuelspawn;
    bool spawnit = false;

	// Use this for initialization
	void Start () {
        time = 0;
        spawn();
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if(spawnit==true && time>15)
        {
            spawn();
            time = 0;
        }
	}

    public void spawn()
    {
        fuelspawn = Instantiate(fuelprefab, transform);
        spawnit = false;
    }

    public void startspawning()
    {
        spawnit = true;
    }
}
