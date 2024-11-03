using System;
using Data;
using Data.Entity;
using Service;
using UnityEngine;

namespace Entity
{
    public sealed class PlayerController : Mono
    {
        public event Action Lose;

        [SerializeField] private PlayerData _data;
        [SerializeField] private Player _playerPrefab;
        
        [Space, Header("Dependencies")]
        [SerializeField] private PointService _pointService;
        [SerializeField] private BulletSpawner _bulletSpawner;
        [SerializeField] private InputService _input;

        private Player _player;
        
        private void Awake()
        {
            _player = GameScene.InstantiateMono<PlayerController, Player>(_playerPrefab, 
                _pointService.GetPlayerSpawnPosition(), 
                Quaternion.identity);
            
            _player.transform.SetParent(transform);
        }

        private void Start()
        {
            _player.Warmup(_input, _bulletSpawner, _data);

            _player.health.Died += InvokeLose;
        }

        private void OnDestroy()
        {
            _player.Dispose();
            
            _player.health.Died -= InvokeLose;
        }

        public Transform GetTransform()
        {
            return _player.transform;
        }
        
        public void Reload()
        {
            _player.ResetPosition();
            _player.health.ResetCurrent();
        }

        private void InvokeLose()
        {
            Lose?.Invoke();
        }
    }
}