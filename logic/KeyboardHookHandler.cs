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
		private readonly IntPtr WM_KEYDOWN = (IntPtr)0x0100;
		private readonly IntPtr WM_KEYUP = (IntPtr)0x0101;
		
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
		/// <returns>Pointer to next hook or pointer to -1 when hook is fully processed.</returns>
		private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
		
		/// <summary>
		/// Pointer to hook, used to disable it.
		/// </summary>
		private readonly IntPtr _hookId;
		
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
			_hookId = SetWindowsHookEx(WH_KEYBOARD_LL, HandleHook, GetModuleHandle(curModuleName), 0);
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
			
			if (wParam == WM_KEYDOWN)
			{
				switch (vkCode)
				{
					// set modifier flags if necessary
					case _winKeyCode:
						_winDown = true;
						return CallNextHookEx(_hookId, nCode, wParam, lParam);
					case _lShiftCode:
						_lShiftDown = true;
						return CallNextHookEx(_hookId, nCode, wParam, lParam);
					case _rShiftCode:
						_rShiftDown = true;
						return CallNextHookEx(_hookId, nCode, wParam, lParam);
					case _mCode:
						if (_winDown)
						{
							// TODO minimizing
							return (IntPtr)(-1);
						}
						return CallNextHookEx(_hookId, nCode, wParam, lParam);
				}
			}
			// reset relevant flag on keyup
			if (wParam == WM_KEYUP)
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
			return CallNextHookEx(_hookId, nCode, wParam, lParam);
		}
		
		/// <summary>
		/// Destructor that removes hook.
		/// </summary>
		~KeyboardHookHandler()
		{
			UnhookWindowsHookEx(_hookId);
		}
		
		// Winapi imports below
		
		[DllImport("user32.dll")]
		private static extern IntPtr SetWindowsHookEx(int idHook,
		                                              LowLevelKeyboardProc lpfn, 
		                                              IntPtr hMod, uint dwThreadId);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool UnhookWindowsHookEx(IntPtr hhk);

		[DllImport("user32.dll")]
		private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
		                                            IntPtr wParam, IntPtr lParam);

		[DllImport("kernel32.dll")]
		private static extern IntPtr GetModuleHandle(string lpModuleName);
	}
}
