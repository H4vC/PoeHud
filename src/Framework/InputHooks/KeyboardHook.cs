using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using PoeHUD.Framework.Helpers;
using PoeHUD.Framework.InputHooks.Structures;

namespace PoeHUD.Framework.InputHooks
{
    public static class KeyboardHook
    {
        private const int WH_KEYBOARD_LL = 13;

        private const int WM_KEYDOWN = 0x100;

        private const int WM_KEYUP = 0x101;

        private const int WM_SYSKEYDOWN = 0x104;

        private const int WM_SYSKEYUP = 0x105;

        private static HookProc hookProc;

        private static int handle;

        private static bool control, alt, shift;

        private static KeyInfo GetKeys(Keys keyData, bool specialValue)
        {
            switch (keyData)
            {
                case Keys.RControlKey:
                case Keys.LControlKey:
                    control = specialValue;
                    break;
                case Keys.RMenu:
                case Keys.LMenu:
                    alt = specialValue;
                    break;
                case Keys.RShiftKey:
                case Keys.LShiftKey:
                    shift = specialValue;
                    break;
            }

            return new KeyInfo(keyData, control, alt, shift);
        }

        private static int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                var keyboardHookStruct = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));

                KeyInfo keyInfo = null;
                if (wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN)
                {
                    var keyData = (Keys)keyboardHookStruct.VirtualKeyCode;
                    keyInfo = GetKeys(keyData, true);
                    keyDown.SafeInvoke(keyInfo);
                }

                if (wParam == WM_KEYUP || wParam == WM_SYSKEYUP)
                {
                    var keyData = (Keys)keyboardHookStruct.VirtualKeyCode;
                    keyInfo = GetKeys(keyData, false);
                    keyUp.SafeInvoke(keyInfo);
                }

                if (keyInfo != null && keyInfo.Handled)
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
                hookProc = KeyboardHookProc;
                handle = WinApi.SetWindowsHookEx(WH_KEYBOARD_LL, hookProc, IntPtr.Zero, 0);
                if (handle == 0)
                {
                    int errorCode = Marshal.GetLastWin32Error();
                    throw new Win32Exception(errorCode);
                }
            }
        }

        private static void TryUnsubscribe()
        {
            if (handle != 0 && keyDown == null && keyUp == null)
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

        #region Keyboard events

        private static event Action<KeyInfo> keyUp;

        public static event Action<KeyInfo> KeyUp
        {
            add
            {
                Subscribe();
                keyUp += value;
            }
            remove
            {
                keyUp -= value;
                TryUnsubscribe();
            }
        }

        private static event Action<KeyInfo> keyDown;

        public static event Action<KeyInfo> KeyDown
        {
            add
            {
                Subscribe();
                keyDown += value;
            }
            remove
            {
                keyDown -= value;
                TryUnsubscribe();
            }
        }

        #endregion
    }
}