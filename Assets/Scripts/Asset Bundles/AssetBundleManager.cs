using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

public class AssetBundleManager : MonoBehaviour
{
    public static AssetBundleManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            //Assign this instance of Gesture Manager
            Instance = this;
        }
        else
        {
            //Destroy other duplicates in scene
            Destroy(gameObject);
        }

    }

    public string BundleRootPath
    {
        get
        {
#if UNITY_EDITOR
            return Application.streamingAssetsPath;
#elif UNITY_ANDROID
            return Application.persistentDataPath;
#endif
        }
    }

    Dictionary<string, AssetBundle> LoadedBundles = new Dictionary<string, AssetBundle>();

    public AssetBundle LoadBundle(string bundle_name)
    {
        if (LoadedBundles.ContainsKey(bundle_name))
        {
            return LoadedBundles[bundle_name];
        }

        AssetBundle ret = AssetBundle.LoadFromFile(Path.Combine(BundleRootPath, bundle_name));

        if (ret == null)
        {
            Debug.LogError($"Failed to load {bundle_name}");
        }
        else
        {
            LoadedBundles.Add(bundle_name, ret);
        }

        return ret;    
    }   
    
    public T GetAsset<T>(string bundle_name, string asset) where T: Object
    {
        T ret = null;

        AssetBundle bundle = LoadBundle(bundle_name);

        if(bundle != null)
        {            
            ret = bundle.LoadAsset<T>(asset);
        }

        return ret;
    }

    public void UnloadAll()
    {
        foreach (KeyValuePair<string, AssetBundle> bundle in LoadedBundles)
        {
            bundle.Value.Unload(true);
        }
    }
}
