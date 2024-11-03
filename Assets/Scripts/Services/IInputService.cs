using System;
using UnityEngine;

namespace Services
{
    public interface IInputService
    {
        public event Action MouseClicked; 

        public Vector2 Direction { get; }
    }
}