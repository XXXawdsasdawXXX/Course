using Common;
using Data;
using Data.Entity;
using Service;
using UnityEngine;

namespace Entity
{
    public sealed class EnemiesController : Mono
    {
        [SerializeField] private int _maxEnemyCount = 6;
        [SerializeField] private EnemyData _data;

        [Space, Header("Spawn")] 
        [SerializeField] private PointService _pointService;
        [SerializeField] private Cooldown _spawnCooldown;
        [SerializeField] private EnemySpawner _pool;

        [Space, Header("Attack")] 
        [SerializeField] private PlayerController _player;
        [SerializeField] private BulletSpawner _bulletSpawner;

        
        private void Start()
        {
            _pool.Warmup(_bulletSpawner, _player.GetTransform(), _data);
        }

        private void Update()
        {
            _spawnCooldown.Run(() =>
            {
                if (_pool.GetEnabledCount() < _maxEnemyCount)
                {
                    Enemy enemy = _pool.GetNext();
                    enemy.health.Died += () =>
                    {
                        _pool.Disable(enemy);
                    };

                    Vector3 startPoint = _pointService.GetSpawnPoint();
                    Vector3 finishPoint = _pointService.GetAttackPoint();
                    enemy.SetPoints(startPoint, finishPoint);

                    _pool.Enable(enemy);
                }
            });
        }

        public void Reload()
        {
            _pool.DisableAll();
        }
    }
}