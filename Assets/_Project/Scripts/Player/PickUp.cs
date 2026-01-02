using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private Gun _weaponPrefab;

    private Gun _hasWeapon;


    void OnTriggerEnter2D(Collider2D other) // raccoglie arma da terra
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _hasWeapon = other.GetComponentInChildren<Gun>();
            if (_hasWeapon == null)
            {
                Debug.Log($"{other.gameObject.name} ha raccolto l'arma!");
                Gun newWeapon = Instantiate(_weaponPrefab, other.transform);
                newWeapon.transform.localPosition = Vector3.zero;
                Destroy(gameObject);
            }
            else
            {
                Debug.Log($"Arma potenziata!");
                _hasWeapon.Upgrade();
                Destroy(gameObject);
            }
        }
    }
}