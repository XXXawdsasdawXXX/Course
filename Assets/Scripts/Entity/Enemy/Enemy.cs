using Common;
using Data.Entity;
using UnityEngine;

namespace Entity
{
    public sealed class Enemy : MonoEntity, IPoolable
    {
        [SerializeField] private ShootComponent _shoot;
        [SerializeField] private MoveToPointComponent _moveToPoint;
        
        private Transform _playerTransform;
        private Cooldown _attackCooldown;
        
        public void Warmup(IBulletSpawner bulletSpawner, Transform playerTransform, EnemyData data)
        {
            _attackCooldown = new Cooldown();
            
            _playerTransform = playerTransform;
    
            health.SetMax(data.maxHealth);
            
            _moveToPoint.SetSpeed(data.moveSpeed);
            _moveToPoint.AddCondition(() => !_moveToPoint.isPointReached);
            
            _shoot.Warmup(bulletSpawner, data.shoot);
            _shoot.AddCondition(() => _moveToPoint.isPointReached);
            
            _attackCooldown.SetRange(data.attackCooldownRange);
        }
        
        private void Update()
        {
            _attackCooldown.Run(onCompleted: Shoot);
        }

        private void FixedUpdate()
        {
            _moveToPoint.Execute();
        }

        public void Enable()
        {
            health.ResetCurrent();
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            health.ResetEvents();
            gameObject.SetActive(false);
        }

        public void SetPoints(Vector3 startPoint, Vector3 finishPoint)
        {
            transform.position = startPoint;
            
            _moveToPoint.SetDestination(finishPoint);
        }

        private void Shoot()
        {
            _shoot.SetDirection((_playerTransform.position - transform.position).normalized);
            _shoot.Execute();
        }
    }
}