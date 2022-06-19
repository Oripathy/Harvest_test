using System.Collections.Generic;
using Barn;
using Base;
using Factories;
using InputHandler;
using Player;
using Player.PlayerUI;
using UnityEngine;
using WheatField;
using WheatField.Wheat;
using WheatField.WheatCube;

namespace Installers
{
    public class GameInstaller : MonoBehaviour
    {
        [SerializeField] private UpdateHandler _updateHandler;
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private PlayerUIView _playerUIView;
        [SerializeField] private WheatFieldView _wheatFieldView;
        [SerializeField] private WheatView _wheatView;
        [SerializeField] private WheatCubeView _wheatCubeView;
        [SerializeField] private BarnView _barnView;
        [SerializeField] private InputHandlerView _inputHandlerView;

        private void Awake()
        {
            _updateHandler = Instantiate(_updateHandler);
            var playerFactory = new PlayerFactory(_playerView, _updateHandler, _playerUIView);
            var player = playerFactory.CreateInstance(new Vector3(0f, 0f, -6f));
            var inputFactory = new InputHandlerFactory(_inputHandlerView, _updateHandler);
            inputFactory.CreateInstance(player);
            var wheatCubeFactory = new WheatCubeFactory(_wheatCubeView, _updateHandler);
            var wheatFactory = new WheatFactory(_updateHandler, _wheatView, wheatCubeFactory);
            var wheatFieldFactory = new WheatFieldFactory(_updateHandler, _wheatFieldView, wheatFactory, wheatCubeFactory);
            wheatFieldFactory.CreateInstance(new Vector3(4f, 0.01f, -4f));
            wheatFieldFactory.CreateInstance(new Vector3(4f, 0.01f, 3f));
            var barnFactory = new BarnFactory(_barnView, _updateHandler);
            barnFactory.CreateInstance(new Vector3(-5f, 0f, -4.45f));
        }
    }
}