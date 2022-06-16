using Base;
using Factories;
using InputHandler;
using Player;
using UnityEngine;

namespace Installers
{
    public class GameInstaller : MonoBehaviour
    {
        [SerializeField] private UpdateHandler _updateHandler;
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private InputHandlerView _inputHandlerView;

        private void Awake()
        {
            _updateHandler = Instantiate(_updateHandler);
            var playerFactory = new PlayerFactory(_playerView, _updateHandler);
            var player = playerFactory.CreateInstance(new Vector3(2f, 0f, 0f));
            var inputFactory = new InputHandlerFactory(_inputHandlerView, _updateHandler);
            inputFactory.CreateInstance(player);
        }
    }
}