using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using DG.Tweening;

public class MyButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    private SVGImage highLight;
    [SerializeField] private Color defaultColor = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    [SerializeField] private Color enterColor = new Color(1.0f, 1.0f, 1.0f, 0.5f);
    [SerializeField] private Color downColor = new Color(0.8f, 0.8f, 0.8f, 0.5f);
    [SerializeField] private UnityEvent clickEvent;
    [SerializeField] private SoundEffect clickSound;
    private bool entered = false;

    private float colorTime = 0.1f;

    private void Start()
    {
        highLight = transform.GetChild(0).GetComponent<SVGImage>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        entered = true;
        highLight.DOColor(enterColor, colorTime);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        entered = false;
        highLight.DOColor(defaultColor, colorTime);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject.FindWithTag("SoundEffect").GetComponent<SoundEffecter>().SoundEffect(clickSound);
        clickEvent.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        highLight.DOColor(downColor, colorTime);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (entered)
        {
            highLight.DOColor(enterColor, colorTime);
        }
        else
        {
            highLight.DOColor(defaultColor, colorTime);
        }
    }
}
