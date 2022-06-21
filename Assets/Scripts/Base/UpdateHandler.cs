using System;
using System.Collections;
using UnityEngine;

namespace Base
{
    public class UpdateHandler : MonoBehaviour
    {
        public event Action UpdateTicked;

        private void Update() => UpdateTicked?.Invoke();
    }
}