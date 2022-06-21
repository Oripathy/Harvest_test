using Base;

namespace Player.WheatDetector
{
    public class WheatDetectorPresenter : BasePresenter<PlayerModel, IWheatDetectorView>
    {
        public override TPresenter Init<TPresenter>(PlayerModel model, IWheatDetectorView view, UpdateHandler updateHandler)
        {
            base.Init<TPresenter>(model, view, updateHandler);
            _view.WheatDetected += OnWheatDetected;
            _view.WheatNotDetected += OnWheatNotDetected;
            return this as TPresenter;
        }

        private void OnWheatDetected()
        {
            _model.OnWheatDetected();
        }

        private void OnWheatNotDetected()
        {
            _model.OnWheatNotDetected();
        }

        public override void Dispose()
        {
            _view.WheatDetected -= OnWheatDetected;
            _view.WheatNotDetected -= OnWheatNotDetected;
            base.Dispose();
        }
    }
}