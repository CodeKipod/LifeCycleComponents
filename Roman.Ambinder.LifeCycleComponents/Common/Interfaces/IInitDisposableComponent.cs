using Roman.Ambinder.DataTypes.OperationResults;
using System;

namespace Roman.Ambinder.LifeCycleComponents.Common.Interfaces
{
    public interface IInitDisposableComponent : IDisposable
    {
        OperationResult TryInit(params string[] args);
    }
}