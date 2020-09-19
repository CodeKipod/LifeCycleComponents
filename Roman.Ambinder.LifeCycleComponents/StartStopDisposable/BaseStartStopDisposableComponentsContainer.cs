using Roman.Ambinder.DataTypes.OperationResults;
using Roman.Ambinder.LifeCycleComponents.Common.DataTypes;
using Roman.Ambinder.LifeCycleComponents.Common.ExtensionsAndHelpers;
using Roman.Ambinder.LifeCycleComponents.Common.Interfaces;
using System.Collections.Concurrent;
using System.Linq;

namespace Roman.Ambinder.LifeCycleComponents.StartStopDisposable
{
    public abstract class BaseStartStopDisposableComponentsContainer :
        BaseStartStopDisposableComponent
    {
        private readonly ConcurrentDictionary<string, OrderWrapper<IStartStopDisposableComponent>>
            _relatedComponents = new ConcurrentDictionary<string, OrderWrapper<IStartStopDisposableComponent>>();

        public TComponent RegisterForLifeCycleEvents<TComponent>(TComponent component,
            in int startStopOrder = 0)
            where TComponent : IStartStopDisposableComponent
        {
            if (component != null)
                _relatedComponents.AddWithTypeBasedUniqueKey(
                    new OrderWrapper<IStartStopDisposableComponent>(component, startStopOrder));

            return component;
        }

        protected virtual OperationResult BeforeTryStart(params string[] args) => OperationResult.Successful;

        protected sealed override OperationResult OnTryStart(string[] args)
        {
            var opRes = BeforeTryStart(args);
            if (!opRes) return opRes;

            foreach (var component in _relatedComponents.Values.OrderBy(c => c.Order))
            {
                opRes = component.Value.TryStart(args);
                if (!opRes)
                    return opRes;
            }

            return AfterTryStart(args);
        }

        protected virtual OperationResult AfterTryStart(params string[] args) => OperationResult.Successful;

        protected virtual void OnBeforeStop()
        {
        }

        protected override void OnStop()
        {
            OnBeforeStop();

            foreach (var component in _relatedComponents.Values.OrderByDescending(c => c.Order))
            {
                try
                {
                    component.Value.Stop();
                }
                catch
                {
                    // ignored
                }
            }

            OnAfterStop();
        }

        protected virtual void OnAfterStop()
        {
        }

        protected sealed override void OnAfterDispose(bool isDisposing)
        {
            foreach (var component in _relatedComponents.Values.OrderByDescending(c => c.Order))
            {
                try
                {
                    component.Value.Dispose();
                }
                catch
                {
                    // ignored
                }
            }

            OnAdditionalDispose(isDisposing);
        }

        protected virtual void OnAdditionalDispose(bool isDisposing)
        { }
    }
}