using System;
using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming

namespace CSharpLiveCodingEnvironment
{
    /// <summary>
    ///     WINAPI wrapper class with P/Invoke native functions.
    /// </summary>
    internal static class WinApi
    {
        public const int GWL_STYLE = -16;
        public const int WS_DISABLED = 0x08000000;

        [DllImport("winmm.dll", EntryPoint = "timeBeginPeriod", SetLastError = true)]
        public static extern uint TimeBeginPeriod(uint uMilliseconds);

        [DllImport("winmm.dll", EntryPoint = "timeEndPeriod", SetLastError = true)]
        public static extern uint TimeEndPeriod(uint uMilliseconds);

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
    }
}