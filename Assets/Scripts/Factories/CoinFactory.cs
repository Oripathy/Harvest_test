using Base;
using CoinUI.Coin;
using UnityEngine;

namespace Factories
{
    public class CoinFactory : Factory<CoinModel, CoinView, ICoinView, CoinPresenter>
    {
        public CoinFactory(UpdateHandler updateHandler, CoinView viewPrefab) : base(updateHandler, viewPrefab)
        {
        }
        
        public override CoinModel CreateInstance(Transform parent, RectTransform destinationPosition)
        {
            var view = Object.Instantiate(_viewPrefab, parent, false);
            var model = new CoinModel();
            view.gameObject.SetActive(false);
            model.SetDestinationPosition(destinationPosition);
            new CoinPresenter().Init<CoinPresenter>(model, view, _updateHandler);
            model.Init();
            return model;
        }
    }
}