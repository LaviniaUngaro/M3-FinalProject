using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    private int _damage;
    private Rigidbody2D _rb;
    private Vector2 _direction;
    private LifeController _life;
    private EnemiesController _enemy;

    // property per accedere a speed
    public float Speed
    {
        get => _speed;
        private set => _speed = value;
    }

    // property per accedere a damage
    public int Damage
    {
        get => _damage;
        set => _damage = value;
    }

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        _rb.velocity = _direction * _speed;
    }

    void Update()
    {
        Destroy(gameObject, 3);
    }

    public void Setup(Vector2 direction)
    {
        _direction = direction;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        _life = collision.gameObject.GetComponent<LifeController>();
        _enemy = collision.gameObject.GetComponent<EnemiesController>();

        if (_life != null)
        {
            _life.TakeDamage(Damage);
            if (_enemy != null)
            {
                if (_life.isDead())
                {
                    _enemy.OnDeath();
                }
                else 
                {
                    _enemy.OnDamage();
                }
            }
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }

}
