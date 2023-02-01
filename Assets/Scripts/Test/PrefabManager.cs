using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class AssetManager
{
    private Dictionary<string, GameObject> prefabs;
    private Dictionary<string, Sprite> sprites;
    private List<Rune> runes;
    private AssetManager()
    {

    }

    private static AssetManager instance;

    public static AssetManager GetInstance()
    { 
        if (instance == null) 
        { 
            instance = new AssetManager(); 
        } 
        return instance; 
    }
}
