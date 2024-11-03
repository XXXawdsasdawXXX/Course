using UnityEngine;

namespace Entities
{
    public class MoveToPointComponent : EntityComponent
    {
        public bool isPointReached { get; private set; }

        [SerializeField] private Rigidbody2D _rigidbody;

        private float _speed = 1;
        private Vector2 _destination;

        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            isPointReached = false;
        }

        public override void Execute()
        {
            if (!IsCan())
            {
                return;
            }

            Vector2 direction = _destination - (Vector2)transform.position;
            if (direction.magnitude <= 0.25f)
            {
                isPointReached = true;
                _rigidbody.velocity = Vector2.zero;
                return;
            }

            _rigidbody.velocity = direction * _speed * Time.deltaTime;
        }

        public void SetSpeed(float speed)
        {
            _speed = speed;
        }
    }
}