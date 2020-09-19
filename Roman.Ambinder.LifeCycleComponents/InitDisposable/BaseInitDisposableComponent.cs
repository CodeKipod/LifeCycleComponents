using Roman.Ambinder.DataTypes.OperationResults;
using Roman.Ambinder.LifeCycleComponents.Common.Interfaces;
using Roman.Ambinder.LifeCycleComponents.Disposable;
using System.Threading;

namespace Roman.Ambinder.LifeCycleComponents.InitDisposable
{
    public abstract class BaseInitDisposableComponent :
        BaseDisposableComponent,
        IInitDisposableComponent
    {
        private int _wasInitialized = 0;

        public OperationResult TryInit(params string[] args)
        {
            return Interlocked.Exchange(ref _wasInitialized, 1) == 1 ?
                OperationResult.Successful :
                OnTryInit(args);
        }

        protected abstract OperationResult OnTryInit(string[] args);
    }
}