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
            return this as TPresenter;
        }

        private async void OnCoinSpawned(Vector3 coinsUIPosition, float scaleFactor)
        {
            await MoveCoin(coinsUIPosition, scaleFactor);
            _view.DestroyCoin();
            _model.OnCoinReachedDestination();
            Debug.Log(_view.RectTransform.position + " " + coinsUIPosition);
        }

        private async Task MoveCoin(Vector3 position, float scaleFactor)
        {
            var initialPosition = _view.RectTransform.anchoredPosition;
            var startTime = Time.time;

            while (Time.time <= startTime + _model.MovementTime)
            {
                _view.RectTransform.anchoredPosition =
                    Vector3.Lerp(initialPosition, position, (Time.time - startTime) / _model.MovementTime);
                await Task.Yield();
            }
        }
    }
}