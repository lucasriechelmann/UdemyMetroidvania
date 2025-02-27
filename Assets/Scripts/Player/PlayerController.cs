using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player States")]
    [SerializeField]
    GameObject _standing;
    [SerializeField]
    GameObject _ball;
    [SerializeField]
    bool _canMove;
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
    [SerializeField]
    Animator _ballAnimator;
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
    [SerializeField]
    float _waitToBall = 0.5f;
    [SerializeField]
    float _waitToStand = 0.5f;    
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
    [Header("Bomb")]
    [SerializeField]
    Transform _bombPoint;
    [SerializeField]
    GameObject _bomb;

    Rigidbody2D _body;
    bool _isOnGround;
    Vector3 _originalScale;

    bool _canDoubleJump = false;
    float _dashCounter;
    float _afterImageCounter;
    float _dashRechargeCounter;
    float _ballCounter;
    float _standingCounter;
    PlayerAbilityTracker _playerAbilityTracker;
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _originalScale = transform.localScale;
        _playerAbilityTracker = GetComponent<PlayerAbilityTracker>();
        _canMove = true;
    }
    void Update()
    {
        if (_canMove && Time.timeScale != 0)
        {
            Dash();
            Move();
            Jump();
            Flip();
            Fire();
            Bomb();            
        }
        else
        {
            _body.linearVelocity = Vector2.zero;
        }

            BallMode();
        StandingMode();
    }
    void Dash()
    {
        if (IsBall() || !_playerAbilityTracker.CanDash)
            return;


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
        
        _body.linearVelocity = new Vector2(speed * _moveSpeed, _body.linearVelocity.y);

        if(IsStanding())
            _animator.SetFloat("speed", Mathf.Abs(speed));

        if (IsBall())
            _ballAnimator.SetFloat("speed", Mathf.Abs(speed));
    }
    void Jump()
    {        
        _isOnGround = Physics2D.OverlapCircle(_groundCheckPoint.position, _groundCheckRadius, _whatIsGround);
        
        if(IsStanding())
            _animator.SetBool("isOnGround", _isOnGround);      

        if (Input.GetButtonDown("Jump") && (_isOnGround || (_canDoubleJump && _playerAbilityTracker.CanDoubleJump)))
        {
            if (_isOnGround)
                _canDoubleJump = true;
            else
            {
                _canDoubleJump = false;

                if (IsStanding())
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
        if (IsBall())
            return;

        if (Input.GetButtonDown("Fire1"))
        {
            var bullet = Instantiate(_bulletController, _shotPoint.position, Quaternion.identity);
            bullet.SetDirection(transform.localScale.x > 0 ? Vector2.right : Vector2.left);
            
            _animator.SetTrigger("shotFired");
        }
    }
    void Bomb()
    {
        if (IsStanding() || !_playerAbilityTracker.CanDropBomb)
            return;

        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(_bomb, _bombPoint.position, Quaternion.identity);
        }
    }
    void BallMode()
    {
        if (IsBall() || !_playerAbilityTracker.CanBecomeBall)
            return;

        if (Input.GetAxisRaw("Vertical") < -0.9f)
        {
            _ballCounter -= Time.deltaTime;

            if (_ballCounter <= 0)
                SetPlayerState(PlayerState.Ball);
        }
        else
            _ballCounter = _waitToBall;
    }
    void StandingMode()
    {
        if(IsStanding())
            return;

        if (Input.GetAxisRaw("Vertical") > 0.9f)
        {
            _standingCounter -= Time.deltaTime;

            if (_standingCounter <= 0)
                SetPlayerState(PlayerState.Standing);
        }
        else
            _standingCounter = _waitToStand;
    }
    void SetPlayerState(PlayerState playerState)
    {
        _standing.SetActive(playerState == PlayerState.Standing);
        _ball.SetActive(playerState == PlayerState.Ball);
    }
    bool IsDashing() => _dashCounter > 0;
    bool IsStanding() => _standing.activeSelf;
    bool IsBall() => _ball.activeSelf;
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_groundCheckPoint.position, _groundCheckRadius);
    }
    enum PlayerState
    {
        Standing,
        Ball
    }
    public void SetPlayerStanding() => SetPlayerState(PlayerState.Standing);
    public void SetCanMove(bool value) => _canMove = value;
    public void MoveTo(Vector2 position)
    {
        float speed = transform.position.x != position.x ? 1 : 0;

        transform.position = position;

        if (IsStanding())
            _animator.SetFloat("speed", Mathf.Abs(speed));

        if (IsBall())
            _ballAnimator.SetFloat("speed", Mathf.Abs(speed));

    }
}
