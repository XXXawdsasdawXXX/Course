using Common;
using Entities.Bullets;
using UnityEngine;

namespace Entities
{
    public class ShootComponent : EntityComponent
    {
        private IBulletSpawner _bulletSpawner;
        private Vector3 _direction;
        private ShootData _data;
        private PhysicsLayer _ownerLayer;

        public void Inject(IBulletSpawner spawner, ShootData data, PhysicsLayer ownerLayer)
        {
            _bulletSpawner = spawner;
            _data = data;
            _ownerLayer = ownerLayer;
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

            BulletData data = new(
                position: GetPosition(), 
                velocity: _direction * _data.BulletSpeed, 
                color: _data.BulletColor);

            Bullet bullet = _bulletSpawner.Spawn(data);
            bullet.gameObject.layer = LayerMask.NameToLayer(_ownerLayer + "Bullet");
            
            bullet.OnCollisionEntered += OnBulletCollisionEntered;
        }

        private Vector3 GetPosition()
        {
            return transform.position + (Vector3)_data.Offset;
        }

        private void OnBulletCollisionEntered(Bullet bullet, Collision2D collision)
        {
            bullet.OnCollisionEntered -= OnBulletCollisionEntered;
            
            if (collision.gameObject.TryGetComponent(out Entity entity))
            {
                if (entity.Layer == _ownerLayer)
                {
                    return;
                }

                entity.Health.Remove(_data.Damage);
            }
            
            _bulletSpawner.Despawn(bullet);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position + (Vector3)_data.Offset, 0.05f);
        }
    }
}