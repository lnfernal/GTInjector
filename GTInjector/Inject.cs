using System.Windows;
using System;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using System.Text;
string dllname = "dllname.dll";
//SOME PARTS OF THE CODE ARE FROM STACKOVERFLOW
//SOME PARTS OF THE CODE ARE FROM STACKOVERFLOW
//SOME PARTS OF THE CODE ARE FROM STACKOVERFLOW
//SOME PARTS OF THE CODE ARE FROM STACKOVERFLOW

[DllImport("kernel32.dll")]
 static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
static extern IntPtr GetModuleHandle(string lpModuleName);

[DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress,
    uint dwSize, uint flAllocationType, uint flProtect);

[DllImport("kernel32.dll", SetLastError = true)]
static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesWritten);

[DllImport("kernel32.dll")]
static extern IntPtr CreateRemoteThread(IntPtr hProcess,
    IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);


Process[] pname = Process.GetProcessesByName("growtopia");
if (pname.Length == 0)
    Console.WriteLine("Growtopia not found!");
else
{
    Console.WriteLine("Growtopia found! starting injection!");
    Thread.Sleep(2000);
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Injecting.");
    Console.ForegroundColor = ConsoleColor.Green;
    Thread.Sleep(2000);
    Console.WriteLine("Injecting..");
    Console.ForegroundColor = ConsoleColor.Blue;
    Thread.Sleep(2000);
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("Injecting...");
    Thread.Sleep(4000);
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("Loading DLL!");
    Thread.Sleep(6000);
    var handleprocess = OpenProcess(0x0002 | 0x040 | 0x0008 | 0x0020 | 0x0010, false, pname[0].Id);
    var loadlib = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");
    var filename = new FileInfo(dllname);
    var allocated = VirtualAllocEx(handleprocess, IntPtr.Zero, (uint)((filename.FullName.Length + 1) * Marshal.SizeOf(typeof(char))), 0x00001000 | 0x00002000, 4);
    WriteProcessMemory(handleprocess, allocated, Encoding.Default.GetBytes(filename.FullName), (uint)((filename.FullName.Length + 1) * Marshal.SizeOf(typeof(char))), out _);
    CreateRemoteThread(handleprocess, IntPtr.Zero, 0, loadlib, allocated, 0, IntPtr.Zero);
   
}
     