using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roman.Ambinder.LifeCycle.Components.Tests.Disposable.TestComponents;

namespace Roman.Ambinder.LifeCycle.Components.Tests.Disposable
{
    [TestClass]
    public class BaseDisposableComponentTests
    {
        [TestMethod]
        public void ScopedBaseDisposableComponent_EndOfUsingScope_DisposeWasCalled()
        {
            //Arrange
            using var disposeWasCalled = new ManualResetEventSlim();

            //Act
            using (var component = new TestDisposableComponent(disposeWasCalled))
            {

            }

            //Assert
            Assert.IsTrue(disposeWasCalled.Wait(1000));
        }

        [TestMethod]
        public void ScopedBaseDisposableComponent_DisposeInParallel_DisposeWasCalledOnce()
        {
            //Arrange
            using var counter = new CountdownEvent(3);
            var component = new TestDisposableComponent(counter);

            //Act
            Parallel.For(0, 2, i =>
            {
                component.Dispose();
            });

            //Assert
            Assert.IsFalse(counter.IsSet);
            Assert.AreEqual(counter.CurrentCount, counter.InitialCount-1);
        }
    }
}
