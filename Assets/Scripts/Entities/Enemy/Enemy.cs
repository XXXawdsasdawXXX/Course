using Common;
using Common.Pools;
using Entities.Bullets;
using UnityEngine;

namespace Entities
{
    public sealed class Enemy : Entity, IPoolable
    {
        [SerializeField] private ShootComponent _shoot;
        [SerializeField] private MoveToPointComponent _moveToPoint;
        
        private Transform _playerTransform;
        private Cooldown _attackCooldown;
        
        public void Warmup(IBulletSpawner bulletSpawner, Transform playerTransform, EnemyData data)
        {
            _attackCooldown = new Cooldown();
            
            _playerTransform = playerTransform;
    
            Health.SetMax(data.MaxHealth);
            
            _moveToPoint.SetSpeed(data.MoveSpeed);
            _moveToPoint.AddCondition(() => !_moveToPoint.isPointReached);
            
            _shoot.Inject(bulletSpawner, data.Shoot, Layer);
            _shoot.AddCondition(() => _moveToPoint.isPointReached);
            
            _attackCooldown.SetRange(data.AttackCooldownRange);
        }
        
        private void Update()
        {
            _attackCooldown.Run(() =>
            {
                _shoot.SetDirection(_playerTransform.position - transform.position);
                
                _shoot.Execute();
            });
        }

        private void FixedUpdate()
        {
            _moveToPoint.Execute();
        }

        public void Enable()
        {
            Health.ResetCurrent();
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            Health.ResetEvents();
            gameObject.SetActive(false);
        }

        public void SetPoints(Vector3 startPoint, Vector3 finishPoint)
        {
            transform.position = startPoint;
            
            _moveToPoint.SetDestination(finishPoint);
        }
    }
}