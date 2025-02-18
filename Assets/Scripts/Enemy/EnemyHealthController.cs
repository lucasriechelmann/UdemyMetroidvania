using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField]
    int _maxHealth = 100;
    [SerializeField]
    GameObject _deathEffect;
    public void DamageEnemy(int damageAmount)
    {
        _maxHealth -= damageAmount;

        if(_maxHealth <= 0)
        {
            if(_deathEffect != null)
                Instantiate(_deathEffect, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
