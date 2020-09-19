using Roman.Ambinder.DataTypes.OperationResults;
using Roman.Ambinder.LifeCycleComponents.Common.Interfaces;
using Roman.Ambinder.LifeCycleComponents.Disposable;
using System.Threading;

namespace Roman.Ambinder.LifeCycleComponents.StartStopDisposable
{
    public abstract class BaseStartStopDisposableComponent :
        BaseDisposableComponent,
        IStartStopDisposableComponent
    {
        private int _stopped, _started;

        public OperationResult TryStart(params string[] args)
        {
            if (Interlocked.Exchange(ref _started, 1) == 1)
                return OperationResult.Successful;

            Interlocked.Exchange(ref _stopped, 0);

            return OnTryStart(args);
        }

        public void Stop()
        {
            if (Interlocked.Exchange(ref _stopped, 1) == 1)
                return;

            Interlocked.Exchange(ref _started, 0);

            OnStop();
        }

        protected sealed override void OnDispose(bool isDisposing) => Stop();

        protected abstract OperationResult OnTryStart(string[] args);

        protected abstract void OnStop();
    }
}