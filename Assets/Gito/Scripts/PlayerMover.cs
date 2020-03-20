using UnityEngine;

// プレイヤーの移動クラス
public class PlayerMover : MonoBehaviour
{
    // プレイヤーが今どの列にいるのか、前の列
    private int posNum = 1, prevPos = 1;
    // 列数
    private int column;
    // リジットボディ
    private Rigidbody rb;
    // スピード
    private float speed;
    // 左右移動のスピード
    [SerializeField] private float playerStepSpeed = 5.0f;
    // 効果音
    private SoundEffecter soundEffecter;

    private void Start()
    {
        // コンポーネントを取得
        rb = GetComponent<Rigidbody>();
        soundEffecter = GameObject.FindWithTag("SoundEffect").GetComponent<SoundEffecter>();
    }

    // 移動開始用の関数
    public void MoveStart(float speed, int column)
    {
        this.speed = speed;
        this.column = column;
    }

    // 右を押すと"1"、左を押すと"-1"、押してないと"0"が返ってくる
    private int InputLeftRight()
    {
        if (Input.GetButtonDown("Right"))
        {
            return 1;
        }
        if (Input.GetButtonDown("Left"))
        {
            return -1;
        }
        return 0;
    }

    // 必要な時だけ呼び出してもらうUpdate
    public void PUpdate()
    {
        // 後ろに移動
        rb.velocity = new Vector3(0.0f, 0.0f, -speed);
        // 入力に応じて位置の変数の値を変える
        posNum += InputLeftRight();
        // はみ出さないように対策
        if(posNum < 0)
        {
            posNum = 0;
        }
        if(posNum > column - 1)
        {
            posNum = column - 1;
        }
        // 移動した時は効果音を鳴らす
        if (posNum != prevPos)
        {
            soundEffecter.SoundEffect(SoundEffect.Swing);
        }
        prevPos = posNum;
        // 実際に左右に移動
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, 0.75f + (posNum * 1.25f), playerStepSpeed * Time.deltaTime), 0.5f, transform.position.z);
    }
}
