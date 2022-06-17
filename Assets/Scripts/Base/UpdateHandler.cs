using System;
using System.Collections;
using UnityEngine;

namespace Base
{
    public class UpdateHandler : MonoBehaviour
    {
        public event Action UpdateTicked;

        private void Update() => UpdateTicked?.Invoke();
        public void ExecuteCoroutine(IEnumerator coroutine) => StartCoroutine(coroutine);
    }
}