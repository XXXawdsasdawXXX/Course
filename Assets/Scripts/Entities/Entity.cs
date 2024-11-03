using Common;
using Data;
using UnityEngine;

namespace Entities
{
    public abstract class Entity : Mono
    {
        [SerializeField] private PhysicsLayer _physicsLayer;
        public PhysicsLayer Layer => _physicsLayer;

        [SerializeField] private HealthParam _healthParam;
        public HealthParam Health => _healthParam;
    }
}