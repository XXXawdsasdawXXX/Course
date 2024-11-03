using System;
using Data.Entity;
using UnityEngine;

namespace Data
{
    [Serializable]
    public struct ShootData
    {
        [SerializeField] private Vector2 _shootOffset;
        public Vector2 shootOffset => _shootOffset;

        [SerializeField] private BulletData _bullet;
        public BulletData bullet => _bullet;
    }
}