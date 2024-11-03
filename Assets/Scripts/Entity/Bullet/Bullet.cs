using System;
using Common;
using Data;
using Data.Entity;
using UnityEngine;

namespace Entity
{
    public sealed class Bullet : Mono, IPoolable
    {
        public event Action<Bullet, Collision2D> OnCollisionEntered;
        
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private BulletData _data;
        private Vector2 _velocity;
        private Vector3 _direction;

        private void OnCollisionEnter2D(Collision2D collision)
        {  
            if (collision.gameObject.TryGetComponent(out MonoEntity entity))
            {
                entity.health.Remove(_data.damage);
            }
            
            OnCollisionEntered?.Invoke(this, collision);
        }

        public void Enable()
        {
            gameObject.SetActive(true);
            _rigidbody2D.velocity = _direction * _data.speed;
        }

        public void Disable()
        {
            gameObject.SetActive(false);
            _rigidbody2D.velocity = Vector2.zero;
        }

        public void SetData(BulletData bulletData)
        {
            _data = bulletData;
            _spriteRenderer.color = _data.color;
            gameObject.layer = LayerMask.NameToLayer(_data.layer + "Bullet");
        }

        public void SetDirection(Vector3 direction)
        {
            _direction = direction;
        }
    }
}