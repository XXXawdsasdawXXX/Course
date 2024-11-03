using System;
using Data;
using Entity;
using UnityEngine;

namespace Service
{
    public class GameLoop : Mono
    {
        [SerializeField] private PlayerController _player;
        [SerializeField] private BulletSpawner _bulletSpawner;
        [SerializeField] private EnemiesController _enemiesController;
        
        private void Awake()
        {
            _player.Lose += ReloadGame;
        }

        private void OnDestroy()
        {
            _player.Lose -= ReloadGame;
        }

        private void ReloadGame()
        {
            _bulletSpawner.Reload();
            _enemiesController.Reload();
            _player.Reload();
        }
    }
}