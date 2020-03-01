using UnityEngine;

public enum SoundEffect
{
    Start, Decision, Cancel, Clear, Failed, Swing, Swap
}

public class SoundEffecter : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private AudioClip[] clips;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SoundEffect(SoundEffect soundEffect)
    {
        audioSource.PlayOneShot(clips[(int)soundEffect]);
    }
}
