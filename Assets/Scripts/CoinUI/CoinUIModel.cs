using System;
using Barn;
using Base;
using CoinUI.Coin;
using Factories;
using ObjectPools;
using UnityEngine;

namespace CoinUI
{
    public class CoinUIModel : BaseModel
    {
        private readonly BarnModel _barnModel;
        private readonly ObjectPool<CoinModel, CoinView, ICoinView, CoinPresenter> _coinPool;
        private int _coinsAmount;

        public ObjectPool<CoinModel, CoinView, ICoinView, CoinPresenter> CoinPool => _coinPool;
        public float ShakeDuration { get; }
        public bool IsShaking { get; set; }

        public event Action<int> CoinsAmountChanged;
        public event Action<Vector3> CubeSold;
        public event Action CoinUIPanelShouldBeShaked;
 
        public CoinUIModel(BarnModel barnModel, ObjectPool<CoinModel, CoinView, ICoinView, CoinPresenter> coinPool)
        {
            ShakeDuration = 0.5f;
            _barnModel = barnModel;
            _coinPool = coinPool;
        }

        public override void Init()
        {
            _barnModel.CubeSold += OnCubeSold;
            CoinsAmountChanged?.Invoke(_coinsAmount);
        }

        private void OnCubeSold(Vector3 position)
        {
            CubeSold?.Invoke(position);
        }

        public void SetCoin(IObjectToPool coin)
        {
            coin.ObjectShouldBeReturned += OnCoinReachedDestination;
        }
        
        private void OnCoinReachedDestination(IObjectToPool coin)
        {
            coin.ObjectShouldBeReturned -= OnCoinReachedDestination;
            _coinsAmount++;
            CoinsAmountChanged?.Invoke(_coinsAmount);
            _coinPool.ReturnToPool(coin);
            CoinUIPanelShouldBeShaked?.Invoke();
        }
    }
}