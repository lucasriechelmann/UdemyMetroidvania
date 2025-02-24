using UnityEngine;
using UnityEngine.UI;

public class BossHealthController : MonoBehaviour
{
    [SerializeField]
    Slider _healthBar;
    [SerializeField]
    float _maxHealth;
    [SerializeField]
    BossBattle _theBoss;
    float _currentHealth;
    public float CurrentHealth => _currentHealth;
    public static BossHealthController Instance { get; private set; }
    void Awake()
    {
        Instance = this;        
    }
    void Start()
    {
        _currentHealth = _maxHealth;
        _healthBar.maxValue = _maxHealth;
        UpdateHealthBar();        
    }
    public void TakeDamage(int damage)
    {
        if (_currentHealth <= 0)
            return;

        _currentHealth -= damage;        

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            _theBoss.EndBattle();
        }

        UpdateHealthBar();
    }
    void UpdateHealthBar() => _healthBar.value = _currentHealth;
}
