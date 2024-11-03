using System;
using Data;
using UnityEngine;

namespace Entity
{
    [Serializable]
    public class ShootComponent : EntityComponent
    {
        [SerializeField] private Transform _transform;
     
        private IBulletSpawner _bulletSpawner;
        private Vector3 _direction;
        private ShootData _data;

        public void Warmup(IBulletSpawner spawner, ShootData data)
        {
            _bulletSpawner = spawner;
            _data = data;
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        public override void Execute()
        {
            if (!IsCan() || _bulletSpawner == null)
            {
                return;
            }

            Bullet bullet = _bulletSpawner.Spawn(_data.bullet, GetBulletPosition(), _direction);

            bullet.OnCollisionEntered += OnBulletCollisionEntered;
        }

        private Vector3 GetBulletPosition()
        {
            return _transform.position + (Vector3)_data.shootOffset;
        }

        private void OnBulletCollisionEntered(Bullet bullet, Collision2D collision)
        {
            bullet.OnCollisionEntered -= OnBulletCollisionEntered;

            _bulletSpawner.Despawn(bullet);
        }
    }
}