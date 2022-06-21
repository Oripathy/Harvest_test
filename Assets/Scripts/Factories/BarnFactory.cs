using Barn;
using Barn.SellPoint;
using Base;
using UnityEngine;

namespace Factories
{
    public class BarnFactory
    {
        private readonly BarnView _barnViewPrefab;
        private readonly UpdateHandler _updateHandler;

        public BarnFactory(UpdateHandler updateHandler, BarnView barnViewPrefab)
        {
            _updateHandler = updateHandler;
            _barnViewPrefab = barnViewPrefab;
        }

        public BarnModel CreateInstance(Vector3 position)
        {
            var view = Object.Instantiate(_barnViewPrefab, position, Quaternion.identity);
            var model = new BarnModel();
            new BarnPresenter().Init<BarnPresenter>(model, view, _updateHandler);
            var sellPointView = view.SellP.GetComponent<SellPointView>();
            new SellPointPresenter().Init<SellPointPresenter>(model, sellPointView, _updateHandler);
            return model;
        }
    }
}