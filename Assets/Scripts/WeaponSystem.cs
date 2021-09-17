using UnityEngine;
using UnityEngine.UI;

public class WeaponSystem : MonoBehaviour
{
    //Weapon
    private GunType FAMAS;
    private GunType AWP;
    private GunType Six;

    //Weapon Holder (Used to show if equipped ot not)
    GameObject Weapon_Holder_1;
    GameObject Weapon_Holder_2;
    GameObject Weapon_Holder_3;

    GameObject Crosshair;

    //Current magazine of gun equipped 
    GameObject Magazine_Holder;

    //Button States
    Color pressed;
    Color normal;

    private GunType selected_weapon;
    public bool change_weapon = true;
    public bool on_shoot = false;
    bool update_values = false;

    // Start is called before the first frame update
    void Start()
    {
        Magazine_Holder = GameObject.FindGameObjectWithTag("MagazineHolder");

        Weapon_Holder_1 = GameObject.FindGameObjectWithTag("Weapon 1");
        Weapon_Holder_2 = GameObject.FindGameObjectWithTag("Weapon 2");
        Weapon_Holder_3 = GameObject.FindGameObjectWithTag("Weapon 3");

        Crosshair = GameObject.FindGameObjectWithTag("Crosshair");

        FAMAS = PlayerSystem.FAMAS;
        Debug.Log(FAMAS.weapon_name);

        AWP = PlayerSystem.AWP;

        Six = PlayerSystem.Six;

        //Set button colors
        normal = new Color(255f, 255f, 255f, 90f / 255f);
        pressed = new Color(0f, 0f, 0f, 0.5f);

        //default
        selected_weapon = FAMAS;
        Weapon_Holder_1.GetComponent<Image>().color = pressed;
        Magazine_Holder.GetComponent<Text>().text = FAMAS.current_magazine + " / " + FAMAS.magazine_size;
        Crosshair.GetComponent<Image>().color = Color.blue;
    }

    // Update is called once per frame
    void Update()
    {
        //Update current  magazine
        if(selected_weapon.weapon_name == "FAMAS")
        {
            if (on_shoot)
            {
                FAMAS.current_magazine -= 1;
            }

            if (Gun.reload)
            {
                FAMAS.current_magazine = FAMAS.magazine_size;
            }
            Magazine_Holder.GetComponent<Text>().text = FAMAS.current_magazine + " / " + FAMAS.magazine_size;
        }

        if (selected_weapon.weapon_name == "AWP")
        {
            if (on_shoot)
            {
                AWP.current_magazine -= 1;
            }

            if (Gun.reload)
            {
                AWP.current_magazine = AWP.magazine_size;
            }
            Magazine_Holder.GetComponent<Text>().text = AWP.current_magazine + " / " + AWP.magazine_size;
        }

        if (selected_weapon.weapon_name == "Six")
        {
            if (on_shoot)
            {
                Six.current_magazine -= 1;
            }

            if (Gun.reload)
            {
                Six.current_magazine = Six.magazine_size;
            }
            Magazine_Holder.GetComponent<Text>().text = Six.current_magazine + " / " + Six.magazine_size;
        }

        on_shoot = false;
        Gun.reload = false;

        //Update stats from upgrade
        if (update_values)
        {
            if(selected_weapon.weapon_name == "FAMAS")
            {
                selected_weapon = FAMAS;  
            }

            else if (selected_weapon.weapon_name == "AWP")
            {
                selected_weapon = AWP;
            }

            else if (selected_weapon.weapon_name == "Six")
            {
                selected_weapon = Six;
            }

            update_values = false;
        }
    }

    //Get currently equipped weapon
    public GunType GetEquipped()
    {
        return selected_weapon;
    }

    public void SelectFAMAS()
    {
        selected_weapon = FAMAS;
        change_weapon = true;
        Weapon_Holder_1.GetComponent<Image>().color = pressed;
        Weapon_Holder_2.GetComponent<Image>().color = normal;
        Weapon_Holder_3.GetComponent<Image>().color = normal;
        Debug.Log("Changed to FAMAS");

        Crosshair.GetComponent<Image>().color = Color.blue;
    }

    public void SelectAWP()
    {
        if (AWP.available)
        {
            selected_weapon = AWP;
            change_weapon = true;
            Weapon_Holder_1.GetComponent<Image>().color = normal;
            Weapon_Holder_2.GetComponent<Image>().color = pressed;
            Weapon_Holder_3.GetComponent<Image>().color = normal;
            Debug.Log("Changed to AWP");

            Crosshair.GetComponent<Image>().color = Color.green;
        }       
    }

    public void SelectSix()
    {
        if (Six.available)
        {
            selected_weapon = Six;
            change_weapon = true;
            Weapon_Holder_1.GetComponent<Image>().color = normal;
            Weapon_Holder_2.GetComponent<Image>().color = normal;
            Weapon_Holder_3.GetComponent<Image>().color = pressed;
            Debug.Log("Changed to Six");

            Crosshair.GetComponent<Image>().color = Color.red;
        }
    }

    public void Upgrade(string weap_name, int damage_up,int damage_cost, int magazine_up, int magazine_cost)
    {
        if(weap_name == "FAMAS")
        {
            FAMAS.damage_amount = damage_up;
            FAMAS.magazine_size = magazine_up;
            FAMAS.current_magazine = magazine_up;
            PlayerSystem.FAMAS = FAMAS;
        }

        else if (weap_name == "AWP")
        {
            AWP.damage_amount = damage_up;
            AWP.magazine_size = magazine_up;
            AWP.current_magazine = magazine_up;
            PlayerSystem.AWP = AWP;
        }

        else if (weap_name == "Six")
        {
            Six.damage_amount = damage_up; 
            Six.magazine_size = magazine_up;
            Six.current_magazine = magazine_up;
            PlayerSystem.Six = Six;
        }

        update_values = true;
    }

    public void Buy(string gun)
    {
        if(gun == "AWP")
        {
            AWP.available = true;
            Weapon_Holder_2.GetComponent<Image>().color = normal;
            Weapon_Holder_2.GetComponent<Button>().interactable = true;
            Weapon_Holder_2.GetComponent<Button>().transform.GetChild(0).gameObject.SetActive(true);
            PlayerSystem.AWP.available = true;
        }

        if (gun == "Six")
        {
            Six.available = true;
            Weapon_Holder_3.GetComponent<Image>().color = normal;
            Weapon_Holder_3.GetComponent<Button>().interactable = true;
            Weapon_Holder_3.GetComponent<Button>().transform.GetChild(0).gameObject.SetActive(true);
            PlayerSystem.Six.available = true;
        }
    }

    public GunType GetStats(string weap_name)
    {
        GunType gun = FAMAS;
        switch (weap_name)
        {
            case "FAMAS": gun = FAMAS; break;
            case "AWP": gun = AWP; break;
            case "Six": gun = Six; break;
        }
        return gun;
    }
}
