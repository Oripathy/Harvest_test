using System;
using UnityEngine;

namespace Barn.SellPoint
{
    public class SellPointView : MonoBehaviour, ISellPointView
    {
        public Transform Transform => transform;
        
        public event Action CubeSold;
        public event Action ObjectDestroyed;

        public void OnCubeSold()
        {
            CubeSold?.Invoke();
        }

        private void OnDestroy()
        {
            ObjectDestroyed?.Invoke();
        }
    }
}