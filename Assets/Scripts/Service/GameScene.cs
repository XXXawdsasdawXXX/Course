﻿using Data;
using UnityEngine;

namespace Service
{
    public static class GameScene
    {
        public static TMono InstantiateMono<TOwner, TMono>(TMono prefab, Vector3 position, Quaternion rotation)
            where TMono : Mono
        {
            TMono mono = Object.Instantiate(prefab, position, rotation);
            
            mono.gameObject.name = $"{typeof(TOwner).FullName}.{prefab.name}";

            return mono;
        }
        
        public static TMono InstantiateMono<TOwner, TMono>(TMono prefab, Transform parent)
            where TMono : Mono 
        {
            TMono mono = Object.Instantiate(prefab, parent);
            
            mono.gameObject.name = $"{typeof(TOwner).FullName}.{prefab.name}";

            return mono;
        }
    }
}