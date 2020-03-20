using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

// UIの共通的な部分の処理を行うクラス
public class UIManager : MonoBehaviour
{
    // ブラックアウト用のイメージ
    [SerializeField] private Image black;

    // フェードインする　遅延、継続時間
    public void BlackFadeIn(float delay, float duration)
    {
        StartCoroutine(FadeInCor(delay, duration));
    }

    // 実際のフェードインアニメーション
    private IEnumerator FadeInCor(float delay, float duration)
    {
        // 暗幕をアクティブ
        black.gameObject.SetActive(true);
        // 透明じゃなくする
        black.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
        // 遅延
        yield return new WaitForSeconds(delay);
        // 透明にする
        black.DOFade(0.0f, duration);
        // 透明になったら非アクティブ
        yield return new WaitForSeconds(duration);
        black.gameObject.SetActive(false);
    }

    // フェードアウトする　遅延、継続時間
    public void BlackFadeOut(float delay, float dration)
    {
        StartCoroutine(FadeOutCor(delay, dration));
    }

    // 実際のフェードアウトアニメーション
    private IEnumerator FadeOutCor(float delay, float duration)
    {
        // 暗幕をアクティブ
        black.gameObject.SetActive(true);
        // 念のため、透明にする
        black.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        // 遅延
        yield return new WaitForSeconds(delay);
        // 暗くしていく
        black.DOFade(1.0f, duration);
        yield return new WaitForSeconds(duration);
    }
}
