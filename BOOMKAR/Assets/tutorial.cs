using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial : MonoBehaviour {

    public GameObject tutorial1;
    public GameObject tutorial3;
    public GameObject tutorail4;
    public GameObject tutorial5;
    public GameObject tutorial6;
    public GameObject meter,floppy,coin;
    public GameObject carcontrol;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void starttutorial()
    {
        tutorial1.SetActive(true);
        Invoke("pause", 1);
    }
    public void tutorial3active()
    {
        tutorial3.SetActive(true);
        carcontrol.SetActive(false);
        FindObjectOfType<gamemanagar>().gamepause();
        meter.SetActive(true);
    }

    public void pause()
    {
        FindObjectOfType<gamemanagar>().gamepause();
    }
    public void tutorial4active()
    {
        Invoke("ontu", 5);

    }
    public void ontu()
    {
        tutorail4.SetActive(true);
        carcontrol.SetActive(false);
    }
    public void tutorial5active()
    {
        Invoke("ont", 3);
    }
    public void ont()
    {
        tutorial5.SetActive(true);
        carcontrol.SetActive(false);
        floppy.SetActive(false);
        meter.SetActive(true);
    }
    public void tutorial6active()
    {
        tutorial6.SetActive(true);
        FindObjectOfType<gamemanagar>().gamepause();
        carcontrol.SetActive(false);
   
    }

    public void tutorial2()
    {
        carcontrol.SetActive(false);
        floppy.SetActive(true);
    }
    public void carcontrolactive()
    {
        carcontrol.SetActive(true);
    }
    public void floppycactive()
    {
        floppy.SetActive(true);
    }
}
