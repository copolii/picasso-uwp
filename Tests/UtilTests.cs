#region Apache 2.0 License
/****************************************************************************
* Copyright ©2016 Mahram Z. Foadi                                           *
*                                                                           *
*  Licensed under the Apache License, Version 2.0 (the "License");          *
*  you may not use this file except in compliance with the License.         *
*  You may obtain a copy of the License at                                  *
*                                                                           *
*      http://www.apache.org/licenses/LICENSE-2.0                           *
*                                                                           *
*  Unless required by applicable law or agreed to in writing, software      *
*  distributed under the License is distributed on an "AS IS" BASIS,        *
*  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. *
*  See the License for the specific language governing permissions and      *
*  limitations under the License.                                           *
****************************************************************************/
#endregion
using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Windows.UI.Xaml;
using Windows.ApplicationModel.Core;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Picasso
{
    [TestClass]
    public class UtilsTests
    {
        [TestMethod]
        public void TestUIThreadDetectionFromMain()
        {
            var task = CheckUIThreadFromMain();
            task.Wait();
            Assert.IsTrue(task.Result);
        }

        private async Task<bool> CheckUIThreadFromMain ()
        {
            var tcr = new TaskCompletionSource<bool>();

            await CoreApplication.MainView.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
                tcr.SetResult(Picasso.Utils.IsUIThread());
            });

            return tcr.Task.Result;
        }

        [TestMethod]
        public void TestUIThreadDetectionFromBackground()
        {
            var task = CheckUIThreadFromTask();
            task.Wait();
            Assert.IsFalse(task.Result);
        }

        private async Task<bool> CheckUIThreadFromTask()
        {
            var tcr = new TaskCompletionSource<bool>();

            await Task.Run(() => {
                tcr.SetResult(Picasso.Utils.IsUIThread());
            });

            return tcr.Task.Result;
        }
    }
}
