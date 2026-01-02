using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _fireRate = 0.3f;
    [SerializeField] private float _fireRange;
    [SerializeField] private int _damage = 5;

    private float lastShotTime;
    private Camera cam;

    private PlayerController _player;


    void Awake()
    {
        cam = Camera.main;
        _player = GetComponentInParent<PlayerController>();
    }

    void Update()
    {
        if (Time.time - lastShotTime > _fireRate)
        {
            lastShotTime = Time.time;
            Shoot();
        }

    }

    private GameObject NearEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject nearestEnemy = null;
        float minDistance = _fireRange;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }

    public void Shoot()
    {
        GameObject nearestEnemy = NearEnemy();

        if (nearestEnemy)
        {
            Vector3 mouse = cam.ScreenToWorldPoint(Input.mousePosition); // -> sparo in direzione del mouse
            Vector2 direction = mouse - transform.position;
            direction.Normalize();

            Bullet bullet = Instantiate(_bulletPrefab);
            _player.ShootSound();
            bullet.Damage = _damage;
            bullet.transform.position = transform.position;

            bullet.Setup(direction);

            // Vector2 direction = nearestEnemy.transform.position - transform.position; -> sparo automatico verso i nemici
            // Vector2 direction = _player.Facing; -> sparo nella direzione in cui guarda il player
        }
    }

    public void Upgrade()
    {
        _damage += 5;
        _player.LevelUpSound();
    }
}
