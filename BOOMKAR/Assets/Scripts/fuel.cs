using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fuel : MonoBehaviour {

    public GameObject fuelprefab;
    public GameObject indicator;
    public int time;
    GameObject fuelspawn;
	// Use this for initialization
	void Start () {
        spawn();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag== "player")
        {
            Debug.Log("refuel");
            FindObjectOfType<carcontroller>().refill_Fuel();
            destoyit();
            indicator.SetActive(false);
            Invoke("spawn", time);
        }
    }
    public void spawn()
    {
        fuelspawn = Instantiate(fuelprefab, transform);
        indicator.SetActive(true);
    }
    public void destoyit()
    {
        Destroy(fuelspawn);
    }
}
