using System;
using UnityEngine;

namespace Entity
{
    [Serializable]
    public class DirectionMovementComponent : EntityComponent
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
   
        private Vector2 _direction;
        private float _speed;

        public override void Execute()
        {
            if (!IsCan())
            {
                return;
            }
            
            _rigidbody2D.velocity = _direction * _speed;
        }

        public void SetSpeed(float speed)
        {
            _speed = speed;
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }
    }
}