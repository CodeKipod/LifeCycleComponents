﻿using Roman.Ambinder.LifeCycleComponents.Common.DataTypes;
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

        public TComponent RegisterForLifeCycleEvents<TComponent>(TComponent component,
            in int startStopOrder = 0)
            where TComponent : IDisposable
        {
            if (component != null)
                _relatedComponents.AddWithTypeBasedUniqueKey(
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