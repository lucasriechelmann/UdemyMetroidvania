using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    float _bulletSpeed = 10f;        
    [SerializeField]
    Vector2 _direction;
    [SerializeField]
    ParticleSystem _shotImpact;

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
    }
    public void SetDirection(Vector2 direction) => _direction = direction;
}
