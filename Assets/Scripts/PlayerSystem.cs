using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSystem : MonoBehaviour
{
    //Player
    public static float health = 100;

    public static int score = 0;

    public static int credits = 0;

    public static string player_name = "";

    //Weapon
    public static GunType FAMAS;
    public static GunType AWP;
    public static GunType Six;

    // Start is called before the first frame update
    void Awake()
    {
        health = 100;
        credits = 500;
        score = 0;

        //FAMAS Stats
        FAMAS.weapon_name = "FAMAS";
        FAMAS.damage_amount = 10f;
        FAMAS.magazine_size = 10;
        FAMAS.current_magazine = 10;
        FAMAS.color = "BLUE";
        FAMAS.available = true;

        //AWP Stats
        AWP.weapon_name = "AWP";
        AWP.damage_amount = 20f;
        AWP.magazine_size = 10;
        AWP.current_magazine = 10;
        AWP.color = "GREEN";
        AWP.available = false;

        //Six Stats
        Six.weapon_name = "Six";
        Six.damage_amount = 30f;
        Six.magazine_size = 20;
        Six.current_magazine = 20;
        Six.color = "RED";
        Six.available = false;
    }

    private void Start()
    {
        
    }
}
