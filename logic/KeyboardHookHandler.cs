using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace winmplusplus3
{
	/// <summary>
	/// Class that intercepts win-m and win-shift-m hotkeys and calls minimizing accordingly.
	/// </summary>
	public class KeyboardHookHandler
	{
		// keycodes to intercept
		private const int _winKeyCode = 92;
		private const int _lShiftCode = 160;
		private const int _rShiftCode = 161;
		private const int _mCode = 77;
		
		// Winapi constants
		private const int WH_KEYBOARD_LL = 0xD;
		private const int WM_KEYDOWN = 0x0100;
		private const int WM_KEYUP = 0x0101;
		
		// flags to indicate state
		private bool _winDown;
		private bool _lShiftDown;
		private bool _rShiftDown;
		
		/// <summary>
		/// Delegate for low-level keyboard hook callback.
		/// </summary>
		/// <param name="nCode">Process parameter. If less than zero, we must call CallNextHookEx.</param>
		/// <param name="wParam">Event type, such as WM_KEYUP.</param>
		/// <param name="lParam">Key code.</param>
		/// <returns>Pointer to next hook or non-zero pointer when hook is fully processed.</returns>
		private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
		
		/// <summary>
		/// Pointer to hook, used to disable it.
		/// </summary>
		private readonly IntPtr _hookId;
		private readonly GCHandle gch;
		/// <summary>
		/// Boolean indicating whether this hook handler is enabled.
		/// </summary>
		public bool Enabled = true;
		
		/// <summary>
		/// Constructor that sets the hook.
		/// </summary>
		public KeyboardHookHandler()
		{
			string curModuleName = Process.GetCurrentProcess().MainModule.ModuleName;
			var callback = new LowLevelKeyboardProc(this.HandleHook);
			gch = GCHandle.Alloc(callback);
			_hookId = SetWindowsHookEx(WH_KEYBOARD_LL, callback, GetModuleHandle(curModuleName), 0);
		}
		
		/// <summary>
		/// Hook callback. See LowLevelKeyboardProc for details.
		/// </summary>
		private IntPtr HandleHook(int nCode, IntPtr wParam, IntPtr lParam)
		{
			// if hook is disabled, we just pass on next one
			if (!Enabled || nCode < 0)
			{
				return CallNextHookEx(_hookId, nCode, wParam, lParam);
			}
			// getting the key code
			var vkCode = Marshal.ReadInt32(lParam);
			var param = (int)wParam;
			
			if (param == WM_KEYDOWN)
			{
				switch (vkCode)
				{
					// set modifier flags if necessary
					case _winKeyCode:
						_winDown = true;
						break;
					case _lShiftCode:
						_lShiftDown = true;
						break;
					case _rShiftCode:
						_rShiftDown = true;
						break;
					case _mCode:
						if (_winDown)
						{
							// TODO minimizing
							return (IntPtr)1;
						}
						break;
				}
			}
			// reset relevant flag on keyup
			else if (param == WM_KEYUP)
			{
				switch (vkCode)
				{
					case _winKeyCode:
						_winDown = false;
						break;
					case _lShiftCode:
						_lShiftDown = false;
						break;
					case _rShiftCode:
						_rShiftDown = false;
						break;
				}
			}
			return CallNextHookEx(_hookId, nCode, wParam, lParam);;
		}
		
		/// <summary>
		/// Destructor that removes hook.
		/// </summary>
		~KeyboardHookHandler()
		{
			UnhookWindowsHookEx(_hookId);
			gch.Free();
		}
		
		// Winapi imports below
		
		[DllImport("user32.dll", SetLastError=true)]
		private static extern IntPtr SetWindowsHookEx(int idHook,
		                                              LowLevelKeyboardProc lpfn, 
		                                              IntPtr hMod, uint dwThreadId);

		[DllImport("user32.dll", SetLastError=true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool UnhookWindowsHookEx(IntPtr hhk);

		[DllImport("user32.dll", SetLastError=true)]
		private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
		                                            IntPtr wParam, IntPtr lParam);

		[DllImport("kernel32.dll", SetLastError=true)]
		private static extern IntPtr GetModuleHandle(string lpModuleName);
	}
}
