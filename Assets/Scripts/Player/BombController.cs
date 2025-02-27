using UnityEngine;

public class BombController : MonoBehaviour
{
    [SerializeField]
    float _timeToExplode = 0.5f;
    [SerializeField]
    GameObject _explosion;
    [SerializeField]
    float _blastRange = 1.5f;
    [SerializeField]
    LayerMask _whatIsDestructible;
    [SerializeField]
    int _damage = 25;
    [SerializeField]
    LayerMask _whatIsDamagable;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _timeToExplode -= Time.deltaTime;

        if (_timeToExplode <= 0)
        {
            Explode();
        }
    }
    void Explode()
    {
        if(_explosion != null)
            Instantiate(_explosion, transform.position, transform.rotation);

        Destroy(gameObject);

        DestroyObjects();
        DamageEnemies();
    }
    void DestroyObjects()
    {
        Collider2D[] objectsToDestroy = Physics2D.OverlapCircleAll(transform.position, _blastRange, _whatIsDestructible);

        foreach (Collider2D obj in objectsToDestroy)
        {
            Destroy(obj.gameObject);
        }
    }
    void DamageEnemies()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(transform.position, _blastRange, _whatIsDamagable);

        foreach (Collider2D enemy in enemiesToDamage)
        {
            enemy.GetComponent<EnemyHealthController>()?.DamageEnemy(_damage);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _blastRange);
    }
}
