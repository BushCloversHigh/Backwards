using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class LevelText : MonoBehaviour
{
    [SerializeField] private bool numOnly = false;
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
