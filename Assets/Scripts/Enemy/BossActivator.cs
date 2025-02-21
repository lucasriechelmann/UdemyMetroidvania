using UnityEngine;

public class BossActivator : MonoBehaviour
{
    [SerializeField]
    GameObject _bossToActivate;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            _bossToActivate.SetActive(true);
            Destroy(gameObject);
        }
    }
}
