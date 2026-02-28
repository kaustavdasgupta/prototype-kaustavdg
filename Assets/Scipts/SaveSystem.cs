using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private const string KEY = "CARD_MATCH_SAVE";

    public static void Save(GameSaveData data)
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(KEY, json);
        PlayerPrefs.Save();
    }

    public static GameSaveData Load()
    {
        if (!PlayerPrefs.HasKey(KEY))
            return null;

        string json = PlayerPrefs.GetString(KEY);
        return JsonUtility.FromJson<GameSaveData>(json);
    }

    public static void Clear()
    {
        PlayerPrefs.DeleteKey(KEY);
    }
}


