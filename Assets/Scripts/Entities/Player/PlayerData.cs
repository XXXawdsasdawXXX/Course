using System;
using UnityEngine;

namespace Entities
{
    [Serializable]
    public struct PlayerData
    {
        [SerializeField] private ShootData _shoot;
        public ShootData Shoot => _shoot;

        [SerializeField] private float _moveSpeed;
        public float MoveSpeed => _moveSpeed;

        [SerializeField] private int _maxHealth;
        public int MaxHealth => _maxHealth;
    }
}