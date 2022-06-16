using Base;
using UnityEngine;
using WheatField.Wheat;

namespace Factories
{
    public class WheatFactory : Factory<WheatModel, IWheatView, WheatPresenter>
    {
        private WheatView _viewPrefab;
        private UpdateHandler _updateHandler;
        
        public WheatFactory(UpdateHandler updateHandler, IWheatView viewPrefab) : base(updateHandler, viewPrefab)
        {
        }

        public override WheatModel CreateInstance(Vector3 position)
        {
            var model = new WheatModel();
            var view = GameObject.Instantiate(_viewPrefab, position, Quaternion.identity);
            var presenter = new WheatPresenter().Init<WheatPresenter>(model, view, _updateHandler);
            return model;
        }
    }
}