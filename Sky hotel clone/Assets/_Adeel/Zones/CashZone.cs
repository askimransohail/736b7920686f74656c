using _Adeel.Managers;
using _Adeel.Player;
using UnityEngine;

public class CashZone : MonoBehaviour
{
    [SerializeField] private int amount;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement playerMovement))
        {
            ScoreManager.Ins.AddScore(transform.position, amount);
            gameObject.SetActive(false);
        }
    }
}
