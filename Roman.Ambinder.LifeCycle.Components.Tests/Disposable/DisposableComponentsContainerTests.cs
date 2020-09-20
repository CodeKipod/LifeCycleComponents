using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestPlatform.Common.ExtensionFramework.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roman.Ambinder.LifeCycleComponents.Disposable;

namespace Roman.Ambinder.LifeCycle.Components.Tests.Disposable
{
    [TestClass]
    public class DisposableComponentsContainerTests
    {

        [TestMethod]
        public void Test()
        {
            //Arrange 
            using (var container = new DisposableComponentsContainer())
            {

            }

            //Act 

            //Assert
        }
    }

}