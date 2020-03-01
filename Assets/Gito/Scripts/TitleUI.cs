using UnityEngine;
using DG.Tweening;

public class TitleUI : MonoBehaviour
{
    [SerializeField] private float titleMoveTime;

    public float TitleSkip()
    {
        RectTransform title = transform.GetChild(0).GetComponent<RectTransform>();
        transform.GetChild(1).gameObject.SetActive(true);
        title.DOScale(Vector3.zero, titleMoveTime).SetEase(Ease.InBack);
        return titleMoveTime + 0.1f;
    }

    public void TitleFalse()
    {
        gameObject.SetActive(false);
    }
}
