using Barn;
using Base;
using CoinUI;
using CoinUI.Coin;
using ObjectPools;
using UnityEngine;

namespace Factories
{
    public class CoinUIFactory
    {
        private readonly BarnModel _barn;
        private readonly CoinFactory _coinFactory;
        private readonly CoinUIView _coinUIPrefab;
        private readonly UpdateHandler _updateHandler;
        private readonly Camera _uiCamera;

        public CoinUIFactory(BarnModel barn, CoinFactory coinFactory, CoinUIView coinUIPrefab,
            UpdateHandler updateHandler, Camera uiCamera)
        {
            _barn = barn;
            _coinFactory = coinFactory;
            _coinUIPrefab = coinUIPrefab;
            _updateHandler = updateHandler;
            _uiCamera = uiCamera;
        }

        public CoinUIModel CreateInstance()
        {
            var view = Object.Instantiate(_coinUIPrefab).Init(_uiCamera);
            var coinPool = new ObjectPool<CoinModel, CoinView, ICoinView, CoinPresenter>(40, _coinFactory).Init(view.Transform, view.CoinsUIPosition);
            var model = new CoinUIModel(_barn, coinPool);
            new CoinUIPresenter().Init<CoinUIPresenter>(model, view, _updateHandler);
            model.Init();
            return model;
        }
    }
}