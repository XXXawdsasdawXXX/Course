using System;
using Data.Value.RangeFloat;
using UnityEngine;

namespace Entities
{
    [Serializable]
    public struct EnemyData
    {
        [SerializeField, MinMaxRangeFloat(0,5)] private RangedFloat _attackCooldownRange;
        public RangedFloat AttackCooldownRange => _attackCooldownRange;

        [SerializeField] private ShootData _shoot;
        public ShootData Shoot => _shoot;

        [SerializeField] private float _moveSpeed;
        public float MoveSpeed => _moveSpeed;

        [SerializeField] private int _maxHealth;
        public int MaxHealth => _maxHealth;
    }
}