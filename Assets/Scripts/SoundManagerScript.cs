using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip default_bgm, shoot_sfx, monster_sfx, button_sfx, reload_sfx, next_reload_sfx;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Awake()
    {
        //default_bgm = Resources.Load<AudioClip>("BGM_Default");
        //monster_sfx = Resources.Load<AudioClip>("Monster");
        //shoot_sfx = Resources.Load<AudioClip>("Shoot");
        //button_sfx = Resources.Load<AudioClip>("Button");
        //reload_sfx = Resources.Load<AudioClip>("Reload");
        //next_reload_sfx = Resources.Load<AudioClip>("ReloadAgain");

        audioSrc = GetComponent<AudioSource>();
    }

    private void Start()
    {
        default_bgm = AssetBundleManager.Instance.GetAsset<AudioClip>("sounds", "BGM_Default");
        default_bgm.LoadAudioData();

        monster_sfx = AssetBundleManager.Instance.GetAsset<AudioClip>("sounds", "Monster");
        monster_sfx.LoadAudioData();

        shoot_sfx = AssetBundleManager.Instance.GetAsset<AudioClip>("sounds", "Shoot");
        shoot_sfx.LoadAudioData();

        button_sfx = AssetBundleManager.Instance.GetAsset<AudioClip>("sounds", "Button");
        button_sfx.LoadAudioData();

        reload_sfx = AssetBundleManager.Instance.GetAsset<AudioClip>("sounds", "Reload");
        reload_sfx.LoadAudioData();

        next_reload_sfx = AssetBundleManager.Instance.GetAsset<AudioClip>("sounds", "ReloadAgain");
        next_reload_sfx.LoadAudioData();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void PlaySound(string clip)
    {
        if(AssetBundleManager.Instance.GetAsset<AudioClip>("sounds", "BGM_Default").loadState == AudioDataLoadState.Loaded)
        {
            switch (clip)
            {
                case "BGM_Default":
                    audioSrc.PlayOneShot(default_bgm);
                    break;
                case "Monster":
                    audioSrc.PlayOneShot(monster_sfx);
                    break;
                case "Shoot":
                    audioSrc.PlayOneShot(shoot_sfx);
                    break;
                case "Button":
                    audioSrc.PlayOneShot(button_sfx);
                    break;
                case "Reload":
                    audioSrc.PlayOneShot(reload_sfx);
                    break;
                case "ReloadAgain":
                    audioSrc.PlayOneShot(next_reload_sfx);
                    break;
            }
        }       
    }
}
