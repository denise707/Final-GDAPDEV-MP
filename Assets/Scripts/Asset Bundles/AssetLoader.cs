using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssetLoader : MonoBehaviour
{
    public AssetBundleManager assetManager;
    public Image imageHolder;

    // Start is called before the first frame update
    void Start()
    {
        Sprite bg = assetManager.GetAsset<Sprite>("textures", "bg");
        imageHolder.sprite = bg;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
