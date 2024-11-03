using Entities;
using Entities.Bullets;
using UnityEngine;

namespace Services
{
    public class GameLoop : MonoBehaviour
    {
        [SerializeField] private PlayerController _player;
        [SerializeField] private BulletSpawner _bulletSpawner;
        [SerializeField] private EnemiesController _enemiesController;
        
        private void Awake()
        {
            _player.Lose += () =>
            {
                _bulletSpawner.Reload();
                _enemiesController.Reload();
                _player.Reload();
            };
        }
    }
}