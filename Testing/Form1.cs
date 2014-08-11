using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Testing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /* Code to Disable WinKey, Alt+Tab, Ctrl+Esc Starts Here */


        // Structure contain information about low-level keyboard input event 
        [StructLayout(LayoutKind.Sequential)]
        private struct KBDLLHOOKSTRUCT
        {
            public Keys key;
            public int scanCode;
            public int flags;
            public int time;
            public IntPtr extra;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string className, string windowText);
        [DllImport("user32.dll")]
        private static extern int ShowWindow(IntPtr hwnd, int command);
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindowEx(
               IntPtr parentHwnd,
               IntPtr childAfterHwnd,
               IntPtr className,
               string windowText);
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessageW(IntPtr hWnd, int Msg,
            IntPtr wParam, IntPtr lParam);
        private const int SW_HIDE = 0;
        private const int SW_SHOW = 1;
        private const int APPCOMMAND_VOLUME_MUTE = 0x80000;
        private const int WM_APPCOMMAND = 0x319;
        private string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        //System level functions to be used for hook and unhook keyboard input  
        public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int id, LowLevelKeyboardProc callback, IntPtr hMod, uint dwThreadId);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hook);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hook, int nCode, IntPtr wp, IntPtr lp);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string name);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern short GetAsyncKeyState(Keys key);
        //Declaring Global objects     
        private IntPtr ptrHook;
        public bool isShown = false;
        public bool isMuted = false;
        public IntPtr hwnd = FindWindow("Shell_TrayWnd", "");
        public IntPtr hwndOrb = FindWindowEx(IntPtr.Zero, IntPtr.Zero, (IntPtr)0xC017, null);
        public LowLevelKeyboardProc objKeyboardProcess;

        private IntPtr captureKey(int nCode, IntPtr wp, IntPtr lp)
        {
            if (nCode >= 0)
            {
                KBDLLHOOKSTRUCT objKeyInfo = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lp, typeof(KBDLLHOOKSTRUCT));

                // Disabling Windows keys 
                if (objKeyInfo.key == Keys.L && (Control.ModifierKeys & Keys.Alt) == Keys.Alt && (Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    if (isMuted == false)
                    {
                        ToggleMute();
                        isMuted = true;
                    }
                    ShowWindow(hwnd, SW_HIDE);
                    ShowWindow(hwndOrb, SW_HIDE);
                    KillCtrlAltDelete("1");
                    isShown = true;
                    this.Show();
                }
                if (isShown)
                {
                    if (objKeyInfo.key == Keys.F4 || objKeyInfo.key == Keys.Alt && HasAltModifier(objKeyInfo.flags) ||
                        objKeyInfo.key == Keys.RWin || objKeyInfo.key == Keys.LWin ||
                        objKeyInfo.key == Keys.Tab && HasAltModifier(objKeyInfo.flags) ||
                        objKeyInfo.key == Keys.Escape && (Control.ModifierKeys & Keys.Control) == Keys.Control)
                    {
                        return (IntPtr) 1; // if 0 is returned then All the above keys will be enabled
                    }

                    if (objKeyInfo.key == Keys.Enter)
                    {
                        checkPassword(nsTextBox1.Text);
                    }
                }
            }
            return CallNextHookEx(ptrHook, nCode, wp, lp);
        }

        bool HasAltModifier(int flags)
        {
            return (flags & 0x20) == 0x20;
        }

        /* Code to Disable WinKey, Alt+Tab, Ctrl+Esc Ends Here */

        private void Form1_Load(object sender, System.EventArgs e)
        {
            ProcessModule objCurrentModule = Process.GetCurrentProcess().MainModule;
            objKeyboardProcess = new LowLevelKeyboardProc(captureKey);
            ptrHook = SetWindowsHookEx(13, objKeyboardProcess, GetModuleHandle(objCurrentModule.ModuleName), 0);
            this.Hide();
        }

        private void nsButton1_Click(object sender, EventArgs e)
        {
            checkPassword(nsTextBox1.Text);
        }

        public void checkPassword(string password)
        {
            nsTextBox1.Text = "";
            if (password == "unlockPassword")
            {
                ToggleMute();
                isMuted = false;
                KillCtrlAltDelete("0");
                isShown = false;
                ShowWindow(hwnd, SW_SHOW);
                ShowWindow(hwndOrb, SW_SHOW);
                this.Hide();
            }
            if (password == "unlockPasswordExit")
            {
                ToggleMute();
                isMuted = false;
                KillCtrlAltDelete("0");
                ShowWindow(hwnd, SW_SHOW);
                ShowWindow(hwndOrb, SW_SHOW);
                Application.Exit();
            }
            else if (password != "")
            {
                Log(DateTime.Now + password + "\r\n");
            }
        }

        private void ToggleMute()
        {
            SendMessageW(this.Handle, WM_APPCOMMAND, this.Handle,
                (IntPtr)APPCOMMAND_VOLUME_MUTE);
        }

        private void Log(string log)
        {
            File.AppendAllText(path + "\\login.log", log);
        }
        public void KillCtrlAltDelete(string value)
        {
            RegistryKey regkey;
            string keyValueInt = value;
            string subKey = "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System";

            if (value == "1")
            {
                try
                {
                    regkey = Registry.CurrentUser.CreateSubKey(subKey);
                    regkey.SetValue("DisableTaskMgr", keyValueInt);
                    regkey.Close();
                }
                catch (Exception ex)
                {
                }
            }
            else if (value == "0")
            {
                try
                {
                    regkey = Registry.CurrentUser.OpenSubKey(subKey, true);
                    regkey.DeleteValue("DisableTaskMgr");
                    regkey.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}
