using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Forces")]
    [SerializeField]
    float _moveSpeed = 8f;
    [SerializeField]
    float _jumpForce = 20f;
    [Header("Ground Check")]
    [SerializeField]
    Transform _groundCheckPoint;
    [SerializeField]
    LayerMask _whatIsGround;
    [SerializeField]
    float _groundCheckRadius = 0.2f;
    [Header("Animations")]
    [SerializeField]
    Animator _animator;
    [Header("Bullet")]
    [SerializeField]
    BulletController _bulletController;
    [SerializeField]
    Transform _shotPoint;

    Rigidbody2D _body;
    bool _isOnGround;
    Vector3 _originalScale;
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _originalScale = transform.localScale;
    }
    void Update()
    {
        Move();
        Jump();
        Flip();
        Fire();
    }
    void Move()
    {
        float speed = Input.GetAxisRaw("Horizontal");
        _animator.SetFloat("speed", Mathf.Abs(speed));
        _body.linearVelocity = new Vector2(speed * _moveSpeed, _body.linearVelocity.y);
    }
    void Jump()
    {
        _isOnGround = Physics2D.OverlapCircle(_groundCheckPoint.position, _groundCheckRadius, _whatIsGround);
        _animator.SetBool("isOnGround", _isOnGround);

        if (Input.GetButtonDown("Jump") && _isOnGround)
        {
            _body.linearVelocity = new Vector2(_body.linearVelocity.x, _jumpForce);
        }
    }
    void Flip()
    {
        if (_body.linearVelocity.x > 0)
        {
            transform.localScale = _originalScale;
        }
        else if (_body.linearVelocity.x < 0)
        {
            transform.localScale = new Vector3(-_originalScale.x, _originalScale.y, _originalScale.z);
        }
    }
    void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            var bullet = Instantiate(_bulletController, _shotPoint.position, Quaternion.identity);
            bullet.SetDirection(transform.localScale.x > 0 ? Vector2.right : Vector2.left);
            _animator.SetTrigger("shotFired");
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_groundCheckPoint.position, _groundCheckRadius);
    }
}
