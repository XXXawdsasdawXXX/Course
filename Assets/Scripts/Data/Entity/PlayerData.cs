using System;
using UnityEngine;

namespace Data.Entity
{
    [Serializable]
    public struct PlayerData
    {
        [SerializeField] private ShootData _shoot;
        public ShootData shoot => _shoot;

        [SerializeField] private float _moveSpeed;
        public float moveSpeed => _moveSpeed;

        [SerializeField] private int _maxHealth;
        public int maxHealth => _maxHealth;
    }
}