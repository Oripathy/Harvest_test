using Base;
using UnityEngine;
using WheatField.Wheat;

namespace Factories
{
    public class WheatFactory
    {
        private readonly WheatView _viewPrefab;
        private readonly UpdateHandler _updateHandler;
        
        public WheatFactory(UpdateHandler updateHandler, WheatView viewPrefab)
        {
            _updateHandler = updateHandler;
            _viewPrefab = viewPrefab;
        }

        public WheatModel CreateInstance(Vector3 position, Transform parent)
        {
            var model = new WheatModel().Init();
            var view = Object.Instantiate(_viewPrefab, parent, false);
            view.transform.localPosition = position;
            new WheatPresenter().Init<WheatPresenter>(model, view, _updateHandler);
            return model;
        }
    }
}