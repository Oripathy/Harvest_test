using System.Data;
using Barn;
using Barn.SellPoint;
using Base;
using UnityEngine;

namespace Factories
{
    public class BarnFactory : Factory<BarnModel, BarnView, IBarnView, BarnPresenter>
    {
        public BarnFactory(UpdateHandler updateHandler, BarnView barnPrefab) : base(updateHandler, barnPrefab)
        {
        }

        public override BarnModel CreateInstance(Vector3 position)
        {
            var view = GameObject.Instantiate(_viewPrefab, position, Quaternion.identity);
            var model = new BarnModel();
            var presenter = new BarnPresenter().Init<BarnPresenter>(model, view, _updateHandler);
            var sellPointView = view.SellP.GetComponent<SellPointView>();
            var sellPointPresenter = new SellPointPresenter().Init<SellPointPresenter>(model, sellPointView, _updateHandler);
            return model;
        }
    }
}