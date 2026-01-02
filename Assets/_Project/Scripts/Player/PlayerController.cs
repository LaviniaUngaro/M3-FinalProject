using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private AudioClip levelUpSound;
    [SerializeField] private AudioClip healSound;
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip footstepSound;
    [SerializeField] private float footstepInterval;


    private float h;
    private float v;
    private float _footstepTimer;

    private Rigidbody2D _rb;
    private Vector2 direction;
    private Vector2 facing;

    private PlayerAnimation _playerAnimCon;

    private AudioSource _audioSource;

    // property per accedere a direction
    public Vector2 Direction
    {
        get => direction;
        private set => direction = value;
    }

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerAnimCon = GetComponent<PlayerAnimation>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        direction = new Vector2(h, v);

        // setto animazione walk/idle
        if (h != 0 || v != 0)
        {
            _playerAnimCon.SetSpeed(1f);
            _footstepTimer -= Time.deltaTime;

            if (_footstepTimer <= 0)
            {
                _audioSource.PlayOneShot(footstepSound);
                _footstepTimer = footstepInterval;
            } 
        }
        else
        {
            _playerAnimCon.SetSpeed(0f);
            _footstepTimer = 0;
        }

        // setto animazione dx/sx
        if (h > 0)
        {
            _playerAnimCon.SetDirection(1f);
        }
        else if (h < 0)
        {
            _playerAnimCon.SetDirection(-1f);
        }
    }

    void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + direction * (_speed * Time.fixedDeltaTime));
    }

    public void OnDamage()
    {
        _playerAnimCon.SetDamage();
    }

    public void OnDeath()
    {
        _playerAnimCon.SetDead();
        Destroy(gameObject, 0.5f);
    }

    public void LevelUpSound()
    {
        _audioSource.PlayOneShot(levelUpSound);
    }

    public void HealSound()
    {
        _audioSource.PlayOneShot(healSound);
    }

    public void DamageSound()
    {
        _audioSource.PlayOneShot(damageSound);
    }

    public void DeathSound()
    {
        _audioSource.PlayOneShot(deathSound);
    }

    public void ShootSound()
    {
        _audioSource.PlayOneShot(shootSound);
    }
}