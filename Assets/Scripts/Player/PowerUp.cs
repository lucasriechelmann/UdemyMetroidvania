using UnityEngine;
using TMPro;
public class PowerUp : MonoBehaviour
{
    [SerializeField]
    PlayerAbility ability;
    [SerializeField]
    ParticleSystem _powerUpEffect;
    [SerializeField]
    TextMeshProUGUI _unlockText;
    [SerializeField]
    float _textDisplayTime = 1.5f;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            EnableMessage();
            PlayerAbilityTracker playerAbilityTracker = other.GetComponentInParent<PlayerAbilityTracker>();
            playerAbilityTracker.SetAbility(ability, true);
            Instantiate(_powerUpEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            AudioManager.Instance.PlaySFX(AudioManager.SFX.PickupGem);
        }
    }
    void EnableMessage()
    {
        _unlockText.transform.parent.SetParent(null);
        _unlockText.text = GetText();
        _unlockText.gameObject.SetActive(true);
        _unlockText.transform.position = transform.position;
        Destroy(_unlockText.transform.parent.gameObject, _textDisplayTime);
    }
    string GetText()
    {
        switch (ability)
        {
            case PlayerAbility.DoubleJump:
                return "Double Jump Unlocked!";
            case PlayerAbility.Dash:
                return "Dash Unlocked!";
            case PlayerAbility.BecomeBall:
                return "Become Ball Unlocked!";
            case PlayerAbility.DropBomb:
                return "Drop Bomb Unlocked!";
            default:
                return "Ability Unlocked!";
        }
    }
}
