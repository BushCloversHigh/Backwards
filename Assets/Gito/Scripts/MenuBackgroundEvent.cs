using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MenuBackgroundEvent : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private UnityEvent clickEvent;

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject.FindWithTag("SoundEffect").GetComponent<SoundEffecter>().SoundEffect(SoundEffect.Cancel);
        clickEvent.Invoke();
    }

}
