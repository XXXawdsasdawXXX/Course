using Data;
using UnityEngine;

namespace Entity
{
    public abstract class MonoEntity : Mono
    {
        [SerializeField] private HealthParam _healthParam;
        public HealthParam health => _healthParam;
    }
}