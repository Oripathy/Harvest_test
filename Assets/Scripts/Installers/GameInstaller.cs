using Barn;
using Base;
using CoinUI;
using CoinUI.Coin;
using Factories;
using Player;
using Player.InputHandler;
using Player.PlayerUI;
using UnityEngine;
using WheatField;
using WheatField.Wheat;
using WheatField.WheatCube;

namespace Installers
{
    public class GameInstaller : MonoBehaviour
    {
        [SerializeField] private Camera _uiCamera;
        [SerializeField] private UpdateHandler _updateHandler;
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private PlayerUIView _playerUIView;
        [SerializeField] private WheatFieldView _wheatFieldView;
        [SerializeField] private WheatView _wheatView;
        [SerializeField] private WheatCubeView _wheatCubeView;
        [SerializeField] private BarnView _barnView;
        [SerializeField] private InputHandlerView _inputHandlerView;
        [SerializeField] private CoinView _coinView;
        [SerializeField] private CoinUIView _coinUIView;

        private void Awake()
        {
            _updateHandler = Instantiate(_updateHandler);
            
            var playerFactory = new PlayerFactory(_playerView, _updateHandler, _playerUIView, _uiCamera);
            var player = playerFactory.CreateInstance(new Vector3(0f, 0f, -6f));
            var inputFactory = new InputHandlerFactory(_inputHandlerView, _updateHandler, _uiCamera);
            inputFactory.CreateInstance(player);
            
            var wheatCubeFactory = new WheatCubeFactory(_updateHandler, _wheatCubeView);
            var wheatFactory = new WheatFactory(_updateHandler, _wheatView);
            var wheatFieldFactory = new WheatFieldFactory(_updateHandler, _wheatFieldView, wheatFactory, wheatCubeFactory);
            wheatFieldFactory.CreateInstance(new Vector3(4f, 0.01f, -4f));
            wheatFieldFactory.CreateInstance(new Vector3(4f, 0.01f, 3f));
            var barnFactory = new BarnFactory(_updateHandler, _barnView);
            var barnModel = barnFactory.CreateInstance(new Vector3(-4.8f, 0f, -4.25f));
            
            var coinFactory = new CoinFactory(_updateHandler, _coinView);
            var coinUIFactory = new CoinUIFactory(barnModel, coinFactory, _coinUIView, _updateHandler, _uiCamera);
            coinUIFactory.CreateInstance();
        }
    }
}