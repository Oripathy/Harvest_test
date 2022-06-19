using System.Threading.Tasks;
using Base;
using UnityEngine;

namespace Coins
{
    public class CoinPresenter : BasePresenter<CoinUIModel, ICoinView>
    {
        public override TPresenter Init<TPresenter>(CoinUIModel model, ICoinView view, UpdateHandler updateHandler)
        {
            base.Init<TPresenter>(model, view, updateHandler);
            _model.CoinSpawned += OnCoinSpawned;
            return this as TPresenter;
        }

        private async void OnCoinSpawned(Vector3 coinsUIPosition)
        {
            await MoveCoin(coinsUIPosition);
            _view.DestroyCoin();
            _model.OnCoinDestroyed();
        }

        private async Task MoveCoin(Vector3 position)
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