using System;
using UnityEngine;

namespace Entities
{
    public class HealthParam : MonoBehaviour
    {
        public event Action<int> Changed;
        public event Action Died;
        
        [SerializeField] private int _currentValue = 1;
        [SerializeField] private int _maxValue = 1;

        public void SetMax(int maxHealth)
        {
            _maxValue = maxHealth;
        }

        public void ResetCurrent()
        {
            _currentValue = _maxValue;
        }

        public void Remove(int value)
        {
            _currentValue -= value;
            
            if (_currentValue <= 0)
            {
                Died?.Invoke();
            }
            
            Changed?.Invoke(_currentValue);
        }

        public void ResetEvents()
        {
            Changed = null;
            Died = null;
        }
    }
}