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
        SoundManagerScript.PlaySound("Button");
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
        SoundManagerScript.PlaySound("Button");
        help.SetActive(true);
    }
    public void OpenDebug(GameObject debug)
    {
        SoundManagerScript.PlaySound("Button");
        debug.SetActive(true);
    }
    public void CloseDebug(GameObject debug)
    {
        SoundManagerScript.PlaySound("Button");
        debug.SetActive(false);
    }

    public void OnOpenLevelSelect()
    {
        SoundManagerScript.PlaySound("Button");
        LevelSelection.SetActive(true);
    }
    public void OnCloseLevelSelect()
    {
        SoundManagerScript.PlaySound("Button");
        LevelSelection.SetActive(false);
    }

    public void onOpenShowAds()
    {
        SoundManagerScript.PlaySound("Button");
        ShowAds.SetActive(true);
    }
    public void OnCloseShowAds()
    {
        SoundManagerScript.PlaySound("Button");
        ShowAds.SetActive(false);
    }

    public void BackMainMenu()
    {
        SoundManagerScript.PlaySound("Button");
        GameUI.SetActive(true);
        adsManager.HideBanner();
        SceneManager.LoadScene("Title Scene");
    }

    public void OnLoadLevelOne()
    {
        SoundManagerScript.PlaySound("Button");
        Time.timeScale = 1;
        SceneManager.LoadScene("Level 1");
    }

    public void OnLoadLevelTwo()
    {
        SoundManagerScript.PlaySound("Button");
        Time.timeScale = 1;
        SceneManager.LoadScene("Level 2");
    }

    public void OnLoadLevelThree()
    {
        SoundManagerScript.PlaySound("Button");
        Time.timeScale = 1;
        SceneManager.LoadScene("Level 3");
    }

    public void OnMaxHealth(Toggle t)
    {
        SoundManagerScript.PlaySound("Button");
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
        SoundManagerScript.PlaySound("Button");
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
        SoundManagerScript.PlaySound("Button");
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
        SoundManagerScript.PlaySound("Button");
        adsManager.ShowInterstitialAd();
    }

    public void OnBanner()
    {
        SoundManagerScript.PlaySound("Button");
        adsManager.ShowBanner();
    }

    public void OnRewarded()
    {
        SoundManagerScript.PlaySound("Button");
        adsManager.ShowRewawrdedAd();
    }
}
