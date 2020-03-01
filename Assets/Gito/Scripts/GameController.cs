using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    private static bool isContinue = false;

    private bool isPlaying = false, isStarted = false;
    private Difficulty difficulty;
    private StageGenerator stageGenerator;
    private CameraMover cameraMover;
    [SerializeField] private GameObject player;
    private PlayerMover playerMover;
    [SerializeField] private LevelUI levelUI;
    [SerializeField] private TitleUI titleUI;
    [SerializeField] private EndUI endUI;

    private SoundEffecter soundEffecter;

    private MySceneManager mySceneManager
    {
        get { return GetComponent<MySceneManager>(); }
    }

    private void Start()
    {
        difficulty = GetComponent<Difficulty>();
        stageGenerator = GetComponent<StageGenerator>();
        cameraMover = GetComponent<CameraMover>();
        playerMover = player.GetComponent<PlayerMover>();
        soundEffecter = GameObject.FindWithTag("SoundEffect").GetComponent<SoundEffecter>();
        stageGenerator.Generate();

        if (isContinue)
        {
            titleUI.TitleFalse();
            StartGame();
        }
    }

    public void StartGame()
    {
        if (!isStarted)
        {
            isStarted = true;
            StartCoroutine(StartFlow());
        }
    }

    private IEnumerator StartFlow()
    {
        yield return new WaitForSeconds(titleUI.TitleSkip() + 0.5f);
        titleUI.TitleFalse();
        soundEffecter.SoundEffect(SoundEffect.Swap);
        yield return new WaitForSeconds(levelUI.LvCutIn(Difficulty.GetLevel()) - 0.5f);
        yield return new WaitForSeconds(cameraMover.StartCameraAnimation(stageGenerator.GetPlaneScaleX(), difficulty.GetRow(), stageGenerator.GetInterval()));
        isPlaying = true;
        playerMover.MoveStart(difficulty.GetPlayerSpeed(), difficulty.GetColumn());
    }

    public void GameFaildEnd()
    {
        isPlaying = false;
        soundEffecter.SoundEffect(SoundEffect.Failed);
        StartCoroutine(FaildEndFlow());
    }

    public void GameClearEnd()
    {
        isPlaying = false;
        soundEffecter.SoundEffect(SoundEffect.Clear);
        StartCoroutine(ClearEndFlow());
    }

    private IEnumerator FaildEndFlow()
    {
        endUI.FailedIn();
        yield return new WaitForSeconds(cameraMover.FaildCameraAnimation(difficulty.GetPlayerSpeed(), stageGenerator.GetPlaneScaleX(), difficulty.GetRow(), stageGenerator.GetInterval()));
    }

    private IEnumerator ClearEndFlow()
    {
        endUI.ClearIn();
        yield return new WaitForSeconds(cameraMover.ClearCameraAnimation(difficulty.GetPlayerSpeed(), stageGenerator.GetPlaneScaleX(), difficulty.GetRow(), stageGenerator.GetInterval()));
    }

    public void NextLevel()
    {
        difficulty.LevelUp();
        isContinue = true;
        mySceneManager.SceneChange(0);
    }

    public void RetryLevel()
    {
        isContinue = true;
        mySceneManager.SceneChange(0);
    }

    public void GoTitleFailed()
    {
        isContinue = false;
        mySceneManager.SceneChange(0);
    }

    public void GoTitleClear()
    {
        difficulty.LevelUp();
        isContinue = false;
        mySceneManager.SceneChange(0);
    }

    private void Update()
    {
        if (!isPlaying) return;
        playerMover.PUpdate();
        cameraMover.PLateUpdate();

    }

    private void LateUpdate()
    {
        if (!isPlaying) return;
    }
}
