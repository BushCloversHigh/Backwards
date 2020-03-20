using UnityEngine;
using UnityEngine.UI;
using System.Text;

// レベル表示のクラス
public class LevelText : MonoBehaviour
{
    // 数字だけ？
    [SerializeField] private bool numOnly = false;

    // インスタンスされたときに適用
    private void Start()
    {
        StringBuilder stringBuilder = new StringBuilder();
        if (!numOnly)
        {
            stringBuilder.Append("- Level. ").Append(Difficulty.GetLevel()).Append(" -");
        }
        else
        {
            stringBuilder.Append(Difficulty.GetLevel());
        }
        GetComponent<Text>().text = stringBuilder.ToString();
    }
}
