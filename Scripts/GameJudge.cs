using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;

public class GameJudge : MonoBehaviour
{   
    private static GameJudge instance;
    public int score = 0;
    public int trials = 0;
    public int max_trials = 1;

    public static GameJudge getInstance()
    {
        if (instance == null)
        {
            instance = (GameJudge)FindObjectOfType(typeof(GameJudge));
            if (instance == null)
            {
                Debug.LogError("An instance of " + typeof(GameJudge)
                    + " is needed in the scene, but there is none.");
            }
        }
        return instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    public void scoring(Disk disk)
    {
        score += disk.score;
    }

    public bool over()
    {
        if( trials > max_trials)
        {
            return true;
        }
        return false;
    } 
    public void Reset()
    {
        score = 0;
        trials = 0;
    }
}
