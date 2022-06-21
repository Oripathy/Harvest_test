using System;
using System.Threading.Tasks;
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
            _model.CoinUIPanelShouldBeShaked += ShakeCoinUIPanel;
            return this as TPresenter;
        }

        private void OnCoinsAmountChanged(int coinsAmount)
        {
            _view.UpdateCoinsAmount(coinsAmount);
        }

        private void SpawnCoin(Vector3 position)
        {
            var pos = _view.Camera.WorldToScreenPoint(position);
            
            if (_model.CoinPool.TryReleaseObject(pos / _view.Canvas.scaleFactor, out var coin))
            {
                _model.SetCoin(coin);
            }
        }

        private async void ShakeCoinUIPanel()
        {
            if (_model.IsShaking)
                return;

            _model.IsShaking = true;
            var startTime = Time.time;
            var position = _view.CoinUIPanel.localPosition;
            var positionDelta = new Vector3(5f, 0f, 0f);
            var frequency = 2 * Math.PI / _model.ShakeDuration;
            
            while (Time.time <= startTime + _model.ShakeDuration)
            {
                _view.CoinUIPanel.localPosition  = position + positionDelta * (float) Math.Sin(3 * frequency * (Time.time - startTime));
                await Task.Yield();
            }

            _view.CoinUIPanel.localPosition = position;
            _model.IsShaking = false;
        }

        public override void Dispose()
        {
            _model.CoinsAmountChanged -= OnCoinsAmountChanged;
            _model.CubeSold -= SpawnCoin;
            _model.CoinUIPanelShouldBeShaked -= ShakeCoinUIPanel;
            base.Dispose();
        }
    }
}