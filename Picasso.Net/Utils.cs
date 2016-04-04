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
        internal static void d(string msg)
        {
            logger.WriteEvent(1, LOGTAG + msg);

            // write to console if debugger is attached
            if (!Debugger.IsAttached) return;

            Debug.WriteLine(LOGTAG + msg);
        }
    }

    internal static class Utils
    {
        internal static readonly string THREAD_PREFIX                   = "Picasso-";
        internal static readonly string THREAD_IDLE_NAME                = THREAD_PREFIX + "Idle";
        internal static readonly int    DEFAULT_READ_TIMEOUT_MILLIS     = 20 * 1000; // 20s
        internal static readonly int    DEFAULT_WRITE_TIMEOUT_MILLIS    = 20 * 1000; // 20s
        internal static readonly int    DEFAULT_CONNECT_TIMEOUT_MILLIS  = 15 * 1000; // 15s
        internal static readonly int    THREAD_LEAK_CLEANING_MS         = 1000;
        internal static readonly char   KEY_SEPARATOR                   = '\n';

        private static readonly string  PICASSO_CACHE       = "picasso-cache";
        private static readonly int     KEY_PADDING         = 50; // Determined by exact science.
        private static readonly int     MIN_DISK_CACHE_SIZE = 5 * 1024 * 1024; // 5MB
        private static readonly int     MAX_DISK_CACHE_SIZE = 50 * 1024 * 1024; // 50MB

        /** Thread confined to main thread for key creation. */
        private static readonly StringBuilder MAIN_THREAD_KEY_BUILDER = new StringBuilder();

        /** Logging */
        internal static readonly string OWNER_MAIN = "Main";
        internal static readonly string OWNER_DISPATCHER = "Dispatcher";
        internal static readonly string OWNER_HUNTER = "Hunter";
        internal static readonly string VERB_CREATED = "created";
        internal static readonly string VERB_CHANGED = "changed";
        internal static readonly string VERB_IGNORED = "ignored";
        internal static readonly string VERB_ENQUEUED = "enqueued";
        internal static readonly string VERB_CANCELED = "canceled";
        internal static readonly string VERB_BATCHED = "batched";
        internal static readonly string VERB_RETRYING = "retrying";
        internal static readonly string VERB_EXECUTING = "executing";
        internal static readonly string VERB_DECODED = "decoded";
        internal static readonly string VERB_TRANSFORMED = "transformed";
        internal static readonly string VERB_JOINED = "joined";
        internal static readonly string VERB_REMOVED = "removed";
        internal static readonly string VERB_DELIVERED = "delivered";
        internal static readonly string VERB_REPLAYING = "replaying";
        internal static readonly string VERB_COMPLETED = "completed";
        internal static readonly string VERB_ERRORED = "errored";
        internal static readonly string VERB_PAUSED = "paused";
        internal static readonly string VERB_RESUMED = "resumed";

        /* WebP file header
           0                   1                   2                   3
           0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1
          +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
          |      'R'      |      'I'      |      'F'      |      'F'      |
          +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
          |                           File Size                           |
          +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
          |      'W'      |      'E'      |      'B'      |      'P'      |
          +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
        */
        private const               int     WEBP_FILE_HEADER_SIZE = 12;
        private static  readonly    string  WEBP_FILE_HEADER_RIFF = "RIFF";
        private static  readonly    string  WEBP_FILE_HEADER_WEBP = "WEBP";


        internal static ulong GetBitmapByteSize(BitmapImage bitmap)
        {
            // TODO
            return 0;
        }

        internal static T AssertNotNull<T>(T value, string message)
        {
            if (null == value)
                throw new NullReferenceException(message);

            return value;
        }

        internal static void AssertMain()
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

        internal static void Log(string owner, string verb, string logId, string extras = null)
        {
            global::Picasso.Log.d(string.Format("{0,-11} {1,-12} {2} {3}", owner, verb, logId, null == extras ? string.Empty : extras));
        }

        internal static string CreateKey (Request data, StringBuilder builder = null)
        {
            if (null == builder)
                builder = MAIN_THREAD_KEY_BUILDER;

            // TODO
            return null;
        }
    }
}
