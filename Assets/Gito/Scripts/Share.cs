using UnityEngine;
using System.Text;

// Twitterでシェア
public class Share : MonoBehaviour
{
    // ツイート
    public void Tweet()
    {
        // ツイートする文字列を組み立てる
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder
            .AppendLine("「Backwards 〜後ろ向きに進むけど、どれだけ回避できる？」");
        if (Difficulty.GetLevel() > 1)
        {
            stringBuilder.Append("レベル").Append(Difficulty.GetLevel()).AppendLine("までクリアした！");
        }
        // ツイート
        naichilab.UnityRoomTweet.Tweet("unity1week_backwards", stringBuilder.ToString(), "unityroom", "unity1week");
    }
}
