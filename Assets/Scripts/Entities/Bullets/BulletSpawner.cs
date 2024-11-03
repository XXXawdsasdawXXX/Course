using Common.Pools;
using UnityEngine;

namespace Entities.Bullets
{
    public sealed class BulletSpawner : MonoBehaviour, IBulletSpawner
    {
        [SerializeField] private Pool<Bullet> _bulletPool;
        
        public Bullet Spawn(BulletData bulletData)
        {
            Bullet bullet = _bulletPool.GetNext();
            bullet.SetData(bulletData);
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