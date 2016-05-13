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
using Picasso.Sample;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SampleApp
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private DemoItem[] Items = DemoItem.Values;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void ListView_SelectionChanged (object sender, SelectionChangedEventArgs e)
        {
            var item = (sender as ListView).SelectedItem as DemoItem;

            if (null == item)
            {
                Debug.WriteLine ("Nothing selected.");
                return;
            }

            Navigate (item);
        }

        private void Navigate (DemoItem item)
        {
            Frame.Navigate (item.Page);
        }
    }
}
