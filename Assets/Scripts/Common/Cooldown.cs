using System;
using Data.RangeFloat;
using UnityEngine;

namespace Common
{
    [Serializable]
    public class Cooldown
    {
        [SerializeField, MinMaxRangeFloat(0,10)] private RangedFloat _ranged;
        
        private float _current;
        
        public void Reset(bool isEmpty)
        {
            _current = isEmpty ? 0 : _ranged.GetRandomValue();
        }
        
        public void Run(Action onCompleted)
        {
            _current -= Time.deltaTime;
           
            if (_current <= 0)
            {
                _current = _ranged.GetRandomValue();
                
                onCompleted?.Invoke();
            }
        }

        public void SetRange(RangedFloat range)
        {
            _ranged = range;
        }
    }
}