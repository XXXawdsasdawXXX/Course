using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Bullets;
using Common;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField]
        private Transform[] spawnPositions;

        [SerializeField]
        private Transform[] attackPositions;
        
        [SerializeField]
        private Player.Player character;

        [SerializeField]
        private Transform worldTransform;

        [SerializeField]
        private Transform container;

        [SerializeField]
        private Enemy prefab;
        
        [SerializeField]
        private BulletManager _bulletSystem;
        
        private readonly HashSet<Enemy> m_activeEnemies = new();
        private readonly Queue<Enemy> enemyPool = new();
        
        private void Awake()
        {
            for (var i = 0; i < 7; i++)
            {
                Enemy enemy = Instantiate(prefab, container);
                enemyPool.Enqueue(enemy);
            }
        }

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(1, 2));
                
                if (!enemyPool.TryDequeue(out Enemy enemy))
                {
                    enemy = Instantiate(prefab, container);
                }

                enemy.transform.SetParent(worldTransform);

                Transform spawnPosition = RandomPoint(spawnPositions);
                enemy.transform.position = spawnPosition.position;

                Transform attackPosition = RandomPoint(attackPositions);
                enemy.SetDestination(attackPosition.position);
                enemy.target = character;

                if (m_activeEnemies.Count < 5 && m_activeEnemies.Add(enemy))
                {
                    enemy.OnFire += OnFire;
                }
            }
        }

        private void FixedUpdate()
        {
            foreach (Enemy enemy in m_activeEnemies.ToArray())
            {
                if (enemy.health <= 0)
                {
                    enemy.OnFire -= OnFire;
                    enemy.transform.SetParent(container);

                    m_activeEnemies.Remove(enemy);
                    enemyPool.Enqueue(enemy);
                }
            }
        }

        private void OnFire(Vector2 position, Vector2 direction)
        {
            _bulletSystem.SpawnBullet(
                position,
                Color.red,
                (int) PhysicsLayer.ENEMY_BULLET,
                1,
                false,
                direction * 2
            );
        }

        private Transform RandomPoint(Transform[] points)
        {
            int index = Random.Range(0, points.Length);
            return points[index];
        }
    }
}