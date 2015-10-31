using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using PoeHUD.Framework.Helpers;
using PoeHUD.Framework.InputHooks.Structures;

namespace PoeHUD.Framework.InputHooks
{
    public static class MouseHook
    {
        private const int WH_MOUSE_LL = 14;

        private const int WM_MOUSEMOVE = 0x200;

        private const int WM_LBUTTONDOWN = 0x201;

        private const int WM_RBUTTONDOWN = 0x204;

        private const int WM_LBUTTONUP = 0x202;

        private const int WM_RBUTTONUP = 0x205;

        private const int WM_MOUSEWHEEL = 0x020A;

        private static HookProc hookProc;

        private static int handle;

        private static int MouseHookProc(int nCode, int wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                var mouseHookStruct = (MouseLLHookStruct)Marshal.PtrToStructure(lParam, typeof(MouseLLHookStruct));
                Point position = mouseHookStruct.Point;

                MouseInfo mouseInfo = null;
                switch (wParam)
                {
                    case WM_LBUTTONDOWN:
                        mouseInfo = new MouseInfo(MouseButtons.Left, position, 0);
                        mouseDown.SafeInvoke(mouseInfo);
                        break;
                    case WM_LBUTTONUP:
                        mouseInfo = new MouseInfo(MouseButtons.Left, position, 0);
                        mouseUp.SafeInvoke(mouseInfo);
                        break;
                    case WM_RBUTTONDOWN:
                        mouseInfo = new MouseInfo(MouseButtons.Right, position, 0);
                        mouseDown.SafeInvoke(mouseInfo);
                        break;
                    case WM_RBUTTONUP:
                        mouseInfo = new MouseInfo(MouseButtons.Right, position, 0);
                        mouseUp.SafeInvoke(mouseInfo);
                        break;
                    case WM_MOUSEWHEEL:
                        int delta = (mouseHookStruct.MouseData >> 16) & 0xffff;
                        mouseInfo = new MouseInfo(MouseButtons.None, position, delta);
                        mouseWheel.SafeInvoke(mouseInfo);
                        break;
                    case WM_MOUSEMOVE:
                        mouseInfo = new MouseInfo(MouseButtons.None, position, 0);
                        mouseMove.SafeInvoke(mouseInfo);
                        break;
                }

                if (mouseInfo != null && mouseInfo.Handled)
                {
                    return -1;
                }
            }

            return WinApi.CallNextHookEx(handle, nCode, wParam, lParam);
        }

        private static void Subscribe()
        {
            if (handle == 0)
            {
                hookProc = MouseHookProc;
                handle = WinApi.SetWindowsHookEx(WH_MOUSE_LL, hookProc, IntPtr.Zero, 0);
                if (handle == 0)
                {
                    int errorCode = Marshal.GetLastWin32Error();
                    throw new Win32Exception(errorCode);
                }
            }
        }

        private static void TryUnsubscribe()
        {
            if (handle != 0 && mouseDown == null && mouseUp == null && mouseMove == null && mouseWheel == null)
            {
                int result = WinApi.UnhookWindowsHookEx(handle);
                handle = 0;
                hookProc = null;
                if (result == 0)
                {
                    int errorCode = Marshal.GetLastWin32Error();
                    throw new Win32Exception(errorCode);
                }
            }
        }

        #region Events

        private static event Action<MouseInfo> mouseMove;

        public static event Action<MouseInfo> MouseMove
        {
            add
            {
                Subscribe();
                mouseMove += value;
            }

            remove
            {
                mouseMove -= value;
                TryUnsubscribe();
            }
        }

        private static event Action<MouseInfo> mouseDown;

        public static event Action<MouseInfo> MouseDown
        {
            add
            {
                Subscribe();
                mouseDown += value;
            }
            remove
            {
                mouseDown -= value;
                TryUnsubscribe();
            }
        }

        private static event Action<MouseInfo> mouseUp;

        public static event Action<MouseInfo> MouseUp
        {
            add
            {
                Subscribe();
                mouseUp += value;
            }
            remove
            {
                mouseUp -= value;
                TryUnsubscribe();
            }
        }

        private static event Action<MouseInfo> mouseWheel;

        public static event Action<MouseInfo> MouseWheel
        {
            add
            {
                Subscribe();
                mouseWheel += value;
            }
            remove
            {
                mouseWheel -= value;
                TryUnsubscribe();
            }
        }

        #endregion
    }
}