using UnityEngine;

public class EnemyFlyingController : MonoBehaviour
{
    [SerializeField]
    float _moveSpeed = 3f;
    [SerializeField]
    float _turnSpeed = 180f;
    [SerializeField]    
    float _rangeToStartChasing = 10f;
    [SerializeField]
    Animator _animator;
    bool _isChasing;    
    Transform _player;    
    void Start()
    {
        _player = PlayerHealthController.Instance.transform;
    }
    void Update()
    {
        Chase();
        _animator.SetBool("IsChasing", _isChasing);
    }
    void Chase()
    {
        if(_isChasing)
        {
            if (_player.gameObject.activeSelf)
            {
                Vector3 direction = transform.position - _player.position;
                float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) + -90;
                Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _turnSpeed * Time.deltaTime);
                //transform.position = Vector3.MoveTowards(transform.position, _player.position, _moveSpeed * Time.deltaTime);
                transform.position += -transform.up * _moveSpeed * Time.deltaTime;
            }
            return;
        }

        if (Vector3.Distance(transform.position, _player.position) < _rangeToStartChasing)
        {
            _isChasing = true;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _rangeToStartChasing);
    }
}
