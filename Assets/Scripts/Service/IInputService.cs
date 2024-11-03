using System;
using UnityEngine;

namespace Service
{
    public interface IInputService
    {
        public event Action MouseClicked; 

        public Vector2 direction { get; }
    }
}