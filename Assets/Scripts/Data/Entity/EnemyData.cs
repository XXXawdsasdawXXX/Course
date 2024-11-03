using System;
using Data.RangeFloat;
using UnityEngine;

namespace Data.Entity
{
    [Serializable]
    public struct EnemyData
    {
        [SerializeField] private float _moveSpeed;
        public float moveSpeed => _moveSpeed;

        [SerializeField] private int _maxHealth;
        public int maxHealth => _maxHealth;
        
        [SerializeField, MinMaxRangeFloat(0,5)] private RangedFloat _attackCooldownRange;
        public RangedFloat attackCooldownRange => _attackCooldownRange;

        [SerializeField] private ShootData _shootData;
        public ShootData shoot => _shootData;
    }
}