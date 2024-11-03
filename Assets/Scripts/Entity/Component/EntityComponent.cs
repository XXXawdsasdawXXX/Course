using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Entity
{
    public abstract class EntityComponent
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