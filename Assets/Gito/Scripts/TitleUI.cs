using UnityEngine;
using DG.Tweening;

// タイトルのUI
public class TitleUI : MonoBehaviour
{
    // タイトルアニメーションに要する時間
    [SerializeField] private float titleMoveTime;

    // タイトル画面を閉じるときのアニメーション
    public float TitleSkip()
    {
        // タイトル画面のUIを取得
        RectTransform title = transform.GetChild(0).GetComponent<RectTransform>();
        // クリックさせないためのオブジェクトをアクティブに
        transform.GetChild(1).gameObject.SetActive(true);
        // タイトル画面を閉じるアニメーション
        title.DOScale(Vector3.zero, titleMoveTime).SetEase(Ease.InBack);
        // アニメーションが終わったら、タイトルを非アクティブに
        Invoke("TitleFalse", titleMoveTime + 0.1f);
        return titleMoveTime + 0.1f;
    }

    // 非アクティブにするだけの関数
    public void TitleFalse()
    {
        gameObject.SetActive(false);
    }
}
