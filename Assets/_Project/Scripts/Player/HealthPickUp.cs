using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    [SerializeField] int heal;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LifeController _playerLife = other.GetComponent<LifeController>();
            _playerLife.AddHp(heal);
            Debug.Log($"+{heal} hp ricevuti!");
            Destroy(gameObject);
        }
    }
}
