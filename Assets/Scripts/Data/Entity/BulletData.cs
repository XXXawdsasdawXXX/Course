using System;
using Common;
using UnityEngine;

namespace Data.Entity
{
    [Serializable]
    public struct BulletData
    {
        [SerializeField] private int _damage;
        public int damage => _damage;
        
        [SerializeField] private float _speed;
        public float speed => _speed;
        
        [SerializeField] private Color _color;
        public Color color => _color;
        
        [SerializeField] private PhysicsLayer _physicsLayer;
        public PhysicsLayer layer => _physicsLayer;
    }
}