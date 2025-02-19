using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    [SerializeField]
    Slider _playerHealthSlider;
    public static UIController Instance { get; private set; }
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
    public void UpdateHealth(int currentValue, int maxValue)
    {
        _playerHealthSlider.maxValue = maxValue;
        _playerHealthSlider.value = currentValue;
    }
}
