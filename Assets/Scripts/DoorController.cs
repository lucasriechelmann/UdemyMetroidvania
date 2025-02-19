using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField]
    Animator _animator;
    [SerializeField]
    float _distanceToOpen = 2.0f;
    PlayerController _player;
    void Start()
    {
        _player = PlayerHealthController.Instance.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, _player.transform.position) < _distanceToOpen)
        {
            _animator.SetBool("IsOpen", true);
        }
        else
        {
            _animator.SetBool("IsOpen", false);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _distanceToOpen);        
    }
}
