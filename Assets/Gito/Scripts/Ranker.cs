using UnityEngine;
using UnityEngine.UI;

// ランキングに掲載されるプレイヤーのクラス
public class Ranker : MonoBehaviour
{
    // 順位、名前、到達レベルを適用する
    public void Set(int rank, string name, int level)
    {
        transform.GetChild(0).GetComponent<Text>().text = rank.ToString();
        transform.GetChild(1).GetComponent<Text>().text = name;
        transform.GetChild(2).GetComponent<Text>().text = level.ToString();
    }
}
