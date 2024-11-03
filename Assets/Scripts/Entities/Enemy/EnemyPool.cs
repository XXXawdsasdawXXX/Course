using System;
using Common.Pools;
using Entities.Bullets;
using Services;
using UnityEngine;

namespace Entities
{
    [Serializable]
    public class EnemySpawner : Pool<Enemy>
    {
        private Transform _playerTransform;
        private IBulletSpawner _bullet;
        private EnemyData _data;
        
        public void Warmup(IBulletSpawner bulletSpawner, Transform player, EnemyData data)
        {
            _bullet = bulletSpawner;
            _playerTransform = player;
            _data = data;
        }
        
        protected override Enemy AddNewEntity()
        {
            Enemy entity = GameScene.InstantiateMono(this, _prefab, _root);
            entity.transform.rotation = Quaternion.Euler(0, 0, -180);
            
            entity.Warmup(_bullet, _playerTransform, _data);
            
            _all.Add(entity);
            
            return entity;
        }
    }
}