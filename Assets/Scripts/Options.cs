using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    public static bool aim_mode = true;
    public static bool unli_ammo = false;
    [SerializeField] GameObject Joystick;
    [SerializeField] GameObject OptionsUI;
    [SerializeField] GameObject LevelSelection;
    [SerializeField] GameObject GameUI;
    [SerializeField] GameObject ShowAds;

    public AdsManager adsManager;

    // Start is called before the first frame update
    void Start()
    {
        aim_mode = true;
        unli_ammo = false;
        LevelSelection.SetActive(false);        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnAimChange(Dropdown dd)
    {
        if (dd.options[dd.value].text == "Crosshair") {
            aim_mode = true;
            Joystick.SetActive(true);
        }
        else
        {
            aim_mode = false;
            Joystick.SetActive(false);
        }

        Debug.Log($"Options: {dd.options[dd.value].text}");
    }

    public void OpenHelp(GameObject help)
    {
        help.SetActive(true);
    }
    public void OpenDebug(GameObject debug)
    {
        debug.SetActive(true);
    }
    public void CloseDebug(GameObject debug)
    {
        debug.SetActive(false);
    }

    public void OnOpenLevelSelect()
    {
        LevelSelection.SetActive(true);
    }
    public void OnCloseLevelSelect()
    {
        LevelSelection.SetActive(false);
    }

    public void onOpenShowAds()
    {
        ShowAds.SetActive(true);
    }
    public void OnCloseShowAds()
    {
        ShowAds.SetActive(false);
    }

    public void BackMainMenu()
    {
        GameUI.SetActive(true);
        SceneManager.LoadScene("Title Scene");
    }

    public void OnLoadLevelOne()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level 1");
    }

    public void OnLoadLevelTwo()
    {
        SceneManager.LoadScene("Level 2");
    }

    public void OnLoadLevelThree()
    {
        SceneManager.LoadScene("Level 3");
    }

    public void OnMaxHealth(Toggle t)
    {
        if (t.isOn)
        {
            PlayerSystem.health = 99999999;
        }
        else
        {
            PlayerSystem.health = 100;
        }
    }

    public void OnMaxCredits(Toggle t)
    {
        if (t.isOn)
        {
            PlayerSystem.credits = 99999999;
        }
        else
        {
            PlayerSystem.credits = 500;
        }
    }

    public void OnMaxAmmo(Toggle t)
    {
        if (t.isOn)
        {
            unli_ammo = true;
        }
        else
        {
            unli_ammo = false;
        }
    }

    public void OnInterstitial()
    {
        adsManager.ShowInterstitialAd();
    }

    public void OnBanner()
    {
        adsManager.ShowBanner();
    }

    public void OnRewarded()
    {
        adsManager.ShowRewawrdedAd();
    }
}
