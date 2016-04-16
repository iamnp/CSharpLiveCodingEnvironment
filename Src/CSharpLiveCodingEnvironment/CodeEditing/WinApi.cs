using System;
using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming

namespace CSharpLiveCodingEnvironment.CodeEditing
{
    /// <summary>
    ///     WINAPI wrapper class with P/Invoke native functions.
    /// </summary>
    internal static class WinApi
    {
        public const int WM_CHAR = 0x102;
        public const int WM_HSCROLL = 0x114;
        public const int WM_VSCROLL = 0x115;
        public const int SB_ENDSCROLL = 0x8;

        [DllImport("User32.dll")]
        public static extern bool CreateCaret(IntPtr hWnd, int hBitmap, int nWidth, int nHeight);

        [DllImport("User32.dll")]
        public static extern bool SetCaretPos(int x, int y);

        [DllImport("User32.dll")]
        public static extern bool DestroyCaret();

        [DllImport("User32.dll")]
        public static extern bool HideCaret(IntPtr hWnd);

        [DllImport("User32.dll")]
        public static extern bool ShowCaret(IntPtr hWnd);
    }
}