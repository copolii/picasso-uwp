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
    /// Designates the policy to use when dealing with memory cache.
    /// </summary>
    public class MemoryPolicy
    {
        /// <summary>
        /// Skips memory cache lookup when processing a request.
        /// </summary>
        public static readonly MemoryPolicy NO_CACHE = new MemoryPolicy(1 << 0);
        /// <summary>
        /// Skips storing the final result into memory cache. Useful for one-off requests to avoid evicting other bitmaps from the cache.
        /// </summary>
        public static readonly MemoryPolicy NO_STORE = new MemoryPolicy(1 << 1);

        private readonly int value;

        private MemoryPolicy(int val)
        {
            value = val;
        }

        public static bool ShouldReadFromMemoryCache(int memorypolicy)
        {
            return 0 == (memorypolicy & NO_CACHE.value);
        }

        public static bool ShouldWriteToMemoryCache(int memorypolicy)
        {
            return 0 == (memorypolicy & NO_STORE.value);
        }
    }
}
