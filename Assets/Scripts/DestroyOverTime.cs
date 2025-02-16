using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    [SerializeField]
    float _lifetime = 1.0f;    
    
    void Start()
    {
        Destroy(gameObject, _lifetime);        
    }
}
