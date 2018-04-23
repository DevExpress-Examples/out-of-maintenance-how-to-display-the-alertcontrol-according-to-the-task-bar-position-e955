using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars.Alerter;
using System.Runtime.InteropServices;

namespace SmartAlertControl
{
    public static class AlertHelper
    {
    
        private static AlertFormLocation GetTaskbarLocation()
        {
            IntPtr tbwnd = FindWindow("Shell_TrayWnd", null);
            if (tbwnd == null) return AlertFormLocation.BottomRight;
            AppBarData abd = new AppBarData();
            abd.cbSize = Marshal.SizeOf(typeof(AppBarData));
            abd.hWnd = tbwnd;
            IntPtr ptr = Marshal.AllocHGlobal(abd.cbSize);
            try
            {
                Marshal.StructureToPtr(abd, ptr, false);
                SHAppBarMessage(5, ptr);
                abd = (AppBarData)Marshal.PtrToStructure(ptr, typeof(AppBarData));
            }
            finally { Marshal.FreeHGlobal(ptr); }
            return GetPosition(abd.rc);
        }

        private static AlertFormLocation GetPosition(Rect rect)
        {
            if (rect.top == rect.left && rect.bottom > rect.right) return AlertFormLocation.BottomLeft;
            if (rect.top == rect.left && rect.bottom < rect.right) return AlertFormLocation.TopRight;
            return AlertFormLocation.BottomRight;
        }

        public static void ShowAlertNearTaskBar(AlertControl alertControl, Form parent, AlertInfo info)
        {
            alertControl.FormLocation = GetTaskbarLocation();
            alertControl.Show(parent, info);
        }

        [DllImport("user32.dll")]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("shell32.dll")]
        static extern UIntPtr SHAppBarMessage(uint dwMessage, IntPtr pAppBarData);

        [StructLayout(LayoutKind.Sequential)]
        private struct AppBarData
        {
            public int cbSize;
            public IntPtr hWnd;
            public uint uCallbackMessage;
            public uint uEdge;
            public Rect rc;
            public IntPtr lParam;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Rect
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }
    }
}
