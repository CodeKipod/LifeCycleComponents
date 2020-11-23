using Roman.Ambinder.LifeCycleComponents.Common.DataTypes;
using Roman.Ambinder.LifeCycleComponents.Common.ExtensionsAndHelpers;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Roman.Ambinder.LifeCycleComponents.Disposable
{
    public class DisposableComponentsContainer : BaseDisposableComponent
    {
        private readonly ConcurrentDictionary<string, OrderWrapper<IDisposable>>
            _relatedComponents = new ConcurrentDictionary<string, OrderWrapper<IDisposable>>();

        public DisposableComponentsContainer() { }

        public DisposableComponentsContainer(params IDisposable[] disposableComponents)
        {
            if (disposableComponents != null && disposableComponents.Length > 0)
            {
                foreach (var component in disposableComponents)
                {
                    RegisterForLifeCycleEvents(component);
                }
            }
        }

        public TComponent RegisterForLifeCycleEvents<TComponent>(TComponent component,
            in int startStopOrder = 0)
            where TComponent : IDisposable
        {
            if (component != null)
                _relatedComponents.AddWithTypeBasedOnUniqueKey(
                    new OrderWrapper<IDisposable>(component, startStopOrder));

            return component;
        }

        protected sealed override void OnDispose(bool isDisposing)
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
        }
    }
}