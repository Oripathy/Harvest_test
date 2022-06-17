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

       private async Task GrowUp()
       {
           var pos = _view.Transform.position + new Vector3(0f, 0.5f, 0f);
           _model.WheatCubeFactory.CreateInstance(pos);
            var startTime = Time.time;
            SetWheatActive(false);
            
            while (Time.time <= startTime + _model.GrowUpTime)
            {
                _view.Transform.localScale = Vector3.Lerp(_model.InitialScale, _model.GrownUpScale,
                    (Time.time - startTime) / _model.GrowUpTime);
                await Task.Yield();
            }
        }
       
       // private IEnumerator GrowUp()
       // {
       //     var startTime = Time.time;
       //     SetWheatActive(false);
       //      
       //     while (Time.time <= startTime + _model.GrowUpTime)
       //     {
       //         _view.Transform.localScale = Vector3.Lerp(_model.InitialScale, _model.GrownUpScale,
       //             (Time.time - startTime) / _model.GrowUpTime);
       //         // var startPos = new Vector3(_view.Transform.position.x, _model.InitialScale.y / 2,
       //         //     _view.Transform.position.z);
       //         // var endPos = new Vector3(_view.Transform.position.x, _model.GrownUpScale.y / 2,
       //         //     _view.Transform.position.z);
       //         // _view.Transform.position = Vector3.Lerp(startPos, endPos, (Time.time - startTime) / _model.GrowUpTime);
       //         yield return null;
       //     }
       // }

        private async void OnWheatHarvested()
        {
            await GrowUp();
            SetWheatActive(true);
        }

        // private void OnWheatHarvested()
        // {
        //     _updateHandler.ExecuteCoroutine(GrowUp());
        //     SetWheatActive(true);
        // }

        private void SetWheatActive(bool isActive)
        {
            _view.Collider.enabled = isActive;
            // _view.GrownUpWheat.SetActive(isActive);
            // _view.WheatLowerPart.SetActive(!isActive);
            // _view.WheatUpperPart.SetActive(!isActive);
        }
    }
}