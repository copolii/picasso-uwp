using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picasso.Sample
{
    internal static class ImageManager
    {
        private static Random RND = new Random(DateTime.Now.Millisecond);

        private static readonly Uri BASE_BAD_URI = new Uri ("http://mahram.ca/nonexistent/images");
        public static readonly Uri[] IMAGE_URIS = {
            new Uri ("https://farm4.staticflickr.com/3700/10165441826_8612d74683_k_d.jpg"),
            new Uri ("https://farm8.staticflickr.com/7365/9291665316_2affe13d8b_k_d.jpg"),
            new Uri ("https://farm4.staticflickr.com/3791/9288885061_67c5884fc2_k_d.jpg"),
            new Uri ("https://farm4.staticflickr.com/3751/9291680816_1c636a3c7e_k_d.jpg"),
            new Uri ("https://farm6.staticflickr.com/5502/9291682276_827dd4a483_k_d.jpg"),
            new Uri ("https://farm4.staticflickr.com/3674/9291726824_49b1640378_k_d.jpg"),
            new Uri ("https://farm3.staticflickr.com/2875/9288955003_a28cf0a3cf_k_d.jpg"),
            new Uri ("https://farm4.staticflickr.com/3735/9561989032_5a754f6319_k_d.jpg"),
            new Uri ("https://farm8.staticflickr.com/7194/14096628066_f5dc0da114_k_d.jpg"),
            new Uri ("https://lh6.googleusercontent.com/-vu3-H142_R8/U8Sz81jdWmI/AAAAAAAAm8U/qI3PVfpyEL4/w1302-h868-no/_DSC3244.jpg"),
            new Uri ("https://lh6.googleusercontent.com/-1CNuae2Sjg4/U2yDjEzsI-I/AAAAAAAAkjs/bp66xSVZS2c/w1302-h868-no/_DSC1523.jpg"),
            new Uri ("https://lh6.googleusercontent.com/-pCSNJqj16y4/U2yDCFLzR1I/AAAAAAAAknw/jo07-88TX5g/w579-h868-no/_DSC1503.jpg")
        };

        public static Uri RandomImageUri
        {
            get
            {
                return IMAGE_URIS[RND.Next(0, IMAGE_URIS.Length)];
            }
        }

        public static Uri RandomUri (float errorProbability)
        {
            return (RND.NextDouble () < errorProbability) ? ImageManager.BadUri : ImageManager.RandomImageUri;
        }

        public static Uri BadUri 
        {
            get
            {
                var b = new UriBuilder (BASE_BAD_URI);
                b.Path += RND.Next ();
                return b.Uri;
            }
        }

    }
}
