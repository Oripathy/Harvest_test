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

        public CoinModel CreateInstance(Vector3 position, Transform parent)
        {
            var view = GameObject.Instantiate(_viewPrefab, parent, false);
            view.RectTransform.anchoredPosition = position;
            var model = new CoinModel();
            var presenter = new CoinPresenter().Init<CoinPresenter>(model, view, _updateHandler);
            model.Init();
            return model;
        }
    }
}