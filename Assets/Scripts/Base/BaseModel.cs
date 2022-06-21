using System.Threading;

namespace Base
{
    public class BaseModel
    {
        public CancellationTokenSource Source { get; private protected set; }

        public bool IsSourceDisposed { get; private protected set; }

        public virtual void Init()
        {

        }

        public CancellationTokenSource CreateCancellationTokenSource()
        {
            IsSourceDisposed = false;
            return Source = new CancellationTokenSource();
        }

        public void DisposeSource()
        {
            if (IsSourceDisposed) 
                return;
            
            Source?.Dispose();
            Source = default;
            IsSourceDisposed = true;
        }
    }
}