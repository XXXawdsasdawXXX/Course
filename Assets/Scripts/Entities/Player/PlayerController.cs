using System;
using Entities.Bullets;
using Services;
using UnityEngine;

namespace Entities
{
    public sealed class PlayerController : MonoBehaviour
    {
        public event Action Lose;

        [SerializeField] private PointService _pointService;
        [SerializeField] private PlayerData _data;
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private BulletSpawner _bulletSpawner;
        [SerializeField] private InputService _input;

        private Player _player;
        private void Awake()
        {
            _player = GameScene.InstantiateMono(
                this, 
                _playerPrefab, 
                _pointService.GetPlayerSpawnPosition(), 
                Quaternion.identity);
            _player.transform.SetParent(transform);
        }

        private void Start()
        {
            _player.Warmup(_input, _bulletSpawner, _data);
            
            _player.Health.Died += () => Lose?.Invoke();
        }

        public Transform GetTransform()
        {
            return _player.transform;
        }
        public void Reload()
        {
            _player.ResetPosition();
            _player.Health.ResetCurrent();
        }
    }
}