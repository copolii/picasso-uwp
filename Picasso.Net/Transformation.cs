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
    /// <summary>
    /// Image transformation.
    /// </summary>
    public interface Transformation
    {
        /// <summary>
        /// Transform the source bitmap into a new bitmap. You may return the original
        /// if no transformation is required.
        /// </summary>
        /// <param name="source">The source bitmap</param>
        /// <returns>Transformed bitmap</returns>
        BitmapImage Transform (BitmapImage source);

        /// <summary>
        /// Returns a unique key for the transformation, used for caching purposes. If the transformation
        /// has parameters (e.g.size, scale factor, etc) then these should be part of the key.
        /// </summary>
        /// <returns>Unique key</returns>
        string Key ();
    }
}
