using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DoorController : MonoBehaviour
{
    [SerializeField]
    Animator _animator;
    [SerializeField]
    float _distanceToOpen = 2.0f;
    [SerializeField]
    Transform _exitPoint;
    [SerializeField]
    float _movePlayerSpeed;
    [SerializeField]
    string _levelToLoad;
    
    PlayerController _player;
    bool _playerExiting;
    void Start()
    {
        _player = PlayerHealthController.Instance.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, _player.transform.position) < _distanceToOpen)
        {
            _animator.SetBool("IsOpen", true);
        }
        else
        {
            _animator.SetBool("IsOpen", false);
        }

        if (_playerExiting)
        {
            _player.MoveTo(Vector2.MoveTowards(_player.transform.position, _exitPoint.position, _movePlayerSpeed * Time.deltaTime));
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!_playerExiting)
            {
                _player.SetCanMove(false);
                _player.SetPlayerStanding();
                StartCoroutine(UseDor());

            }
        }
    }
    IEnumerator UseDor()
    {
        _playerExiting = true;
        UIController.Instance.StartFade(UIController.FadeDirection.Out);
        yield return new WaitForSeconds(1.5f);
        
        _player.SetCanMove(true);
        _playerExiting = false;

        UIController.Instance.StartFade(UIController.FadeDirection.In);

        SceneManager.LoadScene(_levelToLoad);
        SceneManager.sceneLoaded += OnSceneLoaded;        
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        RespawnController.Instance.SetRespawnPoint(FindAnyObjectByType<PlayerEntryPoint>()?.GetEntryPoint() ?? _exitPoint);
        _player.transform.position = FindAnyObjectByType<PlayerEntryPoint>()?.GetEntryPoint().position ?? _exitPoint.position;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _distanceToOpen);        
    }
}
