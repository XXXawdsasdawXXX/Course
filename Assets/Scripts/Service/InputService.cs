using System;
using Data;
using UnityEngine;

namespace Service
{
    public class InputService : Mono, IInputService
    {
        public event Action MouseClicked; 

        public Vector2 direction { get; private set; }
        
        private void Update()
        {
            direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (Input.GetMouseButtonDown(0))
            {
                MouseClicked?.Invoke();
            }
        }
    }
}