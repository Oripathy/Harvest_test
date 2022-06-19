using System;
using System.Collections.Generic;
using Barn;
using Base;
using CoinUI.Coin;
using Factories;
using UnityEngine;

namespace CoinUI
{
    public class CoinUIModel : BaseModel
    {
        private BarnModel _barnModel;
        private CoinFactory _coinFactory;
        private Vector3 _coinsUIPosition;
        private int _coinsAmount;

        public CoinFactory CoinFactory => _coinFactory;

        public event Action<int> CoinsAmountChanged;
        public event Action<Vector3> CubeSold;
 
        public CoinUIModel(BarnModel barnModel, CoinFactory coinFactory, Vector3 coinsUIPosition)
        {
            _barnModel = barnModel;
            _coinFactory = coinFactory;
            _coinsUIPosition = coinsUIPosition;
        }

        public override void Init()
        {
            _barnModel.CubeSold += OnCubeSold;
        }

        private void OnCubeSold(Vector3 position)
        {
            CubeSold?.Invoke(position);
        }
        
        public void SetCoin(CoinModel coinModel)
        {
            coinModel.CoinReachedDestination += OnCoinReachedDestination;
        }

        private void OnCoinReachedDestination(CoinModel coinModel)
        {
            coinModel.CoinReachedDestination -= OnCoinReachedDestination;
            _coinsAmount++;
            CoinsAmountChanged?.Invoke(_coinsAmount);
        }
    }
}