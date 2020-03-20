using UnityEngine;
using System.Collections;
using DG.Tweening;

// クリアか失敗したときの画面用のクラス
public class EndUI : MonoBehaviour
{
    // クリア画面を表示するときの遅延、失敗画面の遅延、表示アニメーションの時間、ボタンなどを表示するまでの間隔
    [SerializeField] private float clearShowDelay = 0.0f, failedShowDelay = 1.0f, showTime = 0.5f, nextScreenInterval = 1.0f;
    // クリアの画像、クリア画面のUI、失敗の画像、失敗画面のUI
    [SerializeField] private RectTransform clearImg, clearUI, failedImg, failedUI;

    // クリアを表示 表示にかかる時間を返す
    public float ClearIn()
    {
        gameObject.SetActive(true);
        StartCoroutine(ClearInCor());
        return clearShowDelay + showTime + nextScreenInterval;
    }

    // クリア表示のアニメーション
    private IEnumerator ClearInCor()
    {
        // 少し待つ
        yield return new WaitForSeconds(clearShowDelay);
        // ぽんっ！と表示
        clearImg.DOScale(Vector3.one, showTime).SetEase(Ease.OutBack);
        // 少ししたらUIを表示
        yield return new WaitForSeconds(showTime + nextScreenInterval);
        clearUI.DOMoveY(Screen.height / 2.0f, showTime).SetEase(Ease.OutExpo);
    }

    // 失敗を表示 表示にかかる時間を返す
    public float FailedIn()
    {
        gameObject.SetActive(true);
        StartCoroutine(FailednCor());
        return failedShowDelay + showTime + nextScreenInterval;
    }

    // 失敗表示のアニメーション
    private IEnumerator FailednCor()
    {
        // 少し待つ
        yield return new WaitForSeconds(failedShowDelay);
        // 上から落ちてくる
        failedImg.DOMoveY(Screen.height / 2.0f + Screen.height / 5.0f, showTime).SetEase(Ease.OutBack);
        // 少ししたらUIを表示
        yield return new WaitForSeconds(showTime + nextScreenInterval);
        failedUI.DOMoveY(Screen.height / 2.0f, showTime).SetEase(Ease.OutExpo);
    }
}
