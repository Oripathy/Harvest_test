using Base;

namespace Barn.SellPoint
{
    public class SellPointPresenter : BasePresenter<BarnModel, ISellPointView>
    {
        public override TPresenter Init<TPresenter>(BarnModel model, ISellPointView view, UpdateHandler updateHandler)
        {
            base.Init<TPresenter>(model, view, updateHandler);
            _view.CubeSold += OnCubeSold;
            return this as TPresenter;
        }

        private void OnCubeSold()
        {
            _model.OnCubeSold(_view.Transform.position);
        }

        public override void Dispose()
        {
            _view.CubeSold -= OnCubeSold;
            base.Dispose();
        }
    }
}