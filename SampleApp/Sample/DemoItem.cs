using Picasso.Sample.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picasso.Sample
{
    internal class DemoItem
    {
        public string Name { get; private set; }
        public Type Page { get; private set; }

        static DemoItem ()
        {
            UseOnce = new DemoItem { Name = "One-off use", Page = typeof (UsePicassoOnce) };
            ContactList = new DemoItem { Name = "Contact List", Page = typeof (ContactList) };
            PhotoSlider = new DemoItem { Name = "Photo Slider", Page = typeof (PhotoSlider) };
            PhotoList = new DemoItem { Name = "Photo List", Page = typeof (PhotoSlider) };
            Values = new DemoItem[] { UseOnce, ContactList, PhotoSlider, PhotoList};
        }

        private DemoItem () { }
        
        public static DemoItem UseOnce { get; private set; }
        public static DemoItem ContactList { get; private set; }
        public static DemoItem PhotoSlider { get; private set; }
        public static DemoItem PhotoList { get; private set; }
        public static DemoItem[] Values { get; private set; }
    }
}
