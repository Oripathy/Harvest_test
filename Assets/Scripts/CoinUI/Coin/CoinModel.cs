using System;
using Base;
using ObjectPools;
using UnityEngine;

namespace CoinUI.Coin
{
    public class CoinModel : BaseModel, IObjectToPool
    {
        private RectTransform _destinationPosition;
        
        public float MovementTime { get; }

        public event Action<RectTransform> CoinSpawned;
        public event Action<Vector3> CoinPlaced;
        public event Action<IObjectToPool> ObjectShouldBeReturned;
        public event Action<bool> ActiveStateChanged;

        public CoinModel()
        {
            MovementTime = 0.5f;
        }

        private void OnCoinSpawned(RectTransform coinUIPosition)
        {
            CoinSpawned?.Invoke(coinUIPosition);
        }

        public void SetDestinationPosition(RectTransform position)
        {
            _destinationPosition = position;
        }

        public void OnCoinReachedDestination()
        {
            ObjectShouldBeReturned?.Invoke(this);
        }

        public IObjectToPool SetActive(bool isActive)
        {
            ActiveStateChanged?.Invoke(isActive);
            return this;
        }

        public void SetPosition(Vector3 position)
        {
            CoinPlaced?.Invoke(position);
            OnCoinSpawned(_destinationPosition);
        }
    }
}