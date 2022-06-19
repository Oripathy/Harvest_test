using Base;

namespace Barn.SellPoint
{
    public class SellPointPresenter : BasePresenter<BarnModel, ISellPointView>
    {
        public override TPresenter Init<TPresenter>(BarnModel model, ISellPointView view, UpdateHandler updateHandler)
        {
            base.Init<TPresenter>(model, view, updateHandler);

            return this as TPresenter;
        }

        private void OnCubeSold()
        {
            _model.OnCubeSold(_view.Transform.position);
        }
    }
}