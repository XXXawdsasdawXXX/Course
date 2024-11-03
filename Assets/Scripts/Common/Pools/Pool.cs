using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Service;
using UnityEngine;

namespace Common
{
    [Serializable]
    public class Pool<TEntity> where TEntity : Mono, IPoolable
    {
        [SerializeField] protected Transform _root;
        [SerializeField] protected TEntity _prefab;
        [SerializeField] protected List<TEntity> _all = new();
        [SerializeField] protected List<TEntity> _enabled = new();

        public TEntity GetNext()
        {
            TEntity entity = GetDisabledEntity() ?? AddNewEntity();

            return entity;
        }

        public void Enable(TEntity entity)
        {
            _enabled.Add(entity);
            
            entity.Enable();
        }

        public void Disable(TEntity entity)
        {
            if (entity == null || !entity.gameObject.activeSelf)
            {
                return;
            }
            
            entity.Disable();
            
            _enabled.Remove(entity);
        }

        public IEnumerable<TEntity> GetAllEnabled()
        {
            return _enabled;
        }

        public int GetEnabledCount()
        {
            return _enabled.Count;
        }

        public void DisableAll()
        {
            foreach (TEntity entity in _all)
            {
                entity.Disable();
            }

            _enabled.Clear();
        }

        protected virtual TEntity AddNewEntity()
        {
            TEntity entity = GameScene.InstantiateMono<Pool<TEntity>, TEntity>(_prefab, _root);
            
            _all.Add(entity);
            
            return entity;
        }

        private TEntity GetDisabledEntity()
        {
            return _all.FirstOrDefault(entity => entity != null && !entity.gameObject.activeSelf);
        }
    }
}