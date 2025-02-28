using UnityEngine;

public class AMLoader : MonoBehaviour
{
    [SerializeField]
    AudioManager _audioManager;

    void Awake()
    {
        if (AudioManager.Instance != null)
            return;
        
        AudioManager newAM = Instantiate(_audioManager);
        AudioManager.Instance = newAM;
        DontDestroyOnLoad(newAM.gameObject);
    }
}
