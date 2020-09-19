using Roman.Ambinder.DataTypes.OperationResults;
using System;

namespace Roman.Ambinder.LifeCycleComponents.Common.Interfaces
{
    public interface IStartStopDisposableComponent : IDisposable
    {
        OperationResult TryStart(params string[] args);

        void Stop();
    }
}