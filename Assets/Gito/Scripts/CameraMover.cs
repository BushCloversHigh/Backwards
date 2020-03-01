using UnityEngine;
using System.Collections;
using DG.Tweening;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private float backTime = 1.5f;
    [SerializeField] private float checkTime = 3.0f;
    [SerializeField] private float playerFollowSensitivity = 5.0f;

    private Transform player;
    private Transform cam;
    private float camOffsetZ;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        cam = Camera.main.transform;
    }

    public float StartCameraAnimation(float planeScaleX, int row, float interval)
    {
        StartCoroutine(StartCameraCor(planeScaleX, row, interval));
        return backTime + checkTime + row + 0.1f;
    }

    private IEnumerator StartCameraCor(float planeScaleX, int row, float interval)
    {
        yield return new WaitForSeconds(0.05f);
        Transform camera = Camera.main.transform;
        Vector3 cameraPos = camera.position;
        Vector3 cameraRot = camera.eulerAngles;
        camOffsetZ = cameraPos.z - player.position.z;
        camera.DOMove(new Vector3(planeScaleX * 2.0f, 7f, -row * interval), backTime).SetEase(Ease.InOutExpo);
        camera.DORotate(new Vector3(50.0f, 0.0f, 0.0f), backTime).SetEase(Ease.InOutExpo);
        yield return new WaitForSeconds(backTime + checkTime);
        camera.DOMove(cameraPos, row).SetEase(Ease.InSine);
        camera.DORotate(cameraRot, row).SetEase(Ease.InOutQuad);
        yield return new WaitForSeconds(row);
    }

    public float FaildCameraAnimation(float speed, float planeScaleX, int row, float interval)
    {
        StartCoroutine(FaildCameraCor(speed, planeScaleX, row, interval));
        return backTime + backTime + 0.1f;
    }

    private IEnumerator FaildCameraCor(float speed, float planeScaleX, int row, float interval)
    {
        Transform camera = Camera.main.transform;
        camera.transform.parent = null;
        camera.DOShakeRotation(1.0f, 50.0f, 10, 360.0f, true);
        camera.DOMoveZ(camera.position.z - backTime * speed, backTime).SetEase(Ease.OutExpo);
        yield return new WaitForSeconds(backTime);
        camera.DOMove(new Vector3(planeScaleX * 2.0f, 7f, -row * interval), backTime).SetEase(Ease.InOutExpo);
        camera.DORotate(new Vector3(50.0f, 0.0f, 0.0f), backTime).SetEase(Ease.InOutExpo);
        yield return new WaitForSeconds(backTime);
    }

    public float ClearCameraAnimation(float speed, float planeScaleX, int row, float interval)
    {
        StartCoroutine(ClearCameraCor(speed, planeScaleX, row, interval));
        return backTime / 2.0f + backTime + 0.1f;
    }

    private IEnumerator ClearCameraCor(float speed, float planeScaleX, int row, float interval)
    {
        float time1 = backTime / 2.0f;
        float time2 = backTime;
        Transform camera = Camera.main.transform;
        camera.transform.parent = null;
        camera.DOMoveZ(camera.position.z - backTime * speed, backTime).SetEase(Ease.OutExpo);
        yield return new WaitForSeconds(time1);
        camera.DOMove(new Vector3(planeScaleX * 2.0f, 7f, -row * interval), time2).SetEase(Ease.InOutExpo);
        camera.DORotate(new Vector3(50.0f, 0.0f, 0.0f), time2).SetEase(Ease.InOutExpo);
        yield return new WaitForSeconds(time2);
    }

    public void PLateUpdate()
    {
        cam.position = new Vector3(Mathf.Lerp(cam.position.x, player.position.x, playerFollowSensitivity * Time.deltaTime), cam.position.y, player.position.z + camOffsetZ);
    }
}
