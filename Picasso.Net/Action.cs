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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Picasso
{
    internal class RequestWeakReference<M> where M: class
    {
        private readonly WeakReference reference;
        private readonly Action action;

        public RequestWeakReference (Action a, M referent)
        {
            reference = new WeakReference (referent);
            action = a;
        }

        internal M Target
        {
            get
            {
                return reference.IsAlive ? reference.Target as M: null;
            }
        }

        internal void Clear ()
        {
            reference.Target = null;
        }
    }

    internal abstract class Action<T> where T :class
    {
        internal Picasso Picasso { get; private set; }

        internal Request Request { get; private set; }

        internal bool WillReplay { get; private set; }

        internal bool IsCancelled { get; set; }

        internal string Key { get; private set; }

        internal MemoryPolicy MemoryPolicy { get; private set; }

        internal NetworkPolicy NetworkPolicy { get; private set; }

        internal object Tag { get; private set; }

        internal T Target
        {
            get
            {
                return null == target 
                    ? null 
                    : (target.IsAlive 
                        ? target.Target as T 
                        : null);
            }
        }

        internal Picasso.Priority Priority
        {
            get { return Request.priority; }
        }

        private readonly WeakReference target;
        private readonly bool noFade;
        private readonly Uri errorResource;
        private readonly BitmapImage errorImage;

        internal Action (Picasso picasso, T _target, Request request, MemoryPolicy mempolicy, NetworkPolicy netPolicy, 
            Uri _errorResource, BitmapImage _errorImage, string key, object tag, bool nofade)
        {
            Picasso = picasso;
            Request = request;
            target = null == _target ? null : new WeakReference (_target);
            MemoryPolicy = mempolicy;
            NetworkPolicy = netPolicy;
            noFade = nofade;
            errorResource = _errorResource;
            errorImage = _errorImage;
            Key = key;
            Tag = null == tag ? this : tag;
        }

        internal abstract void Complete (BitmapImage result, Picasso.LoadedFrom from);
        internal abstract void Error ();
    }
}
