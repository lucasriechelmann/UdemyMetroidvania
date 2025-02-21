using UnityEngine;

public class BossBattle : MonoBehaviour
{
    [SerializeField]
    Transform _cameraPosition;
    [SerializeField]
    float _cameraSpeed;
    CameraController _mainCamera;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _mainCamera = FindAnyObjectByType<CameraController>();
        _mainCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {        
        _mainCamera.transform.position = Vector3.MoveTowards(_mainCamera.transform.position, _cameraPosition.position, _cameraSpeed * Time.deltaTime);
    }
}
