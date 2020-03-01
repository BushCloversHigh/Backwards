using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class EndUI : MonoBehaviour
{
    [SerializeField] private float clearShowDelay = 0.0f, failedShowDelay = 1.0f, showTime = 0.5f, nextScreenInterval = 1.0f;
    [SerializeField] private RectTransform clearImg, clearUI, failedImg, failedUI;

    public float ClearIn()
    {
        gameObject.SetActive(true);
        StartCoroutine(ClearInCor());
        return clearShowDelay + showTime + nextScreenInterval;
    }

    private IEnumerator ClearInCor()
    {
        yield return new WaitForSeconds(clearShowDelay);
        clearImg.DOScale(Vector3.one, showTime).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(showTime + nextScreenInterval);
        clearUI.DOMoveY(Screen.height / 2.0f, showTime).SetEase(Ease.OutExpo);
    }

    public float FailedIn()
    {
        gameObject.SetActive(true);
        StartCoroutine(FailednCor());
        return failedShowDelay + showTime + nextScreenInterval;
    }

    private IEnumerator FailednCor()
    {
        yield return new WaitForSeconds(failedShowDelay);
        failedImg.DOMoveY(Screen.height / 2.0f + Screen.height / 5.0f, showTime).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(showTime + nextScreenInterval);
        failedUI.DOMoveY(Screen.height / 2.0f, showTime).SetEase(Ease.OutExpo);
    }
}
