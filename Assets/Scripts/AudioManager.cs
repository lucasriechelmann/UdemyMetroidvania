using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Music")]
    [SerializeField]
    AudioSource _mainMenu;
    [SerializeField]
    AudioSource _level;
    [SerializeField]
    AudioSource _bossBattle;
    [Header("SFX")]
    [SerializeField]
    AudioSource _bossDeath;
    [SerializeField]
    AudioSource _bossImpact;
    [SerializeField]
    AudioSource _bossShot;
    [SerializeField]
    AudioSource _bulletImpact;
    [SerializeField]
    AudioSource _enemyExplode;
    [SerializeField]
    AudioSource _pickupGem;
    [SerializeField]
    AudioSource _playerBall;
    [SerializeField]
    AudioSource _playerDash;
    [SerializeField]
    AudioSource _playerDeath;
    [SerializeField]
    AudioSource _playerDoubleJump;
    [SerializeField]
    AudioSource _playerFromBall;
    [SerializeField]
    AudioSource _playerHurt;
    [SerializeField]
    AudioSource _playerJump;
    [SerializeField]
    AudioSource _playerMine;
    [SerializeField]
    AudioSource _playerShoot;    
    public static AudioManager Instance { get; set; }
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
    void Start()
    {
        PlayMainMenuMusic();
    }
    public void PlayMainMenuMusic() => PlayMusics(Musics.MainMenu);
    public void PlayLevelMusic() => PlayMusics(Musics.Level);
    public void PlayerBossMusic() => PlayMusics(Musics.BossBattle);
    void PlayMusics(Musics music)
    {
        switch(music)
        {
            case Musics.MainMenu:
                if (_mainMenu.isPlaying)
                    return;
                break;
            case Musics.Level:
                if(_level.isPlaying)
                    return;
                break;
            case Musics.BossBattle:
                if(_bossBattle.isPlaying)
                    return;
                break;
        }

        _mainMenu.Stop();
        _level.Stop();
        _bossBattle.Stop();

        switch (music)
        {
            case Musics.MainMenu:
                _mainMenu.Play();
                break;
            case Musics.Level:
                _level.Play();
                break;
            case Musics.BossBattle:
                _bossBattle.Play();
                break;
        }
    }
    public void PlaySFX(SFX sfx)
    {
        float randomPitch = Random.Range(0.9f, 1.1f);
        switch (sfx)
        {
            case SFX.BossDeath:
                _bossDeath.pitch = randomPitch;
                _bossDeath.Play();
                break;
            case SFX.BossImpact:
                _bossImpact.pitch = randomPitch;
                _bossImpact.Play();
                break;
            case SFX.BossShot:
                _bossShot.pitch = randomPitch;
                _bossShot.Play();
                break;
            case SFX.BulletImpact:
                _bulletImpact.pitch = randomPitch;
                _bulletImpact.Play();
                break;
            case SFX.EnemyExplode:
                _enemyExplode.pitch = randomPitch;
                _enemyExplode.Play();
                break;                
            case SFX.PickupGem:
                _pickupGem.pitch = randomPitch;
                _pickupGem.Play();
                break;
            case SFX.PlayerBall:
                _playerBall.pitch = randomPitch;
                _playerBall.Play();
                break;
            case SFX.PlayerDash:
                _playerDash.pitch = randomPitch;
                _playerDash.Play();
                break;
            case SFX.PlayerDeath:
                _playerDeath.pitch = randomPitch;
                _playerDeath.Play();
                break;
            case SFX.PlayerDoubleJump:
                _playerDoubleJump.pitch = randomPitch;
                _playerDoubleJump.Play();
                break;
            case SFX.PlayerFromBall:
                _playerFromBall.pitch = randomPitch;
                _playerFromBall.Play();
                break;
            case SFX.PlayerHurt:
                _playerHurt.pitch = randomPitch;
                _playerHurt.Play();
                break;
            case SFX.PlayerJump:
                _playerJump.pitch = randomPitch;
                _playerJump.Play();
                break;
            case SFX.PlayerMine:
                _playerMine.pitch = randomPitch;
                _playerMine.Play();
                break;
            case SFX.PlayerShoot:
                _playerShoot.pitch = randomPitch;
                _playerShoot.Play();
                break;
        }
    }
    enum Musics
    {
        None,
        MainMenu,
        Level,
        BossBattle
    }
    public enum SFX
    {
        BossDeath,
        BossImpact,
        BossShot,
        BulletImpact,
        EnemyExplode,
        PickupGem,
        PlayerBall,
        PlayerDash,
        PlayerDeath,
        PlayerDoubleJump,
        PlayerFromBall,
        PlayerHurt,
        PlayerJump,
        PlayerMine,
        PlayerShoot
    }
}
