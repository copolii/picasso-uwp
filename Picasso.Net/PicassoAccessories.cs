using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picasso
{
    public partial class Picasso
    {
        public enum Priority
        {
            Low, Normal, High
        }

        public enum LoadedFrom
        {
            Memory, Disk, Network
        }
    }
}
