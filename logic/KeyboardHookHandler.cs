﻿using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace winmplusplus3.Logic
{
	/// <summary>
	/// Class that intercepts win-m and win-shift-m hotkeys and calls minimizing accordingly.
	/// </summary>
	public class KeyboardHookHandler
	{
		// keycodes to intercept
		private const int _lWinKeyCode = 91;
		private const int _rWinKeyCode = 92;
		private const int _lShiftCode = 160;
		private const int _rShiftCode = 161;
		private const int _mCode = 77;
		
		// Winapi constants
		private const int WH_KEYBOARD_LL = 0xD;
		private const int WM_KEYDOWN = 0x0100;
		private const int WM_KEYUP = 0x0101;
		
		// flags to indicate modifier key states
		private bool _lWinDown;
		private bool _rWinDown;
		private bool _lShiftDown;
		private bool _rShiftDown;
		
		/// <summary>
		/// Flag used when "exceptions.txt" was not loaded correctly.
		/// </summary>
		public bool ExclusionsLoadError {get; private set;}

		/// <summary>
		/// Delegate for low-level keyboard hook callback.
		/// </summary>
		/// <param name="nCode">Process parameter. If less than zero,
		/// we must call CallNextHookEx.</param>
		/// <param name="wParam">Event type, such as WM_KEYUP.</param>
		/// <param name="lParam">Key code.</param>
		/// <returns>Pointer to next hook or non-zero pointer
		/// when hook is fully processed.</returns>
		private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
		
		/// <summary>
		/// Used to get all the windows at once and the focused one specifically if necessary.
		/// </summary>
		private readonly WindowEnumerator _windowEnumerator = new WindowEnumerator();
		
		/// <summary>
		/// Used to actually minimize windows.
		/// </summary>
		private readonly WindowMinimizer _windowMinimizer = new WindowMinimizer();
		
		/// <summary>
		/// Pointer to hook, used to disable it.
		/// </summary>
		private readonly IntPtr _hookId;
		
		/// <summary>
		/// Handle to hook callback delegate. Used to prevent it being eaten alive
		/// by GC causing crashes.
		/// </summary>
		private readonly GCHandle gch;
		
		/// <summary>
		/// Boolean indicating whether this hook handler is enabled.
		/// </summary>
		public bool Enabled = true;
		
		/// <summary>
		/// Collection that holds titles of windows not to minimize.
		/// </summary>
		private readonly IReadOnlyCollection<string> _excluded;
		
		/// <summary>
		/// Constructor that sets the hook.
		/// </summary>
		public KeyboardHookHandler()
		{
			var loader = new ExcludedLoader();
			try
			{
				_excluded = loader.Excluded;
			}
			catch (ApplicationException)
			{
				_excluded = loader.Defaults;
				ExclusionsLoadError = true;
			}
			// set the hook
			string curModuleName = Process.GetCurrentProcess().MainModule.ModuleName;
			var callback = new LowLevelKeyboardProc(HandleHook);
			// GC protection
			gch = GCHandle.Alloc(callback);
			_hookId = SetWindowsHookEx(WH_KEYBOARD_LL, callback,
				GetModuleHandle(curModuleName), 0);
		}
		
		/// <summary>
		/// Hook callback. See LowLevelKeyboardProc for details.
		/// </summary>
		private IntPtr HandleHook(int nCode, IntPtr wParam, IntPtr lParam)
		{
			// if hook is disabled, we just pass on to the next one in chain
			if (!Enabled || nCode < 0)
			{
				return CallNextHookEx(_hookId, nCode, wParam, lParam);
			}
			// getting the parameters
			var vkCode = Marshal.ReadInt32(lParam);
			var param = (int)wParam;
			
			if (param == WM_KEYDOWN)
			{
				switch (vkCode)
				{
					// set modifier flags if necessary
					case _lWinKeyCode:
						_lWinDown = true;
						break;
					case _rWinKeyCode:
						_rWinDown = true;
						break;
					case _lShiftCode:
						_lShiftDown = true;
						break;
					case _rShiftCode:
						_rShiftDown = true;
						break;
					case _mCode:
						if (_lWinDown || _rWinDown)
						{
							// fire and forget, no awaiting
							Task.Run(() => DoWork());
							// prevents other apps from processing this keypress
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
					case _lWinKeyCode:
						_lWinDown = false;
						break;
					case _rWinKeyCode:
						_rWinDown = false;
						break;
					case _lShiftCode:
						_lShiftDown = false;
						break;
					case _rShiftCode:
						_rShiftDown = false;
						break;
				}
			}
			// if it's neither win-m nor win-shift-m, pass on
			return CallNextHookEx(_hookId, nCode, wParam, lParam);
		}
		
		/// <summary>
		/// Minimizes windows based on Shift key state: if any pressed, uses BasicFilter,
		/// else uses CurrentScreenFilter. Runs as Task on separate thread.
		/// </summary>
		private void DoWork()
		{
			IFilter filterToUse = (_lShiftDown || _rShiftDown) ?
				new BasicFilter(_excluded) :
				new CurrentScreenFilter(_excluded, _windowEnumerator.GetForeground());

			_windowMinimizer.Minimize(filterToUse.Filter(_windowEnumerator.Enumerate()));
		}
		
		/// <summary>
		/// Destructor that removes hook and GC protection.
		/// </summary>
		~KeyboardHookHandler()
		{
			UnhookWindowsHookEx(_hookId);
			gch.Free();
		}
		
		// Winapi imports below
		
		[DllImport("user32.dll")]
		private static extern IntPtr SetWindowsHookEx(int idHook,
			LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

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
