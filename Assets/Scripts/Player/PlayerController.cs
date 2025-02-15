using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Forces")]
    [SerializeField]
    float _moveSpeed = 8f;
    [SerializeField]
    float _jumpForce = 20f;
    [SerializeField]
    float _dashSpeed = 25f;
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
    [Header("Times")]
    [SerializeField]
    float _dashTime = 0.2f;
    [SerializeField]
    float _waitAfterDashing = 0.25f;
    [Header("After-Image Effect")]
    [SerializeField]
    SpriteRenderer _characterRenderer;
    [SerializeField]
    SpriteRenderer _afterImageRenderer;
    [SerializeField]
    float _afterImageLifetime = 0.5f;
    [SerializeField]
    float _timeBetweenAfterImages = 0.1f;
    [SerializeField]
    Color _afterImageColor;

    Rigidbody2D _body;
    bool _isOnGround;
    Vector3 _originalScale;

    bool _canDoubleJump = false;
    float _dashCounter;
    float _afterImageCounter;
    float _dashRechargeCounter;
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _originalScale = transform.localScale;
    }
    void Update()
    {
        Dash();
        Move();
        Jump();
        Flip();
        Fire();
    }
    void Dash()
    {
        if(_dashRechargeCounter > 0)
            _dashRechargeCounter -= Time.deltaTime;

        if (Input.GetButtonDown("Fire2") && _dashCounter <= 0 && _dashRechargeCounter <= 0)
        {
            _dashCounter = _dashTime;
            ShowAfterImage();
        }

        if (_dashCounter > 0)
        {
            _body.linearVelocity = new Vector2(transform.localScale.x * _dashSpeed, _body.linearVelocity.y);
            _dashCounter -= Time.deltaTime;
            _afterImageCounter -= Time.deltaTime;

            if (_afterImageCounter <= 0)
                ShowAfterImage();

            _dashRechargeCounter = _waitAfterDashing;
        }        
    }
    void ShowAfterImage()
    {
        SpriteRenderer newAfterImage = Instantiate(_afterImageRenderer, transform.position, Quaternion.identity);
        newAfterImage.sprite = _characterRenderer.sprite;
        newAfterImage.transform.localScale = transform.localScale;
        newAfterImage.color = _afterImageColor;

        Destroy(newAfterImage.gameObject, _afterImageLifetime);

        _afterImageCounter = _timeBetweenAfterImages;
    }
    void Move()
    {
        if (IsDashing())
            return;

        float speed = Input.GetAxisRaw("Horizontal");
        _animator.SetFloat("speed", Mathf.Abs(speed));
        _body.linearVelocity = new Vector2(speed * _moveSpeed, _body.linearVelocity.y);
    }
    void Jump()
    {
        _isOnGround = Physics2D.OverlapCircle(_groundCheckPoint.position, _groundCheckRadius, _whatIsGround);
        _animator.SetBool("isOnGround", _isOnGround);

        if (Input.GetButtonDown("Jump") && (_isOnGround || _canDoubleJump))
        {
            if (_isOnGround)
                _canDoubleJump = true;
            else
            {
                _canDoubleJump = false;
                _animator.SetTrigger("doubleJump");
            }                

            _body.linearVelocity = new Vector2(_body.linearVelocity.x, _jumpForce);
        }
    }
    void Flip()
    {
        if (IsDashing())
            return;

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
    bool IsDashing() => _dashCounter > 0;
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_groundCheckPoint.position, _groundCheckRadius);
    }
}
