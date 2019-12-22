using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;


public class googleplay : MonoBehaviour {

	// Use this for initialization
	void Start () {

        authenticateuser();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void authenticateuser()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) =>
        {
            if(success==true)
            {
                SSTools.ShowMessage("Signing in success", SSTools.Position.bottom, SSTools.Time.oneSecond);
            }
            else
            {
                SSTools.ShowMessage("Signing in failed", SSTools.Position.bottom, SSTools.Time.oneSecond);
            }
        }



        );
    }
    public static void  posttoleaderboard(long newscore)
    {
        Social.ReportScore(newscore, GPGSIds.leaderboard_level_completed, (bool success) =>
          {
              if(success)
              {

              }
              else
              {

              }
          }
        );
    }
    public static void showleaderboardui()
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_level_completed);
    }
}
