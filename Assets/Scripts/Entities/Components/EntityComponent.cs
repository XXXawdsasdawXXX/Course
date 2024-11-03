using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Entities
{
    public abstract class EntityComponent : MonoBehaviour
    {
        private readonly List<Func<bool>> _conditions = new();
        public abstract void Execute();

        public void AddCondition(Func<bool> condition)
        {
            _conditions.Add(condition);
        }

        protected bool IsCan()
        {
            return _conditions.All(condition => condition.Invoke());
        }
    }
}