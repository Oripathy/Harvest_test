using Player;

namespace Base
{
    public abstract class BaseState
    {
        private protected PlayerPresenter _presenter;
        private protected IPlayerView _view;
        private protected PlayerModel _model;
        private protected string _animBoolName;

        protected BaseState(PlayerPresenter presenter, IPlayerView view, PlayerModel model, string animBoolName)
        {
            _presenter = presenter;
            _view = view;
            _model = model;
            _animBoolName = animBoolName;
        }

        public virtual void OnEnter()
        {
            
        }

        public virtual void Update()
        {
            SwitchState();    
        }

        public virtual void OnExit()
        {
            
        }

        private protected abstract void SwitchState();
    }
}