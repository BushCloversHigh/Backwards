using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image black;

    public void BlackFadeIn(float delay, float dulation)
    {
        StartCoroutine(FadeInCor(delay, dulation));
    }

    private IEnumerator FadeInCor(float delay, float dulation)
    {
        black.gameObject.SetActive(true);
        black.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
        yield return new WaitForSeconds(delay);
        black.DOFade(0.0f, dulation);
        yield return new WaitForSeconds(dulation);
        black.gameObject.SetActive(false);
    }

    public void BlackFadeOut(float delay, float dulation)
    {
        StartCoroutine(FadeOutCor(delay, dulation));
    }

    private IEnumerator FadeOutCor(float delay, float dulation)
    {
        black.gameObject.SetActive(true);
        black.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        yield return new WaitForSeconds(delay);
        black.DOFade(1.0f, dulation);
        yield return new WaitForSeconds(dulation);
    }
}
