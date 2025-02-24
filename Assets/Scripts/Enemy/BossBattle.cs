using UnityEngine;

public class BossBattle : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField]
    Transform _cameraPosition;
    [SerializeField]
    float _cameraSpeed;
    [Header("Thresholds")]
    [SerializeField]
    int _threshold1;
    [SerializeField]
    int _threshold2;
    [Header("Times")]
    [SerializeField]
    float _activeTime;
    [SerializeField]
    float _fadeOutTime;
    [SerializeField]
    float _inactiveTime;    
    [Header("Positions")]
    [SerializeField]
    Transform[] _spawnPoints;
    [Header("Forces")]
    [SerializeField]
    float _moveSpeed;
    [Header("Animations")]
    [SerializeField]
    Animator _animator;
    [Header("Boss")]
    [SerializeField]
    Transform _theBoss;
    CameraController _mainCamera;
    float _activeCounter;
    float _fadeOutCounter;
    float _inactiveCounter;
    Transform _targetPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _mainCamera = FindAnyObjectByType<CameraController>();
        _mainCamera.enabled = false;

        _activeCounter = _activeTime;
    }

    // Update is called once per frame
    void Update()
    {        
        _mainCamera.transform.position = Vector3.MoveTowards(_mainCamera.transform.position, _cameraPosition.position, _cameraSpeed * Time.deltaTime);
        Flip();
        Battle();
    }
    void Flip()
    {
        float scale = PlayerHealthController.Instance.transform.position.x < _theBoss.position.x ? 1 : -1;
        _theBoss.localScale = new Vector3(scale, 1, 1);
    }
    void Battle()
    {
        Threshold1();
        Threshold2();
    }
    void Threshold1()
    {
        if(BossHealthController.Instance.CurrentHealth > _threshold1)
        {
            if(_activeCounter > 0)
            {
                _activeCounter -= Time.deltaTime;
                if (_activeCounter <= 0)
                {
                    _animator.SetTrigger("vanish");
                    _fadeOutCounter = _fadeOutTime;
                }
            }
            else if (_fadeOutCounter > 0)
            {
                _fadeOutCounter -= Time.deltaTime;
                if (_fadeOutCounter <= 0)
                {
                    _theBoss.gameObject.SetActive(false);
                    _inactiveCounter = _inactiveTime;
                }
            }
            else if(_inactiveCounter > 0)
            {
                _inactiveCounter -= Time.deltaTime;
                if (_inactiveCounter <= 0)
                {
                    _theBoss.position = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
                    _theBoss.gameObject.SetActive(true);                    
                    _activeCounter = _activeTime;
                }
            }
        }
    }
    void Threshold2()
    {
        if(BossHealthController.Instance.CurrentHealth <= _threshold1)
        {
            if(_targetPoint == null)
            {
                _targetPoint = _theBoss;
                _fadeOutCounter = _fadeOutTime;
                _animator.SetTrigger("vanish");

                return;
            }

            if (Vector3.Distance(_theBoss.position, _targetPoint.position) > 0.2f)
            {
                _theBoss.position = Vector3.MoveTowards(_theBoss.position, _targetPoint.position, _moveSpeed * Time.deltaTime);

                if (Vector3.Distance(_theBoss.position, _targetPoint.position) <= 0.2f)
                {
                    _animator.SetTrigger("vanish");
                    _fadeOutCounter = _fadeOutTime;
                }
            }
            else if (_fadeOutCounter > 0)
            {
                _fadeOutCounter -= Time.deltaTime;
                if (_fadeOutCounter <= 0)
                {
                    _theBoss.gameObject.SetActive(false);
                    _inactiveCounter = _inactiveTime;
                }
            }
            else if (_inactiveCounter > 0)
            {
                _inactiveCounter -= Time.deltaTime;
                if (_inactiveCounter <= 0)
                {
                    _theBoss.position = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
                    _targetPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
                    while(_targetPoint.position == _theBoss.position && _spawnPoints.Length > 1)
                    {
                        _targetPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
                    }
                    _theBoss.gameObject.SetActive(true);
                }
            }
        }
    }
    public void EndBattle()
    {
        _mainCamera.enabled = true;
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }
}
