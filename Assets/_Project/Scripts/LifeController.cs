using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    [SerializeField] private int _hp;
    [SerializeField] private int _maxHp;
    [SerializeField] private bool _isDead;

    private PlayerController _player;
    private EnemiesController _enemy;
    

    void Awake()
    {
        _player = GetComponent<PlayerController>();
        _enemy = GetComponent<EnemiesController>();
    }

    // property per accedere a hp
    public int Hp
    {
        get => _hp;
        private set => _hp = Mathf.Clamp(value, 0, _maxHp);
    }


    public void AddHp(int amount)
    {
        Hp += amount;
        _player.HealSound();

    }

    public void TakeDamage(int damage)
    {
        if (_isDead) return;

        Hp -= damage;
        if (Hp > 0)
        {
            if (gameObject.CompareTag("Player"))
            {
                Debug.Log($"{gameObject.name} subisce {damage} di danno! La sua vita rimanente è {Hp}");
                _player.DamageSound();
            }
            else if (gameObject.CompareTag("Enemy"))
            {
                Debug.Log($"Il nemico {gameObject.name} subisce {damage} di danno!");
            }
        }
        else
        {
            Death();
        }
    }

    public void Death()
    {
        _isDead = true;
        if (gameObject.CompareTag("Player"))
        {
            _player.DeathSound();
            Debug.Log($"Hai perso! {gameObject.name} è morta!");
        }
        else
        {
            _enemy.DeathSound();
            Debug.Log($"{gameObject.name} è morto!");
        }
    }

    public bool isDead()
    {
        return _isDead;
    }

    
}
