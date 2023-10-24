# 一种Unity存储游戏设置的方法

``````c#
using UnityEngine;

public class Settings
{
    
    public float musicVolume;
    public int difficultyLevel;

    private static Settings s_Instance;
    public static Settings Get()
    {
        if (!PlayerPrefs.HasKey("Settings"))
        {
            s_Instance = new Settings();
            Save();
        }
        else
        {
            s_Instance = JsonUtility.FromJson(PlayerPrefs.GetString("Settings"), typeof(Settings)) as Settings;
        }
        return s_Instance;
    }
    public static void Save()
    {
        PlayerPrefs.SetString("Settings", JsonUtility.ToJson(s_Instance));
    }
    public static void Reset()
    {
        s_Instance = new Settings();
        Save();
    }
}

``````

