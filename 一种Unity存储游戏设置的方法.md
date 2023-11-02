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


[建议搭配可序列化字典使用](https://discussions.unity.com/t/solved-how-to-serialize-dictionary-with-unity-serialization-system/71474)

``````c#
[Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField]
    private List<TKey> keys = new List<TKey>();

    [SerializeField]
    private List<TValue> values = new List<TValue>();

    // save the dictionary to lists
    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();
        foreach (KeyValuePair<TKey, TValue> pair in this)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }

    // load dictionary from lists
    public void OnAfterDeserialize()
    {
        this.Clear();

        if (keys.Count != values.Count)
            throw new System.Exception(string.Format("there are {0} keys and {1} values after deserialization. Make sure that both key and value types are serializable."));

        for (int i = 0; i < keys.Count; i++)
            this.Add(keys[i], values[i]);
    }
}
``````
