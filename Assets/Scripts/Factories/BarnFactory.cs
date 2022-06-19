using Barn;
using Base;
using UnityEngine;

namespace Factories
{
    public class BarnFactory
    {
        private BarnView _barnPrefab;
        private UpdateHandler _updateHandler;

        public BarnFactory(BarnView barnPrefab, UpdateHandler updateHandler)
        {
            _barnPrefab = barnPrefab;
            _updateHandler = updateHandler;
        }

        public BarnModel CreateInstance(Vector3 position)
        {
            var view = GameObject.Instantiate(_barnPrefab, position, Quaternion.identity);
            var model = new BarnModel();
            var presenter = new BarnPresenter().Init<BarnPresenter>(model, view, _updateHandler);
            return model;
        }
    }
}