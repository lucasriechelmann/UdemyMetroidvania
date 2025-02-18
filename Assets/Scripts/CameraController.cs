using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    BoxCollider2D _cameraBox;
    PlayerController _player;
    float halfWidth;
    float halfHeight;

    void Start()
    {
        _player = FindAnyObjectByType<PlayerController>();
        var camera = Camera.main;
        halfHeight = camera.orthographicSize;
        halfWidth = halfHeight * camera.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        if(_player != null)
        {
            transform.position = new Vector3(
                Mathf.Clamp(_player.transform.position.x, _cameraBox.bounds.min.x + halfWidth, _cameraBox.bounds.max.x - halfWidth),
                Mathf.Clamp(_player.transform.position.y, _cameraBox.bounds.min.y + halfHeight, _cameraBox.bounds.max.y - halfHeight), 
                transform.position.z);
        }
    }
}
