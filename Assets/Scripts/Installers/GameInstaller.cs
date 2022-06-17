using System.Collections.Generic;
using Base;
using Factories;
using InputHandler;
using Player;
using UnityEngine;
using WheatField;
using WheatField.Wheat;

namespace Installers
{
    public class GameInstaller : MonoBehaviour
    {
        [SerializeField] private UpdateHandler _updateHandler;
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private WheatFieldView _wheatFieldView;
        [SerializeField] private WheatView _wheatView;
        [SerializeField] private InputHandlerView _inputHandlerView;

        private void Awake()
        {
            _updateHandler = Instantiate(_updateHandler);
            var playerFactory = new PlayerFactory(_playerView, _updateHandler);
            var player = playerFactory.CreateInstance(new Vector3(2f, 0f, 0f));
            var inputFactory = new InputHandlerFactory(_inputHandlerView, _updateHandler);
            inputFactory.CreateInstance(player);
            var wheatFactory = new WheatFactory(_updateHandler, _wheatView);
            var wheatFieldFactory = new WheatFieldFactory(_updateHandler, _wheatFieldView, wheatFactory);
            wheatFieldFactory.CreateInstance(new Vector3(3f, 0.01f, -3f));
        }
    }
}