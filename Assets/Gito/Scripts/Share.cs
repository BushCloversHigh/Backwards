using UnityEngine;
using System.Text;

public class Share : MonoBehaviour
{
    public void Tweet()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder
            .AppendLine("「Backwards 〜後ろ向きに進むけど、どれだけ回避できる？」");
        if (Difficulty.GetLevel() > 1)
        {
            stringBuilder.Append("レベル").Append(Difficulty.GetLevel()).AppendLine("までクリアした！");
        }
        naichilab.UnityRoomTweet.Tweet("unity1week_backwards", stringBuilder.ToString(), "unityroom", "unity1week");
    }
}
