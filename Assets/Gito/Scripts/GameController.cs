using UnityEngine;
using System.Collections;

// ゲームの進行管理などを行うクラス
public class GameController : MonoBehaviour
{
    // そのまま続行するかどうか
    private static bool isContinue = false;

    // プレイ中かどうか、始まったか
    private bool isPlaying = false, isStarted = false;
    // 難易度、ステージ生成、カメラ移動のクラス
    private Difficulty difficulty;
    private StageGenerator stageGenerator;
    private CameraMover cameraMover;
    // プレイヤー
    [SerializeField] private GameObject player;
    // プレイヤー移動のクラス
    private PlayerMover playerMover;
    // 表示非表示がからむUI
    [SerializeField] private LevelUI levelUI;
    [SerializeField] private TitleUI titleUI;
    [SerializeField] private EndUI endUI;
    // 効果音クラス
    private SoundEffecter soundEffecter;
    // シーンマネージャーのクラス呼び出し用のプロパティ
    private MySceneManager mySceneManager
    {
        get { return GetComponent<MySceneManager>(); }
    }

    private void Start()
    {
        // クラスを代入
        difficulty = GetComponent<Difficulty>();
        stageGenerator = GetComponent<StageGenerator>();
        cameraMover = GetComponent<CameraMover>();
        playerMover = player.GetComponent<PlayerMover>();
        soundEffecter = GameObject.FindWithTag("SoundEffect").GetComponent<SoundEffecter>();
        // ゲームステージを生成
        stageGenerator.Generate();
        // 前回から継続してるなら、UI消してスタート
        if (isContinue)
        {
            titleUI.TitleFalse();
            StartGame();
        }
    }

    // ゲーム開始用の関数 ボタンからイベントで呼び出してもらう
    public void StartGame()
    {
        // まだスタートしてなければ反応する
        if (!isStarted)
        {
            isStarted = true;
            StartCoroutine(StartFlow());
        }
    }

    // ゲーム開始時のUIのアニメーション
    private IEnumerator StartFlow()
    {
        // タイトル画面を消すアニメーションをする+0.5秒待つ
        yield return new WaitForSeconds(titleUI.TitleSkip() + 0.5f);
        // レベルのカットイン
        soundEffecter.SoundEffect(SoundEffect.Swap);
        yield return new WaitForSeconds(levelUI.LvCutIn(Difficulty.GetLevel()) - 0.5f);
        // 障害物の配置確認用のカメラアニメーション
        yield return new WaitForSeconds(cameraMover.StartCameraAnimation(stageGenerator.GetPlaneScaleX(), difficulty.GetRow(), stageGenerator.GetInterval()));
        // やっとプレイ開始
        isPlaying = true;
        // プレイヤーの移動開始　難易度に応じたスピードと列を送る
        playerMover.MoveStart(difficulty.GetPlayerSpeed(), difficulty.GetColumn());
    }

    // 失敗したときのイベント
    public void GameFaildEnd()
    {
        isPlaying = false;
        soundEffecter.SoundEffect(SoundEffect.Failed);
        StartCoroutine(FaildEndFlow());
    }

    // 成功した時のイベント
    public void GameClearEnd()
    {
        isPlaying = false;
        soundEffecter.SoundEffect(SoundEffect.Clear);
        StartCoroutine(ClearEndFlow());
    }

    // 失敗時のアニメーション
    private IEnumerator FaildEndFlow()
    {
        // 失敗画面を表示
        endUI.FailedIn();
        // 失敗用のカメラアニメーションを再生
        yield return new WaitForSeconds(cameraMover.FaildCameraAnimation(difficulty.GetPlayerSpeed(), stageGenerator.GetPlaneScaleX(), difficulty.GetRow(), stageGenerator.GetInterval()));
    }

    // 成功時のアニメーショn
    private IEnumerator ClearEndFlow()
    {
        // 成功画面を表示
        endUI.ClearIn();
        // 成功時のカメラアニメーションを再生
        yield return new WaitForSeconds(cameraMover.ClearCameraAnimation(difficulty.GetPlayerSpeed(), stageGenerator.GetPlaneScaleX(), difficulty.GetRow(), stageGenerator.GetInterval()));
    }

    // 次のレベルへのイベント
    public void NextLevel()
    {
        // レベルアップ
        difficulty.LevelUp();
        // 継続します
        isContinue = true;
        // シーンをリセット
        mySceneManager.SceneChange(0);
    }

    // リトライのイベント
    public void RetryLevel()
    {
        isContinue = true;
        mySceneManager.SceneChange(0);
    }

    // 失敗時のタイトル画面へ戻るイベント
    public void GoTitleFailed()
    {
        isContinue = false;
        mySceneManager.SceneChange(0);
    }

    // 成功時のタイトル画面へ戻る
    public void GoTitleClear()
    {
        difficulty.LevelUp();
        isContinue = false;
        mySceneManager.SceneChange(0);
    }

    private void Update()
    {
        // プレイ中はプレイヤーとカメラ移動のUpdate関数を呼び出す
        if (!isPlaying) return;
        playerMover.PUpdate();
        cameraMover.PLateUpdate();

    }
}
