using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    public enum FadeDirection
    {
        In,
        Out
    }
    [SerializeField]
    Slider _playerHealthSlider;
    [SerializeField]
    Image _fadeScreen;
    [SerializeField]
    float _fadeSpeed = 2;
    [SerializeField]
    bool _fadingToBlack;
    [SerializeField]
    bool _fadingFromBlack;
    [SerializeField]
    string _mainMenuSceneName = "Main Menu";
    [SerializeField]
    GameObject _pauseScreen;
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
    void Update()
    {
        if (_fadingToBlack)
        {
            float speed = _fadeSpeed * Time.deltaTime;
            _fadeScreen.color = GetColor(Mathf.MoveTowards(_fadeScreen.color.a, 1, _fadeSpeed * Time.deltaTime));
            
            if (_fadeScreen.color.a == 1)
                _fadingToBlack = false;
        }

        if (_fadingFromBlack)
        {
            float speed = _fadeSpeed * Time.deltaTime;
            
            _fadeScreen.color = GetColor(Mathf.MoveTowards(_fadeScreen.color.a, 0, _fadeSpeed * Time.deltaTime));
            
            if (_fadeScreen.color.a == 0)
                _fadingFromBlack = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }
    public void UpdateHealth(int currentValue, int maxValue)
    {
        _playerHealthSlider.maxValue = maxValue;
        _playerHealthSlider.value = currentValue;
    }
    public void StartFade(FadeDirection fadeDirection)
    {
        switch(fadeDirection)
        {
            case FadeDirection.In:
                _fadeScreen.color = GetColor(1);
                break;
            case FadeDirection.Out:
                _fadeScreen.color = GetColor(0);                
                break;
        }
        _fadingToBlack = fadeDirection == FadeDirection.Out;
        _fadingFromBlack = fadeDirection == FadeDirection.In;
    }
    public void PauseUnpause()
    {
        _pauseScreen?.SetActive(!_pauseScreen.activeSelf);
        Time.timeScale = _pauseScreen.activeSelf ? 0 : 1;
    }
    public void GoToMainMenu()
    {        
        Destroy(PlayerHealthController.Instance.gameObject);
        PlayerHealthController.Instance = null;
        Destroy(RespawnController.Instance.gameObject);
        RespawnController.Instance = null;
        Instance = null;
        Destroy(gameObject);
        Time.timeScale = 1;
        SceneManager.LoadScene(_mainMenuSceneName);
    }
    Color GetColor(float alpha) =>
        new Color(_fadeScreen.color.r, _fadeScreen.color.g, _fadeScreen.color.b, alpha);
}
