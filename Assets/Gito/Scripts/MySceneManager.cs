using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

// シーン繊維用のクラス
public class MySceneManager: MonoBehaviour
{
    // フェードイン・フェードアウトに使う
    [SerializeField] private UIManager uiManager;
    // シーン読み込み時のフェードインの時間
    [SerializeField] private float startFadeInTime = 1.0f;
    // シーン遷移するときのフェードアウトの時間
    [SerializeField] private float fadeOutTime = 1.0f;

    // シーン開始時にフェードイン
    private void Start()
    {
        // 0.5秒間は暗いまま
        uiManager.BlackFadeIn(0.5f, startFadeInTime);
    }

    // シーンを変更
    public void SceneChange(int nextScene)
    {
        StartCoroutine(SceneChangeCor(nextScene));
    }

    private IEnumerator SceneChangeCor(int nextScene)
    {
        // フェードアウト
        uiManager.BlackFadeOut(0.2f, fadeOutTime);
        // フェードアウトし終わったらシーンをロード
        yield return new WaitForSeconds(fadeOutTime + 0.5f);
        SceneManager.LoadScene(nextScene);
    }

}
