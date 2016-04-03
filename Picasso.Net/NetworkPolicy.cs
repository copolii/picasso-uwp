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

namespace Picasso
{
    /// <summary>
    /// Designates the policy to use for network requests.
    /// </summary>
    public class NetworkPolicy
    {
        /// <summary>
        /// Skips checking the disk cache and forces loading through the network.
        /// </summary>
        public static readonly NetworkPolicy NO_CACHE = new NetworkPolicy(1 << 0);
        /// <summary>
        /// Skips storing the result into the disk cache.
        /// </summary>
        public static readonly NetworkPolicy NO_STORE = new NetworkPolicy(1 << 1);
        /// <summary>
        /// Forces the request through the disk cache only, skipping network.
        /// </summary>
        public static readonly NetworkPolicy OFFLINE = new NetworkPolicy(1 << 2);

        private readonly int value;

        private NetworkPolicy(int val)
        {
            value = val;
        }

        public static bool ShouldReadFromDiskCache (int memorypolicy)
        {
            return 0 == (memorypolicy & NO_CACHE.value);
        }

        public static bool ShouldWriteToDiskCache (int memorypolicy)
        {
            return 0 == (memorypolicy & NO_STORE.value);
        }

        public static bool IsOfflineOnly(int memorypolicy)
        {
            return 0 == (memorypolicy & OFFLINE.value);
        }
    }
}
