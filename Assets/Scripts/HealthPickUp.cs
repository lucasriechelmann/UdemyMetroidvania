using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    [SerializeField]
    int _healthAmount = 10;
    [SerializeField]
    GameObject _pickUpEffect;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealthController.Instance.HealPlayer(_healthAmount);
            Instantiate(_pickUpEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
