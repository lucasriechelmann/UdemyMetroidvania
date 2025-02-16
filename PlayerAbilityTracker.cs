using UnityEngine;
public enum PlayerAbility
{
    DoubleJump,
    Dash,
    BecomeBall,
    DropBomb
}
public class PlayerAbilityTracker : MonoBehaviour
{
    [Header("Abilities")]
    [SerializeField]
    bool _canDoubleJump = false;
    [SerializeField]
    bool _canDash = false;
    [SerializeField]
    bool _canBecomeBall = false;
    [SerializeField]
    bool _canDropBomb = false;
    public bool CanDoubleJump => _canDoubleJump;
    public bool CanDash => _canDash;
    public bool CanBecomeBall => _canBecomeBall;
    public bool CanDropBomb => _canDropBomb;

    public void SetAbility(PlayerAbility playerAbility, bool value)
    {
        switch (playerAbility)
        {
            case PlayerAbility.DoubleJump:
                _canDoubleJump = value;
                break;
            case PlayerAbility.Dash:
                _canDash = value;
                break;
            case PlayerAbility.BecomeBall:
                _canBecomeBall = value;
                break;
            case PlayerAbility.DropBomb:
                _canDropBomb = value;
                break;
        }
    }
}
