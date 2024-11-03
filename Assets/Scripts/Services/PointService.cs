using UnityEngine;

namespace Services
{
    public class PointService : MonoBehaviour
    {
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private Transform[] _enemySpawnPoints; 
        [SerializeField] private Transform[] _enemyAttackPoints; 

        private int _spawnPointIndex;
        private int _attackPointIndex;

        public Vector3 GetPlayerSpawnPosition()
        {
            return _playerSpawnPoint.position;
        }

        public Vector3 GetSpawnPoint()
        {
            Vector3 position = _enemySpawnPoints[_spawnPointIndex].position;
            
            _spawnPointIndex++;
            
            if (_spawnPointIndex == _enemySpawnPoints.Length)
            {
                _spawnPointIndex = 0;
            }
            
            return position;
        }
        
        public Vector3 GetAttackPoint()
        {
            Vector3 position = _enemyAttackPoints[_attackPointIndex].position;
            
            _attackPointIndex++;
            
            if (_attackPointIndex == _enemyAttackPoints.Length)
            {
                _attackPointIndex = 0;
            }
            
            return position;
        }
    }
}