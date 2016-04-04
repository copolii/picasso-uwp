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

            public Uri Uri
            {
                get { return uri; }
                set
                {
                    if (null == value) throw new ArgumentException ("Image URI may not be null");
                    uri = value;
                }
            }

            public string StableKey { get; set; }
            public int? TargetWidth { get; set; }
            public int? TargetHeight { get; set; }
            public bool? CenterCrop { get; set; }
            public bool? CenterInside { get; set; }
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
                TargetWidth = request.targetWidth;
                TargetHeight = request.targetHeight;
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

            public Builder Transform (Transformation transformation)
            {
                transformations.Add (transformation);
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
                    Picasso.Priority p = null == priority ? Picasso.Priority.Normal : (Picasso.Priority)priority;

                    return new Request (uri, StableKey, transformations, TargetWidth, TargetHeight, CenterCrop,
                        CenterInside, OnlyScaleDown, RotationDegrees, RotationPivotX, RotationPivotY, p);
                }
            }
        }
    }
}
