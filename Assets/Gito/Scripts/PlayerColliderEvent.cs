using UnityEngine;
using UnityEngine.Events;

// プレイヤーの当たり判定クラス
public class PlayerColliderEvent : MonoBehaviour
{
    // 障害物にぶつかったときのエフェクト
    [SerializeField] private GameObject deathEffectPrefab;
    // クリアしたときに呼ぶ関数、失敗したときに呼ぶ関数
    [SerializeField] private UnityEvent clearEvent, faildEvent;

    // 衝突検知
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy": // ぶつかったのが障害物
                // エフェクトを生成
                Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
                // プレイヤーを非アクティブにして消す
                gameObject.SetActive(false);
                // 失敗時の関数を呼ぶ
                faildEvent.Invoke();
                break;
            case "Clear": // ぶつかったのがクリア判定用のオブジェクト
                // クリア時の関数を呼ぶ
                clearEvent.Invoke();
                break;
        }
    }
}
