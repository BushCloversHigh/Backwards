using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MySceneManager: MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private float startFadeInTime = 1.0f;
    [SerializeField] private float fadeOutTime = 1.0f;

    private void Start()
    {
        uiManager.BlackFadeIn(0.5f, startFadeInTime);
    }

    public void SceneChange(int nextScene)
    {
        StartCoroutine(SceneChangeCor(nextScene));
    }

    private IEnumerator SceneChangeCor(int nextScene)
    {
        uiManager.BlackFadeOut(0.2f, fadeOutTime);
        yield return new WaitForSeconds(fadeOutTime + 0.5f);
        SceneManager.LoadScene(nextScene);
    }

}
