using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private string _speed = "speed";
    [SerializeField] private string _direction = "direction";
    private Animator _animator;

    void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void SetSpeed(float speed)
    {
        _animator.SetFloat(_speed, speed);
    }

    public void SetDirection(float direction)
    {
        _animator.SetFloat(_direction, direction);
    }

    public void SetDead()
    {
        _animator.SetTrigger("isDead");
    }

    public void SetDamage()
    {
        _animator.SetTrigger("isDamaged");
    }
}
