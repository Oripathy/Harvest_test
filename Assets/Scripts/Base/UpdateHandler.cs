using System;
using UnityEngine;

namespace Base
{
    public class UpdateHandler : MonoBehaviour
    {
        public event Action UpdateTicked;

        private void Update() => UpdateTicked?.Invoke();
    }
}