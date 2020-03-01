using UnityEngine;

public class Difficulty : MonoBehaviour
{
    // レベル
    private static int level = 0;
    // レベル1の列と行
    [SerializeField] private int lv1Column = 4, lv1Row = 5;
    // 今回の列と行
    private int column, row;

    [SerializeField] private float playerSpeed = 1.0f;

    // 列と行をレベルによって決定
    private void Awake()
    {
        level = SaveManager.GetLevel();
        if (level == 0)
        {
            column = 4;
            row = 3;
        }
        else
        {
            column = lv1Column + level / 4;
            int r = level / 4;
            r *= 2;
            row = lv1Row + level - r;
        }
    }

    // レベルアップ用の関数
    public void LevelUp()
    {
        SaveManager.SaveLevel(level + 1);
    }
    // レベルリセット用の関数
    public void ResetLevel()
    {
        SaveManager.SaveLevel(1);
        GetComponent<MySceneManager>().SceneChange(0);
    }

    public static int GetLevel()
    {
        return level;
    }

    // 取得する関数
    public int GetColumn()
    {
        return column;
    }
    // 取得する関数
    public int GetRow()
    {
        return row;
    }

    public float GetPlayerSpeed()
    {
        return playerSpeed;
    }
}
