using Barn;
using Base;
using CoinUI;
using UnityEngine;

namespace Factories
{
    public class CoinUIFactory
    {
        private BarnModel _barn;
        private CoinFactory _coinFactory;
        private CoinUIView _coinUIPrefab;
        private UpdateHandler _updateHandler;

        public CoinUIFactory(BarnModel barn, CoinFactory coinFactory, CoinUIView coinUIPrefab, UpdateHandler updateHandler)
        {
            _barn = barn;
            _coinFactory = coinFactory;
            _coinUIPrefab = coinUIPrefab;
            _updateHandler = updateHandler;
        }

        public CoinUIModel CreateInstance()
        {
            var view = GameObject.Instantiate(_coinUIPrefab);
            var model = new CoinUIModel(_barn, _coinFactory, view.CoinsUIPosition.position);
            var presenter = new CoinUIPresenter().Init<CoinUIPresenter>(model, view, _updateHandler);
            model.Init();
            return model;
        }
    }
}