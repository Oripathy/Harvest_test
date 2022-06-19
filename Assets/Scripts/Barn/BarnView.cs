using System;
using UnityEngine;

namespace Barn
{
    public class BarnView : MonoBehaviour, IBarnView
    {
        [SerializeField] private Transform _sellPoint;

        public Vector3 SellPoint => _sellPoint.position;
        
        private void Awake()
        {
            transform.rotation *= new Quaternion(0f, (float)Math.Sin(270 * Math.PI / 180 / 2), 0f,
                (float)Math.Cos(270 * Math.PI / 180 / 2));
        }
    }
}