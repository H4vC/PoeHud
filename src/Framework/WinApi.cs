using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using PoeHUD.Framework.Enums;
using PoeHUD.Framework.InputHooks;

namespace PoeHUD.Framework
{
    public static class WinApi
    {
        #region Methods

        public static void EnableTransparent(IntPtr handle, Rectangle size)
        {
            int windowLong = GetWindowLong(handle, GWL_EXSTYLE) | WS_EX_LAYERED | WS_EX_TRANSPARENT;
            SetWindowLong(handle, GWL_EXSTYLE, new IntPtr(windowLong));
            SetLayeredWindowAttributes(handle, 0, 255, LWA_ALPHA);
            Margins margins = Margins.FromRectangle(size);
            DwmExtendFrameIntoClientArea(handle, ref margins);
        }

        public static Rectangle GetClientRectangle(IntPtr handle)
        {
            Rect rect;
            Point point;
            GetClientRect(handle, out rect);
            ClientToScreen(handle, out point);
            return rect.ToRectangle(point);
        }

        public static bool IsForegroundWindow(IntPtr handle)
        {
            return GetForegroundWindow() == handle;
        }

        public static bool IsKeyDown(Keys key)
        {
            return (GetAsyncKeyState(key) & 0x8000) != 0;
        }

        public static IntPtr OpenProcess(Process process, ProcessAccessFlags flags)
        {
            return OpenProcess(flags, false, process.Id);
        }

        public static bool ReadProcessMemory(IntPtr handle, IntPtr baseAddress, byte[] buffer)
        {
            IntPtr bytesRead;
            return ReadProcessMemory(handle, baseAddress, buffer, buffer.Length, out bytesRead);
        }


        #endregion

        #region Constants

        private const int GWL_EXSTYLE = -20;

        private const int WS_EX_LAYERED = 0x80000;

        private const int WS_EX_TRANSPARENT = 0x20;

        private const int LWA_ALPHA = 0x2;

        #endregion

        #region Imports

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode, int wParam, IntPtr lParam);

        [DllImport("ComDlg32.dll", CharSet = CharSet.Unicode)]
        public static extern bool ChooseColor(ref ChooseColor chooseColor);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern bool MoveWindow(IntPtr hWnd, int x, int y, int width, int height, bool repaint);

        [DllImport("user32.dll")]
        public static extern bool ScreenToClient(IntPtr hWnd, ref Point lpPoint);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndParent);

        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize", ExactSpelling = true)]
        public static extern int SetProcessWorkingSetSize(IntPtr process, int minimumWorkingSetSize, int maximumWorkingSetSize);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, int dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern int UnhookWindowsHookEx(int idHook);

        [DllImport("user32.dll")]
        private static extern bool ClientToScreen(IntPtr hWnd, out Point lpPoint);

        [DllImport("dwmapi.dll")]
        private static extern IntPtr DwmExtendFrameIntoClientArea(IntPtr hWnd, ref Margins pMarInset);

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(Keys vKey);

        [DllImport("user32.dll")]
        private static extern bool GetClientRect(IntPtr hWnd, out Rect lpRect);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(ProcessAccessFlags processAccess, bool bInheritHandle, int processId);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool ReadProcessMemory(IntPtr hWnd, IntPtr baseAddr, byte[] buffer, int size, out IntPtr bytesRead);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetLayeredWindowAttributes(IntPtr hWnd, uint crKey, byte bAlpha, uint dwFlags);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        #endregion

        #region Structures

        [StructLayout(LayoutKind.Sequential)]
        private struct Margins
        {
            private int left, right, top, bottom;

            public static Margins FromRectangle(Rectangle rectangle)
            {
                var margins = new Margins
                {
                    left = rectangle.Left,
                    right = rectangle.Right,
                    top = rectangle.Top,
                    bottom = rectangle.Bottom
                };
                return margins;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Rect
        {
            private readonly int left, top, right, bottom;

            public Rectangle ToRectangle(Point point)
            {
                return new Rectangle(point.X, point.Y, right - left, bottom - top);
            }
        }

        #endregion
    }
}