using System;
using Base;
using UnityEngine;

namespace CoinUI.Coin
{
    public class CoinModel : BaseModel
    {
         public float MovementTime { get; }

        public event Action<Vector3, float> CoinSpawned;
        public event Action<CoinModel> CoinReachedDestination;

        public CoinModel()
        {
            MovementTime = 0.5f;
        }

        public void OnCoinSpawned(Vector3 coinUIPosition, float scaleFactor)
        {
            CoinSpawned?.Invoke(coinUIPosition, scaleFactor);
        }

        public void OnCoinReachedDestination()
        {
            CoinReachedDestination?.Invoke(this);
        }
    }
}