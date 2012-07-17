using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;

namespace RemoteMon
{
    internal static class NativeMethods
    {
        #region AeroGlass
        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmExtendFrameIntoClientArea(
            IntPtr hWnd, MARGINS pMargins);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern bool DwmIsCompositionEnabled();

        [StructLayout(LayoutKind.Sequential)]
        public class MARGINS
        {
            public int cxLeftWidth, cxRightWidth,
                       cyTopHeight, cyBottomHeight;

            public MARGINS(int left, int top, int right, int bottom)
            {
                cxLeftWidth = left; cyTopHeight = top;
                cxRightWidth = right; cyBottomHeight = bottom;
            }
        }
        #endregion
        #region NetApi

        [DllImport("Netapi32", CharSet = CharSet.Auto,
            SetLastError = true),
         SuppressUnmanagedCodeSecurity]
        public static extern int NetServerEnum(
            string serverName, // null
            int dwLevel,
            ref IntPtr pBuf,
            int dwPrefMaxLen,
            out int dwEntriesRead,
            out int dwTotalEntries,
            uint dwServerType,
            string domain, // null
            out int dwResumeHandle
            );

        [DllImport("Netapi32", SetLastError = true),
         SuppressUnmanagedCodeSecurityAttribute]
        public static extern int NetApiBufferFree(
            IntPtr pBuf);

        [StructLayout(LayoutKind.Sequential)]
        public struct _SERVER_INFO_100
        {
            internal int sv100_platform_id;
            [MarshalAs(UnmanagedType.LPWStr)]
            internal string sv100_name;
        }
        #endregion
    }
}
