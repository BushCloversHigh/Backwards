using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.EventSystems;

// URLで飛ぶためのクラス
public class URLLauncher : MonoBehaviour, IPointerClickHandler
{
    // URL
    [SerializeField] private string url;
    // 別タブで開くためのライブラリ
    [DllImport("__Internal")]
    private static extern void OpenWindow(string url);

    // クリックされたら別タブでリンクに飛ぶ
    public void OnPointerClick(PointerEventData eventData)
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
#if UNITY_2017_2_OR_NEWER
            OpenWindow(url);
#else
				Application.ExternalEval ("var F = 0;if (screen.height > 500) {F = Math.round((screen.height / 2) - (250));}window.open('" + url + "','intent','left='+Math.round((screen.width/2)-(250))+',top='+F+',width=500,height=260,personalbar=no,toolbar=no,resizable=no,scrollbars=yes');");
#endif
        }
        else
        {
#if UNITY_EDITOR
            //URLを実行する（自動的にブラウザで開かれるはず）
            System.Diagnostics.Process.Start(url);
#else
				Debug.Log ("WebGL以外では実行できません。");
				Debug.Log (url);
#endif
        }
    }
}
