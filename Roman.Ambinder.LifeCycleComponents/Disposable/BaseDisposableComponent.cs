using System;
using System.Threading;

namespace Roman.Ambinder.LifeCycleComponents.Disposable
{
    public abstract class BaseDisposableComponent : IDisposable
    {
        private int _wasDisposed;

        public void Dispose() => OnTryDispose(isDisposing: true);

        ~BaseDisposableComponent() => OnTryDispose(isDisposing: false);

        private void OnTryDispose(bool isDisposing)
        {
            if (Interlocked.Exchange(ref _wasDisposed, 1) == 1)
                return;

            try
            {
                OnBeforeDispose(isDisposing);
                OnDispose(isDisposing);
                OnAfterDispose(isDisposing);

                if (isDisposing)
                    GC.SuppressFinalize(this);
            }
            catch
            {
                // ignored
            }
        }

        protected virtual void OnBeforeDispose(bool isDisposing)
        {
        }

        protected abstract void OnDispose(bool isDisposing);

        protected virtual void OnAfterDispose(bool isDisposing)
        {
        }
    }
}