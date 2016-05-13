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
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Picasso
{
    /// <summary>
    /// Immutable data about an image and the transformations that will be applied to it.
    /// </summary>
    internal partial class Request
    {
        private const long TOO_LONG_LOG = 5000; // Millis

        /// <summary>
        /// A unique ID for the request.
        /// </summary>
        internal int Id { get; set; }
        /// <summary>
        ///  The time that the request was first submitted (in millis).
        /// </summary>
        internal long StartTime { get; set; }
        /// <summary>
        /// The <see cref="NetworkPolicy"/> to use for this request.
        /// </summary>
        internal NetworkPolicy NetworkPolicy { get; set; }

        /// <summary>
        /// The image URI.
        /// </summary>
        public readonly Uri uri;
        /// <summary>
        /// Optional stable key for this request to be used instead of the URI or resource ID when
        /// caching. Two requests with the same value are considered to be for the same resource.
        /// </summary>
        public readonly string stableKey;
        /// <summary>
        /// List of custom transformations to be applied after the built-in transformations.
        /// </summary>
        public readonly ReadOnlyCollection<Transformation> trasnsformations;
        /// <summary>
        /// Target image width for resizing.
        /// </summary>
        public readonly uint? targetWidth;
        /// <summary>
        /// Target image height for resizing.
        /// </summary>
        public readonly uint? targetHeight;
        /// <summary>
        /// True if the final image should use the 'centerCrop' scale technique.
        /// This is mutually exclusive with <see cref="centerInside"/>.
        /// </summary>
        public readonly bool centerCrop;
        /// <summary>
        /// True if the final image should use the 'centerInside' scale technique.
        /// This is mutually exclusive with <see cref="centerCrop"/>.
        /// </summary>
        public readonly bool centerInside;
        /// <summary>
        /// True if the image is not to be magnified
        /// </summary>
        public readonly bool onlyScaleDown;
        /// <summary>
        /// Amount to rotate the image in degrees.
        /// </summary>
        public readonly float? rotationDegrees;
        /// <summary>
        /// Rotation pivot on the X axis.
        /// </summary>
        public readonly float? rotationPivotX;
        /// <summary>
        /// Rotation pivot on the Y axis.
        /// </summary>
        public readonly float? rotationPivotY;
        /// <summary>
        /// Whether or not <see cref="rotationPivotX"/> and <see cref="rotationPivotY"/> are set.
        /// </summary>
        public bool HasRotationPivot { get { return null != rotationPivotX && null != rotationPivotY; } }
        /// <summary>
        /// The priority of this request.
        /// </summary>
        public readonly Picasso.Priority priority;

        private Request (Uri _uri, string _stableKey, List<Transformation> _transformations, uint? width, uint? height, bool? ctrCrop, bool? ctrInside,
            bool scaledown, float? rotdegrees, float? rotpivotx, float? rotpivoty, Picasso.Priority p)
        {
            uri = _uri;
            stableKey = _stableKey;
            trasnsformations = null == _transformations || _transformations.Count == 0 ? null : _transformations.AsReadOnly ();
            targetWidth = width;
            targetHeight = height;
            centerCrop = (bool)ctrCrop;
            centerInside = (bool)ctrInside;
            onlyScaleDown = scaledown;
            rotationDegrees = rotdegrees;
            rotationPivotX = rotpivotx;
            rotationPivotY = rotpivoty;
            priority = p;
        }

        public override string ToString ()
        {
            var sb = new StringBuilder ("Request{");

            sb.Append (uri.ToString());

            if (null != trasnsformations)
                foreach (var t in trasnsformations)
                    sb.Append ($" {t.Key ()}");

            if (null != stableKey)
                sb.Append ($" stableKey({stableKey})");

            if (targetHeight > 0 || targetWidth > 0)
                sb.AppendFormat ($" resize ({targetWidth},{targetHeight})");

            if ((bool)centerCrop)
                sb.Append (" centerCrop");

            if ((bool)centerInside)
                sb.Append (" centerInside");

            if (rotationDegrees > 0)
            {
                sb.Append ($" rotation({rotationDegrees}");

                if (HasRotationPivot)
                {
                    sb.Append ($" @ {rotationPivotX},{rotationPivotY}");
                }

                sb.Append (')');
            }

            return sb.Append('}').ToString ();
        }

        internal string LogId
        {
            get
            {
                var delta = TimeSpan.FromMilliseconds (System.DateTime.Now.Millisecond - StartTime);

                return delta.Milliseconds > TOO_LONG_LOG
                    ? $"{PlainId}+{delta.Seconds}s"
                    : $"{PlainId}+{delta.Milliseconds}ms";
            }
        }

        internal string PlainId
        {
            get { return $"[R{Id}]"; }
        }

        internal string Name
        {
            get
            {
                return uri.AbsolutePath;
            }
        }

        public bool HasSize { get { return null != targetWidth || null != targetHeight; } }

        internal bool NeedsTransformation
        {
            get { return NeedsMatrixTransformation || HasCustomTransformations; }
        }

        internal bool NeedsMatrixTransformation
        {
            get { return HasSize || null != rotationDegrees; }
        }
        
        internal bool HasCustomTransformations
        {
            get { return null != trasnsformations && trasnsformations.Count > 0; }
        }

        public Builder BuildUpon ()
        {
            return new Builder (this);
        }
    }
}
