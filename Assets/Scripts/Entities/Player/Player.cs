using Entities.Bullets;
using Services;
using UnityEngine;

namespace Entities
{
    public class Player : Entity
    {
        [SerializeField] private DirectionMovementComponent _directionMovement;
        [SerializeField] private ShootComponent _shoot;
       
        private IInputService _input;
        private Vector3 _startPosition;
        
        public void Warmup(IInputService input, IBulletSpawner bulletSpawner, PlayerData data)
        {
            _startPosition = transform.position;
            
            _input = input;
            
            _shoot.Inject(bulletSpawner, data.Shoot, Layer);
            _shoot.SetDirection(Vector2.up);
            _input.MouseClicked += () => _shoot.Execute();

            _directionMovement.SetSpeed(data.MoveSpeed);

            Health.SetMax(data.MaxHealth);
            Health.ResetCurrent();
        }

        private void FixedUpdate()
        {
            _directionMovement.SetDirection(_input.Direction * Time.fixedDeltaTime);
            _directionMovement.Execute();
        }

        public void ResetPosition()
        {
            transform.position = _startPosition;
        }
    }
}