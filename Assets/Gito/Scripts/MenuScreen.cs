using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;

public class MenuScreen : MonoBehaviour
{
    [SerializeField] private float moveDulation = 0.5f;
    [SerializeField] private float openY;
    [SerializeField] private float backAlpha = 0.5f;

    private Image back
    {
        get { return transform.GetChild(0).GetComponent<Image>(); }
    }

    private RectTransform panel
    {
        get { return transform.GetChild(1).GetComponent<RectTransform>(); }
    }

    public void Open()
    {
        gameObject.SetActive(true);
        back.DOFade(backAlpha, moveDulation);
        panel.DOMoveY(Screen.height / 2.0f + openY, moveDulation).SetEase(Ease.OutExpo);
    }

    public void Close()
    {
        StartCoroutine(CloseCor());
    }

    private IEnumerator CloseCor()
    {
        back.DOFade(0.0f, moveDulation);
        panel.DOMoveY(Screen.height / 2.0f + Screen.height, moveDulation);
        yield return new WaitForSeconds(moveDulation);
        gameObject.SetActive(false);
    }
}
