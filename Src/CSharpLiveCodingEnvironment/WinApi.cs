using System.Runtime.InteropServices;

namespace CSharpLiveCodingEnvironment
{
    /// <summary>
    ///     WINAPI wrapper class with P/Invoke native functions.
    /// </summary>
    internal static class WinApi
    {
        [DllImport("winmm.dll", EntryPoint = "timeBeginPeriod", SetLastError = true)]
        public static extern uint TimeBeginPeriod(uint uMilliseconds);

        [DllImport("winmm.dll", EntryPoint = "timeEndPeriod", SetLastError = true)]
        public static extern uint TimeEndPeriod(uint uMilliseconds);
    }
}