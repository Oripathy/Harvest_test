using System;
using UnityEngine;

namespace Barn.SellPoint
{
    public class SellPointView : MonoBehaviour, ISellPointView
    {
        public Transform Transform => transform;
        
        public event Action CubeSold;

        public void OnCubeSold()
        {
            CubeSold?.Invoke();
            Debug.Log("Sold");
        }
    }
}