using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MyLibs
{
    /// <summary>
    /// Windows Imports
    /// </summary>
    internal static class Win32
    {
        // http://www.pinvoke.net/default.aspx/Enums/SYMBOLIC_LINK_FLAG.html
        [Flags]
        public enum SYMBOLIC_LINK_FLAG
        {
            File = 0,
            Directory = 1,
            AllowUnprivilegedCreate = 2
        }

        // http://pinvoke.net/default.aspx/kernel32.CreateSymbolicLink
        // https://docs.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-createsymboliclinka
        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool CreateSymbolicLink(string lpSymlinkFileName, string lpTargetFileName, SYMBOLIC_LINK_FLAG dwFlags);

        // http://pinvoke.net/default.aspx/kernel32/CreateHardLink.html
        // https://docs.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-createhardlinka
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool CreateHardLink(string lpFileName, string lpExistingFileName, IntPtr lpSecurityAttributes); // lpSecurityAttributes must always be IntPtr.Zero

        // If I ever need hard links to directories (junction points), look at this: https://stackoverflow.com/a/52078181

        public const int STD_OUTPUT_HANDLE = -11;
        public const uint ENABLE_VIRTUAL_TERMINAL_PROCESSING = 4;

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll")]
        public static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

        [DllImport("kernel32.dll")]
        public static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

        [DllImport("kernel32.dll")]
        public static extern uint GetLastError();
    }
}
