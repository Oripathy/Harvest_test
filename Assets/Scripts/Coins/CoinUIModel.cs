using System;
using Barn;
using Base;
using Factories;
using UnityEngine;

namespace Coins
{
    public class CoinUIModel : BaseModel
    {
        private BarnModel _barnModel;
        private CoinFactory _coinFactory;
        private Vector3 _coinsUIPosition;
        private int _coinsAmount;

        public float MovementTime { get; }

        public event Action<int> CoinsAmountChanged;
        public event Action<Vector3> CoinSpawned;

        public CoinUIModel(BarnModel barnModel, CoinFactory coinFactory, Vector3 coinsUIPosition)
        {
            MovementTime = 0.5f;
            _barnModel = barnModel;
            _coinFactory = coinFactory;
            _coinsUIPosition = coinsUIPosition;
        }

        public void Init()
        {
            _barnModel.CubeSold += SpawnCoin;
        }

        private void SpawnCoin(Vector3 position)
        {
            var pos = Camera.main.WorldToScreenPoint(position);
            _coinFactory.CreateInstance(pos);
            CoinSpawned?.Invoke(_coinsUIPosition);
        }

        public void OnCoinDestroyed()
        {
            _coinsAmount++;
            CoinsAmountChanged?.Invoke(_coinsAmount);
        }
    }
}