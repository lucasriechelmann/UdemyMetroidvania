using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    [SerializeField]
    int _maxHealth = 100;
    [SerializeField]
    int _currentHealth;
    [SerializeField]
    SpriteRenderer[] _playerSprites;
    [SerializeField]
    float _invicibilityLength = 2f;
    [SerializeField]
    float _flashLength = 0.1f;
    float _invicibilityCounter;
    float _flashCounter;
    public static PlayerHealthController Instance;
    void Awake()
    {
        if(Instance == null)
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
        _currentHealth = _maxHealth;
        UpdateHealthBar();
    }
    void Update()
    {
        Flash();
    }
    public void DamagePlayer(int damageAmount)
    {
        _currentHealth -= damageAmount;
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            
            RespawnController.Instance.Respawn();
        }
        else
        {
            _invicibilityCounter = _invicibilityLength;
        }
        
        UpdateHealthBar();
    }
    void UpdateHealthBar() => UIController.Instance.UpdateHealth(_currentHealth, _maxHealth);
    void Flash()
    {
        if(_invicibilityCounter > 0)
        {
            _invicibilityCounter -= Time.deltaTime;
            _flashCounter -= Time.deltaTime;    
            if(_flashCounter <= 0)
            {
                _flashCounter = _flashLength;
                InvertEnableDisableSpriteRenderers();
            }
            if (_invicibilityCounter <= 0)
            {
                EnableSpriteRenderers();
            }
        }
    }
    void InvertEnableDisableSpriteRenderers()
    {
        foreach(SpriteRenderer sprite in _playerSprites)
        {
            sprite.enabled = !sprite.enabled;
        }
    }
    void EnableSpriteRenderers()
    {
        foreach (SpriteRenderer sprite in _playerSprites)
        {
            sprite.enabled = true;
        }
    }
    public void ResetHealth()
    {

        _currentHealth = _maxHealth;
        UpdateHealthBar();
    }
    public void HealPlayer(int healAmount)
    {
        _currentHealth += healAmount;
        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
        UpdateHealthBar();
    }
}
