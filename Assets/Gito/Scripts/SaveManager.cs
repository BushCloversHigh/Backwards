using UnityEngine;

public class SaveManager : MonoBehaviour
{
    readonly static string LEVEL_KEY = "level_key";

    public static void SaveLevel(int level)
    {
        PlayerPrefs.SetInt(LEVEL_KEY, level);
        PlayerPrefs.Save();
    }

    public static int GetLevel()
    {
        return PlayerPrefs.GetInt(LEVEL_KEY, 1);
    }
}
