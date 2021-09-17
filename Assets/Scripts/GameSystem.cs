using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameSystem: MonoBehaviour
{
    //Level
    public static int stage = 1;
    public static int wave = 1;
    public static bool boss_level = true;
    public static bool next = false;
    bool going_next_level = false;

    //Swipe Gesture
    bool swiped = false;
    bool check_swipe = false;

    public static int enemy_increment = 1;
    int wave_increment = 0;
    
    //1 - win   2 - lose
    int game_result = 0;

    //Backgrounds
    [SerializeField] GameObject Wave_1_BG;
    [SerializeField] GameObject LoadingScreen;
    [SerializeField] Image LoadingUI;
    [SerializeField] GameObject DialogueBox;
    public static bool dialogue_end = false;

    Sprite bg;

    public string level;

    AdsManager adsManager;

    // Start is called before the first frame update
    void Start()
    {
        stage = 1;
        wave = 1;
        boss_level = false;
        next = false;
        going_next_level = false;
        swiped = false;
        enemy_increment = 5;
        wave_increment = 0; 
        game_result = 0;
        dialogue_end = false;

        adsManager = GameObject.FindGameObjectWithTag("AdsManager").GetComponent<AdsManager>();
        GestureManager.Instance.OnSwipe += OnSwipe;
        adsManager.OnAdDone += OnAdDone;

        adsManager.ShowBanner();

        Time.timeScale = 1;
        //SoundManagerScript.PlaySound("BGM_Default");
    }

    private void OnAdDone(object sender, AdFinishEventArgs e)
    {
        //Check if the ad is the rewarded ad
        if (e.PlacementID == AdsManager.SampleRewarded)
        {
            switch (e.AdResult)
            {
                case UnityEngine.Advertisements.ShowResult.Failed: Debug.Log("Ad failed loading"); break;
                case UnityEngine.Advertisements.ShowResult.Skipped: Debug.Log("Ad skipped"); break;
                case UnityEngine.Advertisements.ShowResult.Finished:
                    Debug.Log("Ad finished properly");
                    Time.timeScale = 1;
                    UICallbackScript.gameOver = false;
                    PlayerSystem.health = 100;
                    game_result = 0;
                    break;
            }
        }
    }

    private void OnSwipe(object sender, SwipeEventArgs e)
    {
        if(e.SwipeDirection == SwipeDirections.RIGHT && check_swipe)
        {
            UICallbackScript.instuctions = false;
            swiped = true;
            check_swipe = false;
        }
    }

    private void OnDisable()
    {
        //Deregister our tap function for cleanup
        GestureManager.Instance.OnSwipe -= OnSwipe;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerSystem.health <= 0)
        {
            game_result = 2;
        }

        if (game_result == 0)
        {
            if (EnemySpawner.count <= 0 && next == false)
            {
                if (wave >= 4)
                {
                    UICallbackScript.instuctions = true;
                    
                    if (swiped)
                    {
                        UICallbackScript.instuctions = false;                    
                        swiped = false;                       

                        switch (SceneManager.GetActiveScene().name)
                        {
                            case "Level 1":
                                Time.timeScale = 1;
                                PlayerSystem.health += 30;
                                if (PlayerSystem.health >= 100) PlayerSystem.health = 100;
                                SceneManager.LoadScene("Level 2");
                                adsManager.ShowInterstitialAd();
                                break;
                            case "Level 2":
                                PlayerSystem.health += 30;
                                if (PlayerSystem.health >= 100) PlayerSystem.health = 100;
                                Time.timeScale = 1;
                                SceneManager.LoadScene("Level 3");
                                adsManager.ShowInterstitialAd();
                                break;
                            case "Level 3":
                                game_result = 1;
                                //SceneManager.LoadScene("Title Scene");
                                break;
                        }
                    }  
                }

                if (!going_next_level)
                {
                    next = true;

                    switch (wave)
                    {
                        case 1:
                            Wave_1_BG.SetActive(true);
                            bg = AssetBundleManager.Instance.GetAsset<Sprite>("background", "L" + level + "W" + 1);
                            Wave_1_BG.GetComponent<SpriteRenderer>().sprite = bg;
                            break;

                        case 2:
                            bg = AssetBundleManager.Instance.GetAsset<Sprite>("background", "L" + level + "W" + 2);
                            Wave_1_BG.GetComponent<SpriteRenderer>().sprite = bg;
                            break;

                        case 3:
                            bg = AssetBundleManager.Instance.GetAsset<Sprite>("background", "L" + level + "W" + 3);
                            Wave_1_BG.GetComponent<SpriteRenderer>().sprite = bg;

                            boss_level = true;
                            wave_increment = 2;
                            break;
                    }

                    //Total of 3 waves each (> 1);       //Debug = -1             
                    if (wave_increment > 1)
                    {
                        wave++;
                        wave_increment = 0;
                        going_next_level = true;
                    }

                    else
                    {
                        wave_increment++;
                    }                                      
                }

                else
                {
                    if (!dialogue_end)
                        DialogueBox.SetActive(true);

                    else
                    {
                        UICallbackScript.instuctions = true;
                        check_swipe = true;

                        if (swiped)
                        {
                            UICallbackScript.damaged = false;
                            going_next_level = false;
                            enemy_increment += 5;
                            UICallbackScript.instuctions = false;
                            swiped = false;
                            dialogue_end = false;
                        }
                    }                   
                }
            }            
        }

        else if(game_result == 1)
        {
            GameWin();
        }

        else { GameOver(); }
    }

    void GameOver()
    {
        if(PlayerSystem.health <= 0)
        {
            Time.timeScale = 0;
            Debug.Log("Game Over");
            UICallbackScript.gameOver = true;
        }
    }

    void GameWin()
    {
        Debug.Log("Game Win");
        Time.timeScale = 0;
        UICallbackScript.gameWin = true;
    }
}
