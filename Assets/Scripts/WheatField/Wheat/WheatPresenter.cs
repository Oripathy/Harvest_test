using System.Threading;
using System.Threading.Tasks;
using Base;
using UnityEngine;

namespace WheatField.Wheat
{
    public class WheatPresenter : BasePresenter<WheatModel, IWheatView>
    {
        public override TPresenter Init<TPresenter>(WheatModel model, IWheatView view, UpdateHandler updateHandler)
        {
            base.Init<TPresenter>(model, view, updateHandler);
            _view.WheatHarvested += OnWheatHarvested;
            return this as TPresenter;
        }

        private async Task GrowUp(CancellationToken token)
        {
            var startTime = Time.time;
            SetWheatActive(false);

            while (Time.time <= startTime + _model?.GrowUpTime)
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }

                _view.Transform.localScale = Vector3.Lerp(_model.InitialScale, _model.GrownUpScale,
                    (Time.time - startTime) / _model.GrowUpTime);
                await Task.Yield();
            }

            SetWheatActive(true);
        }

        private async void OnWheatHarvested()
        {
            var token = _model.CreateCancellationTokenSource().Token;
            _model.OnHarvested(_view.Transform.position);
            await GrowUp(token);
        }

        private void SetWheatActive(bool isActive)
        {
            if (_view != null)
                _view.Collider.enabled = isActive;
        }

        public override void Dispose()
        {
            _view.WheatHarvested -= OnWheatHarvested;
            base.Dispose();
        }
    }
}