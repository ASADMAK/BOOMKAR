using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;


public class unityads : MonoBehaviour {

    string gameId = "1234567";
    bool testMode = true;
    int timer;
    public ParticleSystem coineffect;

    public void showad()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            Advertisement.Initialize(gameId, true);
            timer = PlayerPrefs.GetInt("timer", 0);
            timer++;
            if (timer < 3)
            {
                Advertisement.Show();
                timer = 0;
            }
            PlayerPrefs.SetInt("timer", timer);
        }

    }

    public void ShowRewardedAd_revive()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                Debug.Log("revived");
                revive();
                break;
            case ShowResult.Skipped:
                SSTools.ShowMessage("The ad was skipped", SSTools.Position.bottom, SSTools.Time.oneSecond);
                break;
            case ShowResult.Failed:
                SSTools.ShowMessage("Internet not available", SSTools.Position.bottom, SSTools.Time.oneSecond);
                break;
        }
    }
    public void revive()
    {
        FindObjectOfType<carcontroller>().revived();
    }
    // Show an ad:
    public void ShowRewardedAd_coindouble()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResultcoin };
            Advertisement.Show("rewardedVideo", options);
        }
    }

    private void HandleShowResultcoin(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                Debug.Log("double");
                doublecoin();
                break;
            case ShowResult.Skipped:
                SSTools.ShowMessage("The ad was skipped", SSTools.Position.bottom, SSTools.Time.oneSecond);
                break;
            case ShowResult.Failed:
                SSTools.ShowMessage("Internet not available", SSTools.Position.bottom, SSTools.Time.oneSecond);
                break;
        }
    }
    public void doublecoin()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            int money = PlayerPrefs.GetInt("gold", 0);
            money += 100;
            PlayerPrefs.SetInt("gold", money);
            coineffect.Play();
            FindObjectOfType<shop>().addshown();
        }
        else
        {
            FindObjectOfType<carcontroller>().doublecoin();
        }
    }

   

}
