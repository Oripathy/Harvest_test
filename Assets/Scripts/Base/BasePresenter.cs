using System;

namespace Base
{
    public abstract class BasePresenter<TModel, TView> : IDisposable
        where TModel : BaseModel
        where TView : IBaseView
    {
        private protected TModel _model;
        private protected TView _view;
        private protected UpdateHandler _updateHandler;

        public virtual TPresenter Init<TPresenter>(TModel model, TView view, UpdateHandler updateHandler)
            where TPresenter: BasePresenter<TModel, TView>
        {
            _model = model;
            _view = view;
            _view.ObjectDestroyed += Dispose;
            _updateHandler = updateHandler;
            _updateHandler.UpdateTicked += Update;
            return this as TPresenter;
        }

        private protected virtual void Update()
        {
            
        }

        public virtual void Dispose()
        {
            _model.Source?.Cancel();
            _model.DisposeSource();
            _updateHandler.UpdateTicked -= Update;
            // _updateHandler = default;
            // _model = default;
            // _view = default;
        }
    }
}