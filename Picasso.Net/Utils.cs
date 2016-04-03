using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Graphics.Imaging;
using Windows.UI.Core;
using Windows.UI.Xaml.Media.Imaging;

namespace Picasso
{
    internal sealed class Log : EventSource
    {
        private static readonly string LOGTAG = "[Picasso] ";
        private static readonly Log logger = new Log();

        [Event(1, Level = EventLevel.Verbose)]
        internal static void d (string msg)
        {
            logger.WriteEvent(1, LOGTAG + msg);

            // write to console if debugger is attached
            if (!Debugger.IsAttached) return;

            Debug.WriteLine(LOGTAG + msg);
        }
    }

    internal static class Utils
    {
        internal static ulong GetBitmapByteSize (BitmapImage bitmap)
        {
            // TODO
            return 0;
        }

        internal static T AssertNotNull<T> (T value, string message)
        {
            if (null == value)
                throw new NullReferenceException(message);

            return value;
        }

        internal static void AssertMain ()
        {
            if (!IsMain)
                throw new InvalidOperationException("Method call should happen from the main thread.");
        }

        internal static void AssertNotMain()
        {
            if (IsMain)
                throw new InvalidOperationException("Method call should not happen from the main thread.");
        }

        internal static bool IsMain 
        {
            get { return CoreApplication.MainView.Dispatcher.HasThreadAccess; }
        }

        /* TODO
        
        internal static string GetLogIdsForHunter(BitmapHunter hunter, string prefix = null)
        {
            var builder = null == prefix ? new StringBuilder() : new StringBuilder(prefix);

            var action = hunter.Action;

            if (null != action)
            {
                builder.Append(action.Request.LogId);
            }

            var actions = hunter.Actions;
            
            if (null != actions)
            {
                var idx = builder.Length;
                var separator = ", ";

                foreach (var a in actions)
                {
                    builder.Append(separator).Append(a.Request.LogId);
                }

                if (null == action)
                    builder.Remove(idx, separator.Length);
            }

            return builder.ToString();
        }
        
        */

        internal static void Log (string owner, string verb, string logId, string extras=null)
        {
            Picasso.Log.d(string.Format("{0,-11} {1,-12} {2} {3}", owner, verb, logId, null == extras ? string.Empty : extras));
        }
    }
}
