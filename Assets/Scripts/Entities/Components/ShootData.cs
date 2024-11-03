using System;
using Common;
using UnityEngine;

namespace Entities
{
    [Serializable]
    public struct ShootData
    {
        [SerializeField] private int _damage;
        public int Damage => _damage;
        
        [SerializeField] private float _bulletSpeed;
        public float BulletSpeed => _bulletSpeed;
        
        [SerializeField] private Color _bulletColor;
        public Color BulletColor => _bulletColor;
        
        [SerializeField] private PhysicsLayer _physicsLayer;
        public PhysicsLayer Layer => _physicsLayer;
        
        [SerializeField] private Vector2 _offset;
        public Vector2 Offset => _offset;
    }
}