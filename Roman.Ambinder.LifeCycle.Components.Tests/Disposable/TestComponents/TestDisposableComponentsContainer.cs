using Roman.Ambinder.LifeCycleComponents.Disposable;

namespace Roman.Ambinder.LifeCycle.Components.Tests.Disposable.TestComponents
{
    public class TestDisposableComponentsContainer : DisposableComponentsContainer
    {
        protected override void OnBeforeDispose(bool isDisposing)
        {
            base.OnBeforeDispose(isDisposing);
        }

        protected override void OnAfterDispose(bool isDisposing)
        {
             
        }
    }
}