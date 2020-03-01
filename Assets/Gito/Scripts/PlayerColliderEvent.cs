using UnityEngine;
using UnityEngine.Events;

public class PlayerColliderEvent : MonoBehaviour
{
    [SerializeField] private GameObject deathEffectPrefab;
    [SerializeField] private UnityEvent clearEvent, faildEvent;

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
                gameObject.SetActive(false);
                faildEvent.Invoke();
                break;
            case "Clear":
                clearEvent.Invoke();
                break;
        }
    }
}
