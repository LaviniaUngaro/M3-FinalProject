using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int damage = 10;
    [SerializeField] private AudioClip deathSound;

    private float aggroDistance = 15f;
    private PlayerController _player;
    private LifeController _playerLife;
    private Rigidbody2D _rb;

    private EnemiesAnimation _enemiesAnimCon;
    private AudioSource _audioSource;

    private bool _hasHitPlayer = false;


    void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        _player = player.GetComponent<PlayerController>();
        _playerLife = player.GetComponent<LifeController>();
        _rb = GetComponent<Rigidbody2D>();
        _enemiesAnimCon = GetComponent<EnemiesAnimation>();
        _audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        EnemyMovement();
    }

    public void EnemyMovement()
    {
        if (_player != null)
        {
            Vector2 direction = _player.transform.position - transform.position;
            float distance = direction.sqrMagnitude;

            if (distance < aggroDistance * aggroDistance)
            {
                direction.Normalize();
                _rb.velocity = direction * _speed;
                _enemiesAnimCon.SetSpeed(1f);
            }
            else
            {
                _rb.velocity = Vector2.zero;
                _enemiesAnimCon.SetSpeed(0f);
            }

            if (direction.x > 0)
            {
                _enemiesAnimCon.SetDirection(1f);
            }
            else if (direction.x < 0)
            {
                _enemiesAnimCon.SetDirection(-1f);
            }
        }
        else
        {
            _enemiesAnimCon.SetSpeed(0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_hasHitPlayer) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            _hasHitPlayer = true;
            _playerLife.TakeDamage(damage);
            if (_playerLife.isDead())
            {
                _player.OnDeath();
            }
            else
            {
                _player.OnDamage();
            }
            OnDeath();
            // Debug.Log($"Il nemico è morto");
        }
    }

    public void OnDamage()
    {
        _enemiesAnimCon.SetDamage();
    }

    public void OnDeath()
    {
        _enemiesAnimCon.SetDead();
        Destroy(gameObject, 0.5f);
    }

    public void DeathSound()
    {
        _audioSource.PlayOneShot(deathSound);
    }
}
