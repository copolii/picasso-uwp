using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.FileProperties;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

using static Picasso.Utils;

namespace Picasso
{
    public abstract partial class RequestHandler
    {
        public sealed class Result : IDisposable
        {
            public Picasso.LoadedFrom LoadedFrom { get; private set; }
            public BitmapImage Bitmap { get; private set; }
            public Stream Stream { get; private set; }
            public PhotoOrientation ExifOrientation { get; private set; }


            public Result (BitmapImage bitmap, Picasso.LoadedFrom from) 
                : this (AssertNotNull<BitmapImage>(bitmap, "bitmap == null"), null, from, PhotoOrientation.Normal) {}

            public Result (Stream stream, Picasso.LoadedFrom from) 
                : this (null, AssertNotNull<Stream>(stream, "stream == null"), from, PhotoOrientation.Normal) { }

            private Result (BitmapImage bitmap, Stream stream, Picasso.LoadedFrom loadedFrom, PhotoOrientation orientation)
            {
                if (!(bitmap != null ^ stream != null))
                    throw new ArgumentNullException ("bitmap and stream can't both be null");

                Bitmap = bitmap;
                Stream = stream;
                LoadedFrom = AssertNotNull<Picasso.LoadedFrom> (loadedFrom, "loadedFrom == null");
                ExifOrientation = DefaultIfNull<PhotoOrientation> (orientation, PhotoOrientation.Normal);
            }

            public void Dispose ()
            {
                Stream.Dispose ();
            }
        }
    }
}