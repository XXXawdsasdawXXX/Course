namespace Entities.Bullets
{
    public interface IBulletSpawner
    {
        public Bullet Spawn(BulletData bulletData);
        public void Despawn(Bullet bullet);
    }
}