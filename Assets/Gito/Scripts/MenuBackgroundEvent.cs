using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

// メニューが開いているときの、なにもない部分のイベント (基本はメニューを閉じる)
public class MenuBackgroundEvent : MonoBehaviour, IPointerClickHandler
{
    // クリックしたときに呼ぶ関数
    [SerializeField] private UnityEvent clickEvent;
    // クリックされたら効果音を鳴らして関数を呼ぶ
    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject.FindWithTag("SoundEffect").GetComponent<SoundEffecter>().SoundEffect(SoundEffect.Cancel);
        clickEvent.Invoke();
    }

}
