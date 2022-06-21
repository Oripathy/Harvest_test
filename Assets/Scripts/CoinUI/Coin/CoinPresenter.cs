using System.Threading;
using System.Threading.Tasks;
using Base;
using UnityEngine;

namespace CoinUI.Coin
{
    public class CoinPresenter : BasePresenter<CoinModel, ICoinView>
    {
        public override TPresenter Init<TPresenter>(CoinModel model, ICoinView view, UpdateHandler updateHandler)
        {
            base.Init<TPresenter>(model, view, updateHandler);
            _model.CoinSpawned += OnCoinSpawned;
            _model.CoinPlaced += OnCoinPlaced;
            _model.ActiveStateChanged += OnActiveStateChanged;
            return this as TPresenter;
        }

        private async void OnCoinSpawned(RectTransform coinsUIPosition)
        {
            var token = _model.Source?.Token ?? _model.CreateCancellationTokenSource().Token;
            await MoveCoin(coinsUIPosition, token);
            _model.OnCoinReachedDestination();
        }

        private async Task MoveCoin(RectTransform position, CancellationToken token)
        {
            await Task.Delay(200, token);
            var initialPosition = _view.RectTransform.position;
            var destinationPosition = position.position;
            var startTime = Time.time;
            
            while (Time.time <= startTime + _model?.MovementTime)
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }
                
                _view.RectTransform.position =
                    Vector3.Lerp(initialPosition, destinationPosition, (Time.time - startTime) / _model.MovementTime);
                await Task.Yield();
            }
            
            _view.RectTransform.position = destinationPosition;
        }
        
        private async void OnActiveStateChanged(bool isActive)
        {
            var token = _model.Source?.Token ?? _model.CreateCancellationTokenSource().Token;
            await ChangeActiveState(isActive, token);
        }
        private async Task ChangeActiveState(bool isActive, CancellationToken token)
        {
            if (isActive)
                await Task.Delay(200, token);

            if (token.IsCancellationRequested)
            {
                return;
            }
            
            _view.SetCoinActive(isActive);
        }

        private void OnCoinPlaced(Vector3 position)
        {
            _view.RectTransform.anchoredPosition = position;
        }

        public override void Dispose()
        {
            _model.CoinSpawned -= OnCoinSpawned;
            _model.CoinPlaced -= OnCoinPlaced;
            _model.ActiveStateChanged -= OnActiveStateChanged;
            base.Dispose();
        }
    }
}