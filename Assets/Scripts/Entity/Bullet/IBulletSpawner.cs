using Data.Entity;
using UnityEngine;

namespace Entity
{
    public interface IBulletSpawner
    {
        public Bullet Spawn(BulletData bulletData, Vector3 position, Vector3 direction);

        public void Despawn(Bullet bullet);
    }
}