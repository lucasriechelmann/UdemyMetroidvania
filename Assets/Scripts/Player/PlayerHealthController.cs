using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    [SerializeField]
    int _maxHealth = 100;
    [SerializeField]
    int _currentHealth;
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
    }
    public void DamagePlayer(int damageAmount)
    {
        _currentHealth -= damageAmount;
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
}
