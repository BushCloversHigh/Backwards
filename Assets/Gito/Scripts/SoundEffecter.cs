using UnityEngine;

// 効果音の種類の列挙
public enum SoundEffect
{
    Start, Decision, Cancel, Clear, Failed, Swing, Swap
}

// 効果音クラス
public class SoundEffecter : MonoBehaviour
{
    // オーディオソース
    private AudioSource audioSource;
    // それぞれのクリップ (列挙型通りにアタッチ)
    [SerializeField] private AudioClip[] clips;

    private void Start()
    {
        // 取得
        audioSource = GetComponent<AudioSource>();
    }

    // 効果音を鳴らす
    public void SoundEffect(SoundEffect soundEffect)
    {
        audioSource.PlayOneShot(clips[(int)soundEffect]);
    }
}
