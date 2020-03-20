using UnityEngine;
using System.Collections;
using DG.Tweening;

// カメラ移動のスクリプト
public class CameraMover : MonoBehaviour
{
    // 後ろに下がっていく時間
    [SerializeField] private float backTime = 1.5f;
    // 止まって配置を確認する時間
    [SerializeField] private float checkTime = 3.0f;
    // プレイヤーの左右移動に追従する感度
    [SerializeField] private float playerFollowSensitivity = 5.0f;

    // プレイヤーとカメラのトランスフォーム
    private Transform player;
    private Transform cam;
    // カメラとプレイヤーのオフセット
    private float camOffsetZ;

    private void Start()
    {
        // プレイヤーのトランスフォームを代入
        player = GameObject.FindWithTag("Player").transform;
        // カメラのトランスフォームを代入
        cam = Camera.main.transform;
    }

    // ゲームスタート時のカメラアニメーション用の関数 (後ろに引いていく) 要する時間を返す
    public float StartCameraAnimation(float planeScaleX, int row, float interval)
    {
        // アニメーションのコルーチンを呼び出す
        StartCoroutine(StartCameraCor(planeScaleX, row, interval));
        return backTime + checkTime + row + 0.1f;
    }

    // 後ろに引いて、障害物の配置を確認し、元の位置に戻るフロー
    private IEnumerator StartCameraCor(float planeScaleX, int row, float interval)
    {
        // 一瞬待つ
        yield return new WaitForSeconds(0.05f);
        // カメラの位置と回転を保持
        Vector3 cameraPos = cam.position;
        Vector3 cameraRot = cam.eulerAngles;
        // カメラとプレイヤーの位置関係を代入
        camOffsetZ = cameraPos.z - player.position.z;
        // 障害物の配置を確認するところにカメラを移動・少し下を向く
        cam.DOMove(new Vector3(planeScaleX * 2.0f, 7f, -row * interval), backTime).SetEase(Ease.InOutExpo);
        cam.DORotate(new Vector3(50.0f, 0.0f, 0.0f), backTime).SetEase(Ease.InOutExpo);
        // 配置を確認する時間
        yield return new WaitForSeconds(backTime + checkTime);
        // 元の位置、回転に戻す
        cam.DOMove(cameraPos, row).SetEase(Ease.InSine);
        cam.DORotate(cameraRot, row).SetEase(Ease.InOutQuad);
        // 元の位置に戻ったら終わり
        yield return new WaitForSeconds(row);
    }

    // 失敗時のアニメーション用の関数 (答え合わせ)
    public float FaildCameraAnimation(float speed, float planeScaleX, int row, float interval)
    {
        StartCoroutine(FaildCameraCor(speed, planeScaleX, row, interval));
        return backTime + backTime + 0.1f;
    }

    // 障害物の配置を確認できるところにカメラを移動するフロー
    private IEnumerator FaildCameraCor(float speed, float planeScaleX, int row, float interval)
    {
        // カメラを揺らす
        cam.DOShakeRotation(1.0f, 50.0f, 10, 360.0f, true);
        // 少しそのまま進む
        cam.DOMoveZ(cam.position.z - backTime * speed, backTime).SetEase(Ease.OutExpo);
        yield return new WaitForSeconds(backTime);
        // カメラを障害物が確認できるところに移動
        cam.DOMove(new Vector3(planeScaleX * 2.0f, 7f, -row * interval), backTime).SetEase(Ease.InOutExpo);
        cam.DORotate(new Vector3(50.0f, 0.0f, 0.0f), backTime).SetEase(Ease.InOutExpo);
        yield return new WaitForSeconds(backTime);
    }

    // クリア時のアニメーション用の関数
    public float ClearCameraAnimation(float speed, float planeScaleX, int row, float interval)
    {
        StartCoroutine(ClearCameraCor(speed, planeScaleX, row, interval));
        return backTime / 2.0f + backTime + 0.1f;
    }

    // クリア時のカメラ移動のフロー
    private IEnumerator ClearCameraCor(float speed, float planeScaleX, int row, float interval)
    {
        // 後ろに下がる時間と上に上がる時間
        float time1 = backTime / 2.0f;
        float time2 = backTime;
        // 少しそのまま進む
        cam.DOMoveZ(cam.position.z - backTime * speed, backTime).SetEase(Ease.OutExpo);
        // 後ろに下がる時間の半分だけ待つ
        yield return new WaitForSeconds(time1);
        // 障害物の配置を確認できる位置へ
        cam.DOMove(new Vector3(planeScaleX * 2.0f, 7f, -row * interval), time2).SetEase(Ease.InOutExpo);
        cam.DORotate(new Vector3(50.0f, 0.0f, 0.0f), time2).SetEase(Ease.InOutExpo);
        yield return new WaitForSeconds(time2);
    }

    // Update関数から呼び出してもらう
    public void PLateUpdate()
    {
        // プレイヤーに追従
        cam.position = new Vector3(Mathf.Lerp(cam.position.x, player.position.x, playerFollowSensitivity * Time.deltaTime), cam.position.y, player.position.z + camOffsetZ);
    }
}
