using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    [SerializeField]
    int _damage = 20;
    [SerializeField]
    bool _destroyOnDamage;
    [SerializeField]
    GameObject _destroyEffect;
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            DealDamage(other.gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            DealDamage(other.gameObject);
        }
    }
    void DealDamage(GameObject player)
    {
        player.GetComponent<PlayerHealthController>().DamagePlayer(_damage);

        if(_destroyOnDamage)
        {
            if (_destroyEffect != null)
            {
                Instantiate(_destroyEffect, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
