using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gamemanagar : MonoBehaviour {

   
    public void changescene(int n)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(n);
        
    }
    public void gamepause()
    {
        Time.timeScale = 0;
    }
    public void retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
       
    }
    public void gameresume()
    {
        Time.timeScale = 1;
    }
    public void nextlevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
       
    }

    public void menu()
    {
        int m=PlayerPrefs.GetInt("firstone", 0);
        if(m==0)
        {
            SceneManager.LoadScene(2);
            PlayerPrefs.SetInt("firstone", 2);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
