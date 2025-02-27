using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnController : MonoBehaviour
{
    [SerializeField]
    Transform _respawnPoint;
    [SerializeField]
    float _waitForRespawn = 2f;
    [SerializeField]
    GameObject _deathEffect;
    GameObject _player;
    
    public static RespawnController Instance { get; set; }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        _player = PlayerHealthController.Instance.gameObject;
        _player.transform.position = _respawnPoint.position;
        
    }
    
    public void Respawn()
    {
        StartCoroutine(RespawnPlayer());
    }

    IEnumerator RespawnPlayer()
    {
        _player.SetActive(false);
        _player.GetComponent<PlayerController>().SetPlayerStanding();
        Instantiate(_deathEffect, _player.transform.position, _player.transform.rotation);
        
        yield return new WaitForSeconds(_waitForRespawn);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        _player.transform.position = _respawnPoint.position;
        PlayerHealthController.Instance.ResetHealth();

        _player.SetActive(true);
    }
    public void SetRespawnPoint(Transform newRespawnPoint)
    {
        _respawnPoint = newRespawnPoint;
    }
}
