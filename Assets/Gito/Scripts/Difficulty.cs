using UnityEngine;

// レベルと難易度を管理しているクラス
public class Difficulty : MonoBehaviour
{
    // レベル
    private static int level = 1;
    // レベル1の列と行
    [SerializeField] private int lv1Column = 4, lv1Row = 5;
    // 今回の列と行
    private int column, row;
    // レベル1のプレイヤーのスピード
    [SerializeField] private float playerSpeed = 1.0f;

    // 列と行をレベルによって決定
    private void Awake()
    {
        // レベルを取得
        level = SaveManager.GetLevel();
        // レベル1のときはレベル1用の難易度 
        if (level == 1)
        {
            column = lv1Column;
            row = lv1Row;
        }
        else // レベルが2以降
        {
            // 4レベルに1列増える
            column = lv1Column + level / 4;
            // 1レベルに一行増えるが、列が増えるとき行も減る (2, 3, 4, 3, 4, 5, 6, 4, 5, 6, 7, 5...)
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
    // レベルを取得する関数
    public static int GetLevel()
    {
        return level;
    }
    // 今回の列を取得
    public int GetColumn()
    {
        return column;
    }
    // 今回の行を取得
    public int GetRow()
    {
        return row;
    }
    // 今回のプレイヤーのスピードを取得
    public float GetPlayerSpeed()
    {
        return playerSpeed;
    }
}
