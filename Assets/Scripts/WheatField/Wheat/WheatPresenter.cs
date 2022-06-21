using System.Threading;
using System.Threading.Tasks;
using Base;
using UnityEngine;

namespace WheatField.Wheat
{
    public class WheatPresenter : BasePresenter<WheatModel, IWheatView>
    {
        private static readonly int UVCut = Shader.PropertyToID("_UVCut");
        private static readonly int Color = Shader.PropertyToID("_Color");

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

            while (Time.time <= startTime + _model.GrowUpTime)
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }

                var color = Vector4.Lerp(_model.InitialColor, _model.GrownUpColor,
                    (Time.time - startTime) / _model.GrowUpTime);
                var cutPart = Mathf.Lerp(0.1f, 1f, (Time.time - startTime) / _model.GrowUpTime);
                _view.MeshRenderer.material.SetFloat(UVCut, cutPart);
                _view.MeshRenderer.material.SetVector(Color, color);
                await Task.Yield();
            }
            
            _view.MeshRenderer.material.SetFloat(UVCut, 1f);
            _view.MeshRenderer.material.SetVector(Color, _model.GrownUpColor);
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