using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using DG.Tweening;

// ボタンクラス
public class MyButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    // ハイライト
    private SVGImage highLight;
    // 通常時の色、マウスオーバー時の色、押したときの色
    [SerializeField] private Color defaultColor = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    [SerializeField] private Color enterColor = new Color(1.0f, 1.0f, 1.0f, 0.5f);
    [SerializeField] private Color downColor = new Color(0.8f, 0.8f, 0.8f, 0.5f);
    // クリックしたときに呼ぶ関数
    [SerializeField] private UnityEvent clickEvent;
    // クリック音
    [SerializeField] private SoundEffect clickSound;
    // マウスオーバーしてるか
    private bool entered = false;
    // 色が変わるのに要する時間
    private float colorTime = 0.1f;

    private void Start()
    {
        // ハイライトの画像を子オブジェクトから取得
        highLight = transform.GetChild(0).GetComponent<SVGImage>();
    }

    // マウスオーバーの色に
    public void OnPointerEnter(PointerEventData eventData)
    {
        entered = true;
        highLight.DOColor(enterColor, colorTime);
    }

    // マウスが外れたら、通常時の色に
    public void OnPointerExit(PointerEventData eventData)
    {
        entered = false;
        highLight.DOColor(defaultColor, colorTime);
    }

    // クリックしたら効果音を鳴らし、設定された関数を呼ぶ
    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject.FindWithTag("SoundEffect").GetComponent<SoundEffecter>().SoundEffect(clickSound);
        clickEvent.Invoke();
    }

    // ボタンを押し込んだときの色に
    public void OnPointerDown(PointerEventData eventData)
    {
        highLight.DOColor(downColor, colorTime);
    }

    // ボタンを押し込んで離したとき
    public void OnPointerUp(PointerEventData eventData)
    {
        // マウスオーバー中だったら、マウスーバージの色に
        if (entered)
        {
            highLight.DOColor(enterColor, colorTime);
        }
        else // マウスオーバーじゃなかったら、通常時の色に
        {
            highLight.DOColor(defaultColor, colorTime);
        }
    }
}
