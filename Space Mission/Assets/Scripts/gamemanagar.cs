using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gamemanagar : MonoBehaviour {

   
    public void changescene(int n)
    {
        SceneManager.LoadScene(n);
        Time.timeScale = 1;
    }
    public void gamepause()
    {
        Time.timeScale = 0;
    }
    public void retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
    public void gameresume()
    {
        Time.timeScale = 1;
    }
    public void nextlevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
