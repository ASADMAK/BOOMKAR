using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class complete : MonoBehaviour {

    bool levelcomplete;
    public GameObject missioncomplete,game,dropship;
    public AudioSource levelcompleted;
    public GameObject cameraa,player;
    public Animator drop;

	// Use this for initialization
	void Start () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (levelcomplete == true)
        {
            if (other.tag == "player")
            {
   
                cameraa.SetActive(true);
                dropship.SetActive(true);
                drop.SetTrigger("level");
                Invoke("completeanim", 8);
                Invoke("playeroff", 5.5f);
                FindObjectOfType<carcontroller>().playerstationary();
            }
        }
    }

    public void completeanim()
    {
        missioncomplete.SetActive(true);
        game.SetActive(false);
        levelcompleted.Play();
    }
    // Update is called once per frame
    void Update () {
		
	}
    public void done()
    {
        levelcomplete = true;
    }
    public void playeroff()
    {
        player.SetActive(false);
    }
}
