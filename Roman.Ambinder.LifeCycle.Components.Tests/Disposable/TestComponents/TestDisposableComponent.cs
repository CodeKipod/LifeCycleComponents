using System.Threading;
using Roman.Ambinder.LifeCycleComponents.Disposable;

namespace Roman.Ambinder.LifeCycle.Components.Tests.Disposable.TestComponents
{
    public class TestDisposableComponent : BaseDisposableComponent
    {
        private ManualResetEventSlim _disposed;
        private CountdownEvent _counter;

        public TestDisposableComponent(ManualResetEventSlim disposed)
        {
            _disposed = disposed;
        }

        public TestDisposableComponent(CountdownEvent counter)
        {
            _counter = counter;
        }

        protected override void OnDispose(bool isDisposing)
        {
            _disposed?.Set();
            _counter?.Signal();
        }
    }
}