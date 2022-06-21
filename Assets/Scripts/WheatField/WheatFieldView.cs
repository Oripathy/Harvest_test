using System;
using UnityEngine;

namespace WheatField
{
    public class WheatFieldView : MonoBehaviour, IWheatFieldView
    {
        public Transform Transform => transform;

        public event Action ObjectDestroyed;
        
        private void OnDestroy()
        {
            ObjectDestroyed?.Invoke();
        }
    }
}