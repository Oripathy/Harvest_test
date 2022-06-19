using Base;
using UnityEngine;

namespace CoinUI
{
    public class CoinUIPresenter : BasePresenter<CoinUIModel, ICoinUIView>
    {
        public override TPresenter Init<TPresenter>(CoinUIModel model, ICoinUIView view, UpdateHandler updateHandler)
        {
            base.Init<TPresenter>(model, view, updateHandler);
            _model.CoinsAmountChanged += OnCoinsAmountChanged;
            _model.CubeSold += SpawnCoin;
            return this as TPresenter;
        }

        private void OnCoinsAmountChanged(int coinsAmount)
        {
            _view.UpdateCoinsAmount(coinsAmount);
        }

        private void SpawnCoin(Vector3 position)
        {
            var pos = Camera.main.WorldToScreenPoint(position);
            var coin = _model.CoinFactory.CreateInstance(pos / _view.Canvas.scaleFactor, _view.Transform);
            coin.OnCoinSpawned(_view.CoinsUIPosition.anchoredPosition, _view.Canvas.scaleFactor);
            _model.SetCoin(coin);
        }
    }
}