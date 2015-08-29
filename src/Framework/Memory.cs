using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using PoeHUD.Framework.Enums;
using PoeHUD.Models;
using PoeHUD.Poe;

namespace PoeHUD.Framework
{
    public class Memory : IDisposable
    {
        public readonly int AddressOfProcess;
        private readonly Dictionary<string, int> modules;
        private bool closed;
        public Offsets offsets;
        private IntPtr procHandle;

        public Memory(Offsets offs, int pId)
        {
            try
            {
                offsets = offs;
                Process = Process.GetProcessById(pId);
                AddressOfProcess = Process.MainModule.BaseAddress.ToInt32();
                Open();
                modules = new Dictionary<string, int>();
            }
            catch (Win32Exception ex)
            {
                throw new Exception("You should run program as an administrator", ex);
            }
        }

        public Process Process { get; private set; }

        public void Dispose()
        {
            Close();
        }

        ~Memory()
        {
            Close();
        }

        public int GetModule(string name)
        {
            if (modules.ContainsKey(name))
            {
                return modules[name];
            }
            int num = Process.Modules.Cast<ProcessModule>().First(m => m.ModuleName == name).BaseAddress.ToInt32();
            modules.Add(name, num);
            return num;
        }

        public bool IsInvalid()
        {
            return Process.HasExited || closed;
        }

        public int ReadInt(int addr)
        {
            return BitConverter.ToInt32(ReadMem(addr, 4), 0);
        }

        public int ReadInt(int addr, params int[] offsets)
        {
            int num = ReadInt(addr);
            return offsets.Aggregate(num, (current, num2) => ReadInt(current + num2));
        }

        public float ReadFloat(int addr)
        {
            return BitConverter.ToSingle(ReadMem(addr, 4), 0);
        }

        public long ReadLong(int addr)
        {
            return BitConverter.ToInt64(ReadMem(addr, 8), 0);
        }

        public uint ReadUInt(int addr)
        {
            return BitConverter.ToUInt32(ReadMem(addr, 4), 0);
        }

        public short ReadShort(int addr)
        {
            return BitConverter.ToInt16(ReadMem(addr, 2), 0);
        }

        /// <summary>
        /// Read string as ASCII
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="length"></param>
        /// <param name="replaceNull"></param>
        /// <returns></returns>
        public string ReadString(int addr, int length = 256, bool replaceNull = true)
        {
            if (addr <= 65536 && addr >= -1)
            {
                return string.Empty;
            }
            string @string = Encoding.ASCII.GetString(ReadMem(addr, length));
            if (replaceNull)
                return RTrimNull(@string);
            return @string;
        }

        private static string RTrimNull(string text)
        {
            int num = text.IndexOf('\0');
            if (num > 0)
            {
                return text.Substring(0, num);
            }
            return String.Empty;
        }

        /// <summary>
        /// Read string as Unicode
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="length"></param>
        /// <param name="replaceNull"></param>
        /// <returns></returns>
        public string ReadStringU(int addr, int length = 256, bool replaceNull = true)
        {
            if (addr <= 65536 && addr >= -1)
            {
                return string.Empty;
            }
            byte[] mem = ReadMem(addr, length);
            if (mem[0] == 0 && mem[1] == 0)
                return string.Empty;
            string @string = Encoding.Unicode.GetString(mem);
            if (replaceNull)
                return RTrimNull(@string);
            return @string;
        }

        public byte ReadByte(int addr)
        {
            return ReadBytes(addr, 1)[0];
        }

        public byte[] ReadBytes(int addr, int length)
        {
            return ReadMem(addr, length);
        }

        private void Open()
        {
            procHandle = WinApi.OpenProcess(Process, ProcessAccessFlags.All);
        }

        private bool Close()
        {
            if (!closed)
            {
                closed = true;
                return WinApi.CloseHandle(procHandle);
            }
            return true;
        }

        private byte[] ReadMem(int addr, int size)
        {
            var array = new byte[size];
            WinApi.ReadProcessMemory(procHandle, (IntPtr)addr, array);
            return array;
        }

        [DllImport("kernel32.dll")]
        private static extern bool VirtualProtectEx(IntPtr hProcess, IntPtr lpAddress, IntPtr dwSize, uint flNewProtect,
            ref uint lpflOldProtect);

        public int[] FindPatterns(params Pattern[] patterns)
        {
            byte[] exeImage = ReadBytes(AddressOfProcess, 0x2000000); //33mb
            var address = new int[patterns.Length];

            for (int iPattern = 0; iPattern < patterns.Length; iPattern++)
            {
                Pattern pattern = patterns[iPattern];
                byte[] patternData = pattern.Bytes;
                int patternLength = patternData.Length;

                for (int offset = 0; offset < exeImage.Length - patternLength; offset += 4)
                {
                    if (CompareData(pattern, exeImage, offset))
                    {
                        address[iPattern] = offset;
                        Console.WriteLine("Pattern " + iPattern + " is found at " +
                                          (AddressOfProcess + offset).ToString("X"));
                        break;
                    }
                }
            }
            return address;
        }

   
        private bool CompareData(Pattern pattern, byte[] data, int offset)
        {
            for (int i = 0; i < pattern.Bytes.Length; i++)
            {
                if (pattern.Mask[i] == 'x' && pattern.Bytes[i] != data[offset + i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}