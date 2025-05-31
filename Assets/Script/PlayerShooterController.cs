using System.Collections.Generic;
using UnityEngine;

public class PlayerShooterController : MonoBehaviour
{
    [SerializeField] private float _fireRate = 2f;
    [Range(1f, 7f)]
    [SerializeField] private float _fireRange = 5f;
    private float _fireRangeSqr;
    private float _timeFromLastShhot;
    [SerializeField] private Transform _bullets;

    public Bullet bulletPrefab;

    List<Enemy> _enemies = new List<Enemy>();
    private GameObject _nearestEnemy;

    public void AddEnemyInList(Enemy enemy)
    {
        _enemies.Add(enemy);
    }

    public void RemoveEnemyInList(Enemy enemy)
    {
        _enemies.Remove(enemy);
    }

    void Start()
    {
        _timeFromLastShhot = Time.time;
        _fireRangeSqr = _fireRange * _fireRange;
    }

    private GameObject FindNearestEnemy()
    {
        Enemy nearestEnemy = null;
        float nearestEnemyDistanceSqr = float.MaxValue;
        float distanceSqr;
        foreach (Enemy enemy in _enemies)
        {
            distanceSqr = Mathf.Abs((enemy.transform.position - transform.position).sqrMagnitude);
            //Sfrutto le proprietà della radice quadrata senza eseguirla, non mi serve il numero esatto, ma solo come si comporta nei suoi intervalli
            if (distanceSqr < 1f)
            {
                if (distanceSqr > nearestEnemyDistanceSqr || nearestEnemyDistanceSqr > 1f)
                {
                    nearestEnemyDistanceSqr = distanceSqr;
                    nearestEnemy = enemy;
                }
            }
            else
            {
                if (distanceSqr < _fireRangeSqr && distanceSqr < nearestEnemyDistanceSqr)
                {
                    nearestEnemyDistanceSqr = distanceSqr;
                    nearestEnemy = enemy;
                }
            }
        }

        if (nearestEnemy == null)
        {
            Debug.Log("Nessun nemico a portata di tiro");
            return null;
        }
        return nearestEnemy.gameObject;
    }

    private void Shoot()
    {
        
        if (_enemies.Count > 0 && Time.time - _timeFromLastShhot > _fireRate)
        {
            _nearestEnemy = FindNearestEnemy();
            if (_nearestEnemy != null)
            {
                _timeFromLastShhot = Time.time;
                Bullet clone = Instantiate(bulletPrefab, transform.position, transform.rotation, _bullets);
                clone.Born((_nearestEnemy.transform.position - transform.position).normalized);
            }
        }
    }

    void Update()
    {
        Shoot();
    }
}
