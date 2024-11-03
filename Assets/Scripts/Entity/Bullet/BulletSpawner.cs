using Common;
using Data.Entity;
using UnityEngine;

namespace Entity
{
    public sealed class BulletSpawner : MonoBehaviour, IBulletSpawner
    {
        [SerializeField] private Pool<Bullet> _bulletPool;
        
        public Bullet Spawn(BulletData bulletData, Vector3 position, Vector3 direction)
        {
            Bullet bullet = _bulletPool.GetNext();
            bullet.transform.position = position;
            
            bullet.SetData(bulletData);
            bullet.SetDirection(direction);
            
            _bulletPool.Enable(bullet);
            
            return bullet;
        }

        public void Despawn(Bullet bullet)
        {
            _bulletPool.Disable(bullet);
        }

        public void Reload()
        {
           _bulletPool.DisableAll();
        }
    }
}