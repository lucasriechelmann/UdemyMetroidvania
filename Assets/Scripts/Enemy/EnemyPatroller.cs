using System;
using UnityEngine;

public class EnemyPatroller : MonoBehaviour
{
    [SerializeField]
    Transform[] _patrolPoints;
    [SerializeField]
    float _moveSpeed = 5.0f;
    [SerializeField]
    float _jumpForce = 10.0f;
    [SerializeField]
    float _waitTime = 1.0f;
    [SerializeField]
    Animator _animator;
    [SerializeField]
    LayerMask _groundLayer;
    [SerializeField]
    float _wallCheckDistance = 0.1f;
    [SerializeField]
    Transform _wallCheckPoint;
    Rigidbody2D _body;
    int _currentPatrolPointIndex = 0;
    float _waitTimeCounter = 0;
    bool _hitWall = false;
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _waitTimeCounter = _waitTime;

        Array.ForEach(_patrolPoints, point => point.SetParent(null));        
    }

    // Update is called once per frame
    void Update()
    {
        _hitWall = Physics2D.OverlapCircle(_wallCheckPoint.position, _wallCheckDistance, _groundLayer);        

        if (Mathf.Abs(transform.position.x - GetCurrentPatrolPointX()) > 0.2)
        {
            if (transform.position.x < GetCurrentPatrolPointX())
            {
                _body.linearVelocity = new Vector2(_moveSpeed, _body.linearVelocity.y);
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                _body.linearVelocity = new Vector2(-_moveSpeed, _body.linearVelocity.y);
                transform.localScale = new Vector3(1, 1, 1);
            }

            if(_hitWall)
            {
                _body.linearVelocity = new Vector2(_body.linearVelocity.x, _jumpForce);
            }
        }
        else
        {
            _body.linearVelocity = new Vector2(0, _body.linearVelocity.y);

            _waitTimeCounter -= Time.deltaTime;

            if (_waitTimeCounter < 0)
            {
                _waitTimeCounter = _waitTime;
                _currentPatrolPointIndex++;

                if (_currentPatrolPointIndex >= _patrolPoints.Length)
                {
                    _currentPatrolPointIndex = 0;
                }                
            }
        }

        _animator.SetFloat("speed", Mathf.Abs(_body.linearVelocity.x));
    }
    float GetCurrentPatrolPointX() => _patrolPoints[_currentPatrolPointIndex].position.x;
    float GetCurrentPatrolPointY() => _patrolPoints[_currentPatrolPointIndex].position.y;
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_wallCheckPoint.position, _wallCheckDistance);
    }
}
