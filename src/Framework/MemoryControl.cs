using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PoeHUD.Framework
{
    public class MemoryControl
    {
        private static MemoryControl memoryControl;
        private long lastTime;

        private MemoryControl()
        {
            lastTime = DateTime.Now.Ticks;
            Application.Idle += delegate
            {
                try
                {
                    long ticks = DateTime.Now.Ticks;
                    if (ticks - lastTime > 10000000L)
                    {
                        lastTime = ticks;
                        MemoryFree();
                    }
                }
                catch
                {
                    // ignored
                }
            };
        }


        private void MemoryFree()
        {
            try
            {
                using (Process currentProcess = Process.GetCurrentProcess())
                {
                   WinApi.SetProcessWorkingSetSize(currentProcess.Handle, -1, -1);
                }
            }
            catch
            {
                // ignored
            }
        }

        public static void Start()
        {
            try
            {
                if (memoryControl == null && Environment.OSVersion.Platform == PlatformID.Win32NT)
                {
                    memoryControl = new MemoryControl();
                }
            }
            catch
            {
                // ignored
            }
        }
    }
}