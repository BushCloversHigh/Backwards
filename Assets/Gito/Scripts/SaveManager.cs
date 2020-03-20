using UnityEngine;

// データ保存管理してるクラス
public class SaveManager : MonoBehaviour
{
    // キー
    readonly static string LEVEL_KEY = "level_key";

    // レベルをセーブする
    public static void SaveLevel(int level)
    {
        PlayerPrefs.SetInt(LEVEL_KEY, level);
        PlayerPrefs.Save();
    }

    // レベルを取得する
    public static int GetLevel()
    {
        return PlayerPrefs.GetInt(LEVEL_KEY, 1);
    }
}
