using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UICallbackScript : MonoBehaviour
{
    [HideInInspector]
    public bool shoot = false;

    [SerializeField] private GameObject Health_Bar;
    [SerializeField] private Text Health_Holder;
    [SerializeField] private GameObject Score_Holder;
    [SerializeField] private Text Credit_Holder;

    [SerializeField] private GameObject gameMenu;
    [SerializeField] private GameObject shopMenu;

    //Initialize Windows/Pop-ups
    [SerializeField] GameObject GameResultPopUp;
    [SerializeField] GameObject GameWinPopUp;
    [SerializeField] Text Message;
    [SerializeField] GameObject Instructions;
    [SerializeField] GameObject DamageIndicator;

    [SerializeField] GameObject ShopMenu;
    [SerializeField] GameObject UpgradeMenu;
    [SerializeField] GameObject BuyMenu;
    [SerializeField] GameObject OptionsMenu;
    [SerializeField] GameObject DebugMenu;
    [SerializeField] GameObject LevelMenu;
    [SerializeField] GameObject AddPlayer;

    public static bool instuctions = false;
    public static bool gameOver = false;
    public static bool gameWin = false;

    public static bool newLevel = false;
    public static bool damaged = false;

    public AdsManager adsManager;

    //Interval
    public static int hour = 0;
    public static int min = 0;
    public static int sec = 0;

    // Start is called before the first frame update
    void Awake()
    {
        instuctions = false;
        gameOver = false;
        gameWin = false;

        Instructions.SetActive(false);
        GameResultPopUp.SetActive(false);
        ShopMenu.SetActive(false);
        UpgradeMenu.SetActive(false);
        BuyMenu.SetActive(false);
        OptionsMenu.SetActive(false);
        DebugMenu.SetActive(false);
        LevelMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();

        if (newLevel)
        {
            instuctions = false;
            gameOver = false;
            gameWin = false;
            newLevel = false;

            Instructions.SetActive(false);
            GameResultPopUp.SetActive(false);
            ShopMenu.SetActive(false);
            UpgradeMenu.SetActive(false);
            BuyMenu.SetActive(false);
            OptionsMenu.SetActive(false);
            DebugMenu.SetActive(false);
            LevelMenu.SetActive(false);
        }

        if (instuctions)
        {
            Instructions.SetActive(true);
        }
        else
        {
            Instructions.SetActive(false);
        }

        if (gameOver)
        { 
            Message.text = "GAME OVER";
            GameResultPopUp.SetActive(true);
        }

        else
        {
            GameResultPopUp.SetActive(false);
        }

        if (gameWin)
        {
            Message.text = "YOU SURVIVED";
            GameWinPopUp.SetActive(true);
        }

        if (damaged)
        {
            DamageIndicator.GetComponent<Image>().color = new Color(Color.red.r, Color.red.g, Color.red.b, (float) 30/255);
        }

        else
        {
            DamageIndicator.GetComponent<Image>().color = new Color(Color.red.r, Color.red.g, Color.red.b, 0);
        }
    }

    public void onShoot()
    {
        shoot = true;
    }
    public void onShopButton()
    {
        adsManager.HideBanner();
        SoundManagerScript.PlaySound("Button");
        gameMenu.SetActive(false);
        shopMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void onReturn()
    {
        adsManager.ShowBanner();
        SoundManagerScript.PlaySound("Button");
        gameMenu.SetActive(true);
        shopMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void openOptions(GameObject options)
    {
        adsManager.ShowBanner();
        SoundManagerScript.PlaySound("Button");
        adsManager.HideBanner();
        options.SetActive(true);
        gameMenu.SetActive(false);
        
        Time.timeScale = 0;
    }

    public void closeOptions(GameObject options)
    {
        SoundManagerScript.PlaySound("Button");
        options.SetActive(false);
        gameMenu.SetActive(true);
        Time.timeScale = 1;
    }

    public void onChangeName(InputField input)
    {
        PlayerSystem.player_name = input.text;
    }

    public void onConfirmName()
    {
        SoundManagerScript.PlaySound("Button");
        SceneManager.LoadScene("Title Scene");
        WebHandlerScript.Instance.CreatePlayer();
        Debug.Log($"Player name: {PlayerSystem.player_name}");
        adsManager.HideBanner();
    }

    public void onOK()
    {
        SoundManagerScript.PlaySound("Button");
        AddPlayer.SetActive(true);
    }

    void UpdateUI()
    {
        Health_Bar.GetComponent<Image>().fillAmount = PlayerSystem.health / 100;
        Health_Holder.GetComponent<Text>().text = PlayerSystem.health.ToString() + " / " + 100;
        Score_Holder.GetComponent<Text>().text = PlayerSystem.score.ToString();
        Credit_Holder.text = PlayerSystem.credits.ToString();
    }
}
