using Roman.Ambinder.DataTypes.OperationResults;
using Roman.Ambinder.LifeCycleComponents.Common.DataTypes;
using Roman.Ambinder.LifeCycleComponents.Common.ExtensionsAndHelpers;
using Roman.Ambinder.LifeCycleComponents.Common.Interfaces;
using System.Collections.Concurrent;
using System.Linq;

namespace Roman.Ambinder.LifeCycleComponents.InitDisposable
{
    public abstract class BaseInitDisposableComponentsContainer :
        BaseInitDisposableComponent
    {
        private readonly ConcurrentDictionary<string, OrderWrapper<IInitDisposableComponent>>
            _relatedComponents = new ConcurrentDictionary<string, OrderWrapper<IInitDisposableComponent>>();

        public TComponent RegisterForLifeCycleEvents<TComponent>(
            TComponent component,
            in int startStopOrder = 0)
            where TComponent : IInitDisposableComponent
        {
            if (component != null)
                _relatedComponents.AddWithTypeBasedUniqueKey(
                    new OrderWrapper<IInitDisposableComponent>(component, startStopOrder));

            return component;
        }

        protected virtual OperationResult OnBeforeTryInit(string[] args) => OperationResult.Successful;

        protected sealed override OperationResult OnTryInit(string[] args)
        {
            var opRes = OnBeforeTryInit(args);
            if (!opRes) return opRes;

            foreach (var component in _relatedComponents.Values.OrderBy(c => c.Order))
            {
                opRes = component.Value.TryInit(args);
                if (!opRes) return opRes;
            }

            return OnAfterTryInit(args);
        }

        protected virtual OperationResult OnAfterTryInit(string[] args)
            => OperationResult.Successful;
    }
}