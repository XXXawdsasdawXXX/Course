using System;
using UnityEngine;

namespace Entity
{
    [Serializable]
    public class MoveToPointComponent : EntityComponent
    {
        private const float THRESHOLD_DISTANCE = 0.25f;
        public bool isPointReached { get; private set; }

        [SerializeField] private Rigidbody2D _rigidbody;

        private Vector2 _target;
        private float _speed;

        public void SetDestination(Vector2 target)
        {
            _target = target;
            isPointReached = false;
        }

        public override void Execute()
        {
            if (!IsCan())
            {
                return;
            }

            Vector2 direction = _target - _rigidbody.position;
            if (direction.sqrMagnitude <= THRESHOLD_DISTANCE * THRESHOLD_DISTANCE)
            {
                isPointReached = true;
                _rigidbody.velocity = Vector2.zero;
                return;
            }

            _rigidbody.velocity = direction.normalized * _speed * Time.deltaTime;
        }

        public void SetSpeed(float speed)
        {
            _speed = speed;
        }
    }
}