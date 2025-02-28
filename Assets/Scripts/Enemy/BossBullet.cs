using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BossBullet : MonoBehaviour
{
    [SerializeField]
    float _moveSpeed;
    [SerializeField]
    int _damage;
    [SerializeField]
    Rigidbody2D _body;
    [SerializeField]
    GameObject _impactEffect;
    void Start()
    {        
        Vector3 direction = transform.position - PlayerHealthController.Instance.transform.position;
        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        AudioManager.Instance.PlaySFX(AudioManager.SFX.BossShot);
    }
    void Update()
    {
        _body.linearVelocity = -transform.right * _moveSpeed;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.TryGetComponent(out PlayerHealthController player))
        {
            AudioManager.Instance.PlaySFX(AudioManager.SFX.BossImpact);
            player.DamagePlayer(_damage);            
        }

        if(_impactEffect != null)
            Instantiate(_impactEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
