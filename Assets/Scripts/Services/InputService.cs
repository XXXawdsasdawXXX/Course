using System;
using Data;
using UnityEngine;

namespace Services
{
    public class InputService : Mono, IInputService
    {
        public event Action MouseClicked; 

        public Vector2 Direction { get; private set; }
        
        private void Update()
        {
            Direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (Input.GetMouseButtonDown(0))
            {
                MouseClicked?.Invoke();
            }
        }
    }
}