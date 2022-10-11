using System;
using System.Diagnostics;
//using Essentials.Packages.LogSystem;

namespace Essentials.Extensions
{

    public static class DebugExtensions 
    {
        //public static Log Log = new Log(typeof(DebugExtensions).Name, DebugLevels.Debug);

        public static void Watch(this Action testCase, string message, Action preProcess = null)
        {
            preProcess?.Invoke();

            Stopwatch watch = new Stopwatch();
            watch.Start();
            testCase?.Invoke();
            watch.Stop();
            //Log.Debug($"{message}: {watch.Elapsed.TotalMilliseconds.ToString("N6")} milliseconds", null, LogTypes.Performance);
        }

    }

}