using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    float _bulletSpeed = 10f;        
    [SerializeField]
    Vector2 _direction;
    [SerializeField]
    ParticleSystem _shotImpact;
    [SerializeField]
    int _damage = 10;

    Rigidbody2D _body;

    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
    }    
    void Update()
    {
        _body.linearVelocity = _direction * _bulletSpeed;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(_shotImpact, transform.position, Quaternion.identity);
        Destroy(gameObject);

        if (other.TryGetComponent(out EnemyHealthController enemy))
        {
            enemy.DamageEnemy(_damage);
        }

        if (other.TryGetComponent(out BossHealthController boss))
        {
            boss.TakeDamage(_damage);
        }
    }
    public void SetDirection(Vector2 direction) => _direction = direction;
}
