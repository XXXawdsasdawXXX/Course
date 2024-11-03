using System;
using Common.Pools;
using Data;
using UnityEngine;

namespace Entities.Bullets
{
    public sealed class Bullet : Mono, IPoolable
    {
        public event Action<Bullet, Collision2D> OnCollisionEntered;
        
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private Vector2 _velocity;
        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollisionEntered?.Invoke(this, collision);
        }

        public void Enable()
        {
            gameObject.SetActive(true);
            _rigidbody2D.velocity = _velocity;
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public void SetData(BulletData bulletData)
        {
            transform.position = bulletData.Position;
            _spriteRenderer.color = bulletData.Color;
            _velocity = bulletData.Velocity;
        }
    }
}