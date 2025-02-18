using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    [SerializeField]
    int _damage = 20;
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("OnCollisionEnter2D");
            DealDamage(other.gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("OnCollisionEnter2D");
            DealDamage(other.gameObject);
        }
    }
    void DealDamage(GameObject player)
    {
        Debug.Log(player.name);
        player.GetComponent<PlayerHealthController>().DamagePlayer(_damage);
    }
}
