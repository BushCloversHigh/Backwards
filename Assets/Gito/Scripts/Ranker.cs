using UnityEngine;
using UnityEngine.UI;

public class Ranker : MonoBehaviour
{
    public void Set(int rank, string name, int level)
    {
        transform.GetChild(0).GetComponent<Text>().text = rank.ToString();
        transform.GetChild(1).GetComponent<Text>().text = name;
        transform.GetChild(2).GetComponent<Text>().text = level.ToString();
    }
}
