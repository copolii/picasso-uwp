using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picasso
{
    internal partial class Request
    {
        public sealed class Builder
        {
            private Uri uri;
            private Picasso.Priority? priority;
            private readonly List<Transformation> transformations;
            private uint? targetWidth;
            private uint? targetHeight;
            private bool centerCrop = false;
            private bool centerInside = false;
            
            public string StableKey { get; set; }
            public bool OnlyScaleDown { get; set; }
            public float? RotationDegrees { get; set; }
            public float? RotationPivotX { get; set; }
            public float? RotationPivotY { get; set; }

            public bool HasRotationPivot { get { return null != RotationPivotX && null != RotationPivotY; } }

            public Picasso.Priority? Priority
            {
                get { return priority; }
                set
                {
                    if (null == value) throw new ArgumentException ("Priority cannot be null");
                    if (null != priority) throw new InvalidOperationException ("Priority already set");
                    priority = value;
                }
            }

            public Builder (Uri u)
            {
                uri = u;
                transformations = new List<Transformation> ();
            }

            public Builder (Request request) :this(request.uri)
            {
                StableKey = request.stableKey;
                targetWidth = request.targetWidth;
                targetHeight = request.targetHeight;
                CenterCrop = request.centerCrop;
                CenterInside = request.centerInside;
                RotationDegrees = request.rotationDegrees;
                RotationPivotX = request.rotationPivotX;
                RotationPivotY = request.rotationPivotY;
                OnlyScaleDown = request.onlyScaleDown;

                if (request.trasnsformations != null)
                {
                    foreach (var t in request.trasnsformations)
                        transformations.Add (t);                    
                }

                priority = request.priority;
            }

            internal bool HasImage
            {
                get { return null != uri; }
            }

            internal bool HasSize
            {
                get { return targetWidth != null && targetHeight != null; }
            }

            internal bool HasPriority
            {
                get { return Priority != null; }
            }

            public Uri Uri
            {
                get { return uri; }
                set
                {
                    if (null == value)
                        throw new ArgumentException ("Image URI may not be null");
                    uri = value;
                }
            }

            public uint TargetWidth
            {
                get { return (uint)targetWidth; }
                set { targetWidth = value; }
            }

            public uint TargetHeight
            {
                get { return (uint)targetHeight; }
                set { targetHeight = value; }
            }

            public Builder Resize (uint width, uint height)
            {
                if (width == 0 && height == 0)
                    throw new ArgumentException ("At least one dimension has to be non-zero");

                TargetWidth = width;
                TargetHeight = height;
                return this;
            }

            public Builder ClearResize ()
            {
                targetHeight = null;
                targetWidth = null;
                centerCrop = false;
                centerInside = false;
                return this;
            }

            public bool CenterCrop
            {
                get { return (bool)centerCrop; }
                set
                {
                    if (value && CenterInside)
                        throw new InvalidOperationException ("Center crop can not be used after calling centerInside");

                    centerCrop = value;
                }
            }

            public bool CenterInside
            {
                get { return (bool)centerInside; }
                set
                {
                    if (value && CenterCrop)
                        throw new InvalidOperationException ("Center inside can not be used after calling centerCrop");

                    centerInside = value;
                }
            }

            public Builder Transform (Transformation transformation)
            {
                transformations.Add (transformation);
                return this;
            }

            public Builder Transform (params Transformation[] xformations)
            {
                if (null == xformations)
                    throw new ArgumentNullException ("Transformation list must not be null.");

                foreach (var t in xformations)
                    transformations.Add (t);

                return this;
            }

            public Builder Rotate (float degrees, float pivotx, float pivoty)
            {
                RotationDegrees = degrees;
                RotationPivotX = pivotx;
                RotationPivotY = pivoty;
                return this;
            }

            public Builder ClearRotation ()
            {
                RotationDegrees = null;
                RotationPivotX = null;
                RotationPivotY = null;
                return this;
            }

            public Request Request
            {
                get
                {
                    if (CenterCrop && TargetHeight == 0 && TargetWidth == 0)
                        throw new InvalidOperationException ("Center crop requires calling resize with positive width and height.");

                    if (CenterInside && TargetHeight == 0 && TargetWidth == 0)
                        throw new InvalidOperationException ("Center inside requires calling resize with positive width and height.");

                    Picasso.Priority p = null == priority ? Picasso.Priority.Normal : (Picasso.Priority)priority;

                    return new Request (uri, StableKey, transformations, TargetWidth, TargetHeight, CenterCrop,
                        CenterInside, OnlyScaleDown, RotationDegrees, RotationPivotX, RotationPivotY, p);
                }
            }
        }
    }
}
