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

        // protected BasePresenter(TModel model, TView view, UpdateHandler updateHandler)
        // {
        //     _model = model;
        //     _view = view;
        //     _updateHandler = updateHandler;
        // }

        public virtual void Init(TModel model, TView view, UpdateHandler updateHandler)
        {
            _model = model;
            _view = view;
            _updateHandler = updateHandler;
            _updateHandler.UpdateTicked += Update;
        }

        private protected virtual void Update()
        {
            
        }

        public virtual void Dispose()
        {
            _updateHandler.UpdateTicked -= Update;
            _updateHandler = default;
            _model = default;
            _view = default;
        }
    }
}