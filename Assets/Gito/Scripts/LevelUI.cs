using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private float lvMoveTime, lvStopTime;

    public float LvCutIn(int level)
    {
        StartCoroutine(CutInCor(level));
        return lvMoveTime + lvStopTime + lvMoveTime;
    }

    private IEnumerator CutInCor(int level)
    {
        int width = Screen.width, height = Screen.height;
        RectTransform lv = transform.GetChild(0).GetComponent<RectTransform>();
        RectTransform frame = lv.GetChild(0).GetComponent<RectTransform>();
        RectTransform num = lv.GetChild(1).GetComponent<RectTransform>();
        Text numText = num.GetComponent<Text>();
        numText.text = level.ToString();
        frame.DOMoveX(width / 2.0f, lvMoveTime).SetEase(Ease.OutExpo);
        num.DOMoveX(width / 2.0f - 5.0f, lvMoveTime).SetEase(Ease.OutExpo);
        yield return new WaitForSeconds(lvMoveTime + lvStopTime);
        lv.DOMove(new Vector3(width - width / 15.0f, height - height / 10.0f, 0.0f), lvMoveTime).SetEase(Ease.InOutQuad);
        lv.DOScale(Vector3.one * 0.4f, lvMoveTime).SetEase(Ease.InOutQuad);
        yield return new WaitForSeconds(lvMoveTime);
    }
}
