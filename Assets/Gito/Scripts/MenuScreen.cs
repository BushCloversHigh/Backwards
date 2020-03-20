using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;

// メニューの表示・非表示を行うクラス
public class MenuScreen : MonoBehaviour
{
    // アニメーションに要する時間
    [SerializeField] private float moveDulation = 0.5f;
    // 開くときのy座標
    [SerializeField] private float openY;
    // なにもない部分の透明度
    [SerializeField] private float backAlpha = 0.5f;

    // 取得用のプロパティ
    private Image back
    {
        get { return transform.GetChild(0).GetComponent<Image>(); }
    }

    private RectTransform panel
    {
        get { return transform.GetChild(1).GetComponent<RectTransform>(); }
    }

    // このメニューを開く
    public void Open()
    {
        gameObject.SetActive(true);
        back.DOFade(backAlpha, moveDulation);
        panel.DOMoveY(Screen.height / 2.0f + openY, moveDulation).SetEase(Ease.OutExpo);
    }

    // このメニューを閉じる
    public void Close()
    {
        StartCoroutine(CloseCor());
    }

    // 閉じるアニメーション
    private IEnumerator CloseCor()
    {
        back.DOFade(0.0f, moveDulation);
        panel.DOMoveY(Screen.height / 2.0f + Screen.height, moveDulation);
        yield return new WaitForSeconds(moveDulation);
        gameObject.SetActive(false);
    }
}
