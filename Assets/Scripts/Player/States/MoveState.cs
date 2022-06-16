using Base;
using UnityEngine;

namespace Player.States
{
    public class MoveState : BaseState
    {
        public MoveState(PlayerPresenter presenter, IPlayerView view, PlayerModel model, string animBoolName) : base(
            presenter, view, model, animBoolName)
        {
        }

        public override void Update()
        {
            base.Update();
            _view.Rigidbody.MovePosition(_view.Transform.position +
                                         _model.MoveDirection * _model.MoveSpeed * Time.fixedDeltaTime);
            _presenter.RotateAt(_model.MoveDirection);
        }

        private protected override void SwitchState()
        {
            if (_model.MoveDirection == Vector3.zero)
                _presenter.SwitchState<IdleState>();
        }
    }
}