using Data.Entity;
using Service;
using UnityEngine;

namespace Entity
{
    public class Player : MonoEntity
    {
        [SerializeField] private DirectionMovementComponent _directionMovement;
        [SerializeField] private ShootComponent _shoot;
       
        private IInputService _input;
        private Vector3 _startPosition;
        
        public void Warmup(IInputService input, IBulletSpawner bulletSpawner, PlayerData data)
        {
            _startPosition = transform.position;
            
            _input = input;
            
            _shoot.Warmup(bulletSpawner, data.shoot);
            _shoot.SetDirection(Vector2.up);
            _input.MouseClicked += _shoot.Execute;

            _directionMovement.SetSpeed(data.moveSpeed);

            health.SetMax(data.maxHealth);
            health.ResetCurrent();
        }

        private void FixedUpdate()
        {
            _directionMovement.SetDirection(_input.direction * Time.fixedDeltaTime);
            _directionMovement.Execute();
        }

        public void ResetPosition()
        {
            transform.position = _startPosition;
        }

        public void Dispose()
        {
            _input.MouseClicked -= _shoot.Execute;
        }
    }
}