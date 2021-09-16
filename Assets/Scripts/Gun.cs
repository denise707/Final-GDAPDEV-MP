using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject ReloadUI;
 
    protected OnScreenJoystick JoyStick;
    protected UICallbackScript UICallback;
    protected WeaponSystem WeaponSys;

    private GunType selectedWeapon;
    float bonus_damage = 0.0f;
    const float fire_rate = 0.3f;
    float ticks = 0.0f;
    float speed = 300f;

    //Drag Gestures
    private Touch trackedFinger1;
    bool dragged = false;

    //Pinch Gestures
    bool panned = false;
    bool already_panned = false;

    //Reload Variables
    public static bool reload = false;
    float reload_ticks = 0.0f;
    const float reload_time = 4.0f; 

    // Start is called before the first frame update
    void Start()
    {
        JoyStick = FindObjectOfType<OnScreenJoystick>();
        UICallback = FindObjectOfType<UICallbackScript>();
        WeaponSys = FindObjectOfType<WeaponSystem>();    
    }

    private void OnDrag(object sender, DragEventArgs e)
    {
        dragged = true;
        trackedFinger1 = e.TargerFinger;
    }

    private void OnSpread(object sender, SpreadEventArgs e)
    {
        if (e.DistanceDelta <= 0)
        {
            //panned = true;
            if (!already_panned)
            {
                panned = false;
                already_panned = true;
                SoundManagerScript.PlaySound("ReloadAgain");
                ReloadUI.SetActive(true);
            }
        }
    }

    private void OnDisable()
    {
        //Deregister our tap function for cleanup
        GestureManager.Instance.OnSpread -= OnSpread;
        GestureManager.Instance.OnDrag -= OnDrag;
    }

    private void OnEnable()
    {
        GestureManager.Instance.OnSpread += OnSpread;
        GestureManager.Instance.OnDrag += OnDrag;
    }

    // Update is called once per frame
    void Update()
    {

        if (WeaponSys.change_weapon)
        {
            ChangeGun();          
        }

        MoveCrosshair();

        if (dragged && !Options.aim_mode)
        {
            transform.position = trackedFinger1.position;
        }

        else
        {
            dragged = false;
        }

        ticks += Time.deltaTime;

        if (UICallback.shoot)
        {
            if((WeaponSys.GetStats(selectedWeapon.weapon_name).current_magazine > 0 && ticks >= fire_rate && !already_panned) || Options.unli_ammo)
            {
                Shoot();
                ticks = 0.0f;
                Debug.Log("Shoot");
            }

            else
            {
                UICallback.shoot = false;
            }
        }
        else
        {
            UICallback.shoot = false;
        }

        //if (panned && !already_panned)
        //{
        //    panned = false;
        //    already_panned = true;
        //    SoundManagerScript.PlaySound("ReloadAgain");
        //    ReloadUI.SetActive(true);
        //    Debug.Log("Pinch");
        //}

        if (already_panned)
        {
            reload_ticks += Time.deltaTime;
        }
    
        if (already_panned && reload_ticks >= reload_time)
        {
            SoundManagerScript.PlaySound("Reload");
            reload = true;
            already_panned = false;
            reload_ticks = 0.0f;
            ReloadUI.SetActive(false);
        }
    }

    void MoveCrosshair()
    {
        if (Options.aim_mode)
        {
            float x = JoyStick.JoystickAxis.x;
            float y = JoyStick.JoystickAxis.y;
            transform.Translate(x * speed * Time.deltaTime, y * speed * Time.deltaTime, 0);
        }  
    }

    void Shoot()
    {
        SoundManagerScript.PlaySound("Shoot");
        Ray ray = Camera.main.ScreenPointToRay(this.transform.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();

            if (enemy != null)
            {
                //Application of rock-paper-scissors mechanic
                bonus_damage = this.selectedWeapon.damage_amount * 0.3f;
                switch (selectedWeapon.color)
                {
                    case "BLUE":
                        if (enemy.type == "WASP") { enemy.TakeDamage(this.selectedWeapon.damage_amount + bonus_damage); }
                        else { enemy.TakeDamage(this.selectedWeapon.damage_amount); }
                        break;
                    case "GREEN":
                        if (enemy.type == "METAL ARM") { enemy.TakeDamage(this.selectedWeapon.damage_amount + bonus_damage); }
                        else { enemy.TakeDamage(this.selectedWeapon.damage_amount); }
                        break;
                    case "RED":
                        if (enemy.type == "INSECT") { enemy.TakeDamage(this.selectedWeapon.damage_amount + bonus_damage); }
                        else { enemy.TakeDamage(this.selectedWeapon.damage_amount); }
                        break;
                }
            }
        }
        WeaponSys.on_shoot = true;
        UICallback.shoot = false;
    }

    void ChangeGun()
    {
        this.selectedWeapon = WeaponSys.GetEquipped();      
    }
}
