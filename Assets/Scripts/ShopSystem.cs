using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopSystem : MonoBehaviour
{
    //Credits
    [SerializeField] private Text Credit_Holder;
    [SerializeField] private Text Bill_Holder;

    //From other scripts
    protected WeaponSystem weaponsys;

    //Buy and Upgrade Pop 
    [SerializeField] private GameObject Buy_Pop_Up;
    [SerializeField] private GameObject Upgrade_Pop_Up;

    //Buy Button
    [SerializeField] private Button AWPButton_Buy;
    [SerializeField] private Button SixButton_Buy;

    //Upgrade Button
    [SerializeField] private Button FAMASButton_Upgrade;
    [SerializeField] private Button AWPButton_Upgrade;
    [SerializeField] private Button SixButton_Upgrade;

    [SerializeField] private Button ConfirmButton;
    [SerializeField] private Button ReturnButton;
    [SerializeField] private Button CancelButton;

    //Price List
    int AWP_Price = 200;
    int Six_Price = 500;

    //Upgrade
    [SerializeField] private Image Weapon_Image;
    [SerializeField] private Text Weapon_Name;

    [SerializeField] private Sprite FAMAS_Sprite;
    [SerializeField] private Sprite AWP_Sprite;
    [SerializeField] private Sprite Six_Sprite;

    [SerializeField] private Image Damage_Image;
    [SerializeField] private Text Damage_Label;

    [SerializeField] private Image Magazine_Image;
    [SerializeField] private Text Magazine_Label;

    //Cart
    string item_name;
    int damage_up = 10;
    int magazine_up = 10;

    //Temporary Values
    int damage_cost = 0;
    int magazine_cost = 0;
    bool start = true;

    // Start is called before the first frame update
    void Start()
    {
        weaponsys = FindObjectOfType<WeaponSystem>();

        Credit_Holder.text = $"Credits: {PlayerSystem.credits}";
        Bill_Holder.text = $"Total: {0}";    
    }

    // Update is called once per frame
    void Update()
    {   
        Credit_Holder.text = $"Credits: {PlayerSystem.credits}";
        Bill_Holder.text = $"Total: {damage_cost + magazine_cost}";

        if (start)
        {
            if (PlayerSystem.FAMAS.available)
            {
                item_name = "FAMAS";               
            }
            if (PlayerSystem.AWP.available)
            {
                item_name = "AWP";
            }
            if (PlayerSystem.Six.available)
            {
                item_name = "Six";
            }

            weaponsys.Buy(item_name);
            switch (item_name)
            {
                case "AWP":
                    AWPButton_Buy.interactable = false;
                    AWPButton_Upgrade.interactable = true;
                    PlayerSystem.credits -= AWP_Price;
                    break;
                case "Six":
                    SixButton_Buy.interactable = false;
                    SixButton_Upgrade.interactable = true;
                    PlayerSystem.credits -= Six_Price;
                    break;
            }

            start = false;
        }        
    }

    //----------------Start of Buy Weapons----------------//
    public void Buy_AWP(Text message)
    {
        SoundManagerScript.PlaySound("Button");
        if (PlayerSystem.credits >= AWP_Price)
        {
            Debug.Log(PlayerSystem.credits);
            int current_credits = PlayerSystem.credits - AWP_Price;
            message.text = "Are you sure you want to buy AWP for " + AWP_Price + " Credits?";
            item_name = "AWP";                      
        }

        else
        {
            message.text = "Insufficient Credits";
            ConfirmButton.gameObject.SetActive(false);
            CancelButton.gameObject.SetActive(false);
            ReturnButton.gameObject.SetActive(true);
        }

        Buy_Pop_Up.SetActive(true);
    }

    public void Buy_Six(Text message)
    {
        SoundManagerScript.PlaySound("Button");
        if (PlayerSystem.credits >= Six_Price)
        {
            Debug.Log(PlayerSystem.credits);
            int current_credits = PlayerSystem.credits - Six_Price;
            message.text = "Are you sure you want to buy Six for " + Six_Price + " Credits?";
            item_name = "Six";
        }

        else
        {
            message.text = "Insufficient Credits";
            ConfirmButton.gameObject.SetActive(false);
            CancelButton.gameObject.SetActive(false);
            ReturnButton.gameObject.SetActive(true);
        }

        Buy_Pop_Up.SetActive(true);
    }

    public void Buy_Confirm()
    {
        SoundManagerScript.PlaySound("Button");
        weaponsys.Buy(item_name);
        switch (item_name)
        {
            case "AWP":
                AWPButton_Buy.interactable = false;
                AWPButton_Upgrade.interactable = true;
                PlayerSystem.credits -= AWP_Price;
                break;
            case "Six":
                SixButton_Buy.interactable = false;
                SixButton_Upgrade.interactable = true;
                PlayerSystem.credits -= Six_Price;
                break;
        }
        Buy_Pop_Up.SetActive(false);
    }

    public void Buy_Cancel(GameObject buy_pop_up)
    {
        SoundManagerScript.PlaySound("Button");
        buy_pop_up.SetActive(false);
    }
    //----------------End of Buy Weapons----------------//

    //----------------Start of Upgrade Weapons----------------//
    public void Upgrade_FAMAS()
    {
        SoundManagerScript.PlaySound("Button");
        item_name = "FAMAS";
        Weapon_Name.text = "FAMAS";
        Weapon_Image.sprite = FAMAS_Sprite;

        damage_up = (int) weaponsys.GetStats(item_name).damage_amount;
        damage_cost = Damage_Cost();

        magazine_up = (int)weaponsys.GetStats(item_name).magazine_size;
        magazine_cost = Magazine_Cost();

        Upgrade_Pop_Up.SetActive(true);

        Damage_Image.fillAmount = (float) weaponsys.GetStats("FAMAS").damage_amount / 100;
        Damage_Label.text = (weaponsys.GetStats("FAMAS").damage_amount).ToString() + " / 100";
        Magazine_Image.fillAmount = (float) weaponsys.GetStats("FAMAS").magazine_size / 50;
        Magazine_Label.text = (weaponsys.GetStats("FAMAS").magazine_size).ToString() + " / 50";
    }

    public void Upgrade_AWP()
    {
        SoundManagerScript.PlaySound("Button");
        item_name = "AWP";
        Weapon_Name.text = "AWP";
        Weapon_Image.sprite = AWP_Sprite;

        damage_up = (int)weaponsys.GetStats(item_name).damage_amount;
        damage_cost = Damage_Cost();

        magazine_up = (int)weaponsys.GetStats(item_name).magazine_size;
        magazine_cost = Magazine_Cost();

        Upgrade_Pop_Up.SetActive(true);

        Damage_Image.fillAmount = (float)weaponsys.GetStats("AWP").damage_amount / 100;
        Damage_Label.text = (weaponsys.GetStats("AWP").damage_amount).ToString() + " / 100";
        Magazine_Image.fillAmount = (float) weaponsys.GetStats("AWP").magazine_size / 50;
        Magazine_Label.text = (weaponsys.GetStats("AWP").magazine_size).ToString() + " / 50";
    }

    public void Upgrade_Six()
    {
        SoundManagerScript.PlaySound("Button");
        item_name = "Six";
        Weapon_Name.text = "Six";
        Weapon_Image.sprite = Six_Sprite;

        damage_up = (int)weaponsys.GetStats(item_name).damage_amount;
        damage_cost = Damage_Cost();

        magazine_up = (int)weaponsys.GetStats(item_name).magazine_size;
        magazine_cost = Magazine_Cost();

        Upgrade_Pop_Up.SetActive(true);

        Damage_Image.fillAmount = (float)weaponsys.GetStats("Six").damage_amount / 100;
        Damage_Label.text = (weaponsys.GetStats("Six").damage_amount).ToString() + " / 100";
        Magazine_Image.fillAmount = (float) weaponsys.GetStats("Six").magazine_size / 50;
        Magazine_Label.text = (weaponsys.GetStats("Six").magazine_size).ToString() + " / 50";
    }

    public void More_Damage()
    {
        SoundManagerScript.PlaySound("Button");
        if (damage_up < 100)
        {
            damage_up += 10;
            damage_cost += Damage_Cost();

            if (damage_cost + magazine_cost > PlayerSystem.credits)
            {               
                damage_cost -= Damage_Cost();
                damage_up -= 10;
            }            
        }

        Damage_Image.fillAmount = (float)damage_up / 100;
        Damage_Label.text = (damage_up).ToString() + " / 100";
    }

    public void Less_Damage()
    {
        SoundManagerScript.PlaySound("Button");
        damage_cost -= Damage_Cost();
        damage_up -= 10;

        if (damage_up < weaponsys.GetStats(item_name).damage_amount)
        {
            damage_up += 10;
            damage_cost += Damage_Cost();
        }

        Damage_Image.fillAmount = (float)damage_up / 100;
        Damage_Label.text = (damage_up).ToString() + " / 100";
    }

    public void More_Magazine()
    {
        SoundManagerScript.PlaySound("Button");
        if (magazine_up < 50)
        {
            magazine_up += 10;
            magazine_cost += Magazine_Cost();

            if (magazine_cost + damage_cost > PlayerSystem.credits)
            {                
                magazine_cost -= Magazine_Cost();
                magazine_up -= 10;
            }
        }

        Magazine_Image.fillAmount = (float) magazine_up / 50;
        Magazine_Label.text = (magazine_up).ToString() + " / 50";
    }

    public void Less_Magazine()
    {
        SoundManagerScript.PlaySound("Button");
        magazine_cost -= Magazine_Cost();
        magazine_up -= 10;

        if (magazine_up < weaponsys.GetStats(item_name).magazine_size)
        {
            magazine_up += 10;
            magazine_cost += Magazine_Cost();            
        }

        Magazine_Image.fillAmount = (float) magazine_up / 50;
        Magazine_Label.text = (magazine_up).ToString() + " / 50";
    }

    public void Upgrade_Confirm(GameObject upgrade_pop_up)
    {
        SoundManagerScript.PlaySound("Button");
        weaponsys.Upgrade(item_name, damage_up, damage_cost, magazine_up, magazine_cost);
        PlayerSystem.credits -= damage_cost + magazine_cost;
        upgrade_pop_up.SetActive(false);
    }

    public void Upgrade_Cancel(GameObject upgrade_pop_up)
    {
        SoundManagerScript.PlaySound("Button");
        damage_up = 0;
        damage_cost = 0;
        upgrade_pop_up.SetActive(false);
    }

    //Upgrade Damage Prices
    int Damage_Cost()
    {
        int damage_cost = 0;

        switch (damage_up)
        {
            case 10: damage_cost = 0; break;
            case 20: damage_cost = 300; break;
            case 30: damage_cost = 500; break;
            case 40: damage_cost = 800; break;
            case 50: damage_cost = 1000; break;
            case 60: damage_cost = 1200; break;
            case 70: damage_cost = 1400; break;
            case 80: damage_cost = 1600; break;
            case 90: damage_cost = 2000; break;
            case 100: damage_cost = 2500; break;
        }

        if (damage_up == weaponsys.GetStats(item_name).damage_amount){
            damage_cost = 0;           
        }
        
        return damage_cost;
    }

    //Upgrade Magazine Prices
    int Magazine_Cost()
    {
        int magazine_cost = 0;

        switch (magazine_up)
        {
            case 10: magazine_cost = 0; break;
            case 20: magazine_cost = 200; break;
            case 30: magazine_cost = 1000; break;
            case 40: magazine_cost = 1500; break;
            case 50: magazine_cost = 2000; break;
        }

        if (magazine_up == weaponsys.GetStats(item_name).magazine_size)
        {
            magazine_cost = 0;
        }

        return magazine_cost;
    }
    //----------------End of Upgrade Weapons----------------//
}
