using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace winmplusplus3.Logic
{
	/// <summary>
	/// Class that intercepts win-m and win-shift-m hotkeys
	/// and calls minimizing accordingly.
	/// </summary>
	public class KeyboardHookHandler
	{
		// keycodes to intercept
		private const int _lWinKeyCode = 91;
		private const int _rWinKeyCode = 92;
		private const int _lShiftCode = 160;
		private const int _rShiftCode = 161;

		private const int _mCode = 77;

		/// <summary>
		/// Dictionary to store relevalnt modifier states.
		/// True means key is pressed right now.
		/// </summary>
		private readonly Dictionary<int, bool> _modifiers = new Dictionary<int, bool>()
		{
			// it is _supposed_ that no other keys should be added
			// this isn't enforced though, there's no dict with fixed Keys AFAIK
			[_lWinKeyCode] = false,
			[_rWinKeyCode] = false,
			[_lShiftCode] = false,
			[_rShiftCode] = false
		};

		// Winapi constants
		private const int WH_KEYBOARD_LL = 0xD;
		private const int WM_KEYDOWN = 0x0100;
		private const int WM_KEYUP = 0x0101;
		
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
		private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam,
			IntPtr lParam);
		
		/// <summary>
		/// Used to get all the windows at once
		/// and the focused one specifically if necessary.
		/// </summary>
		private readonly WindowEnumerator _enumerator = new WindowEnumerator();
		
		/// <summary>
		/// Used to actually minimize windows.
		/// </summary>
		private readonly WindowMinimizer _minimizer = new WindowMinimizer();
		
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
		/// True by default. That means there's no way to start this app
		/// in disabled state.
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
			// if hook is disabled or keycode is nonsense
			// we just pass on to the next hook in chain
			if (!Enabled || nCode < 0)
			{
				return CallNextHookEx(_hookId, nCode, wParam, lParam);
			}
			// getting the parameters
			var vkCode = Marshal.ReadInt32(lParam);
			var param = (int)wParam;
			
			if (param == WM_KEYDOWN)
			{
				if (_modifiers.Keys.Contains(vkCode))
				{
					_modifiers[vkCode] = true;
				}
				else if (vkCode == _mCode)
				{
					if (_modifiers[_lWinKeyCode] || _modifiers[_rWinKeyCode])
					{
						// fire and forget, no awaiting
						Task.Run(() => DoWork());
						// prevents other apps from processing this keypress
						return (IntPtr)1;
					}
				}
			}
			// reset relevant flag on keyup
			else if (param == WM_KEYUP)
			{
				if (_modifiers.Keys.Contains(vkCode))
				{
					_modifiers[vkCode] = false;
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
			IFilter filterToUse = (_modifiers[_lShiftCode] || _modifiers[_rShiftCode]) ?
				new BasicFilter(_excluded) :
				new CurrentScreenFilter(_excluded, _enumerator.GetForeground());
			// yay linq
			var windowsToMinimizeQuery =
				from w in _enumerator.Enumerate()
				where filterToUse.Filter(w)
				select w;

			foreach (var w in windowsToMinimizeQuery)
			{
				_minimizer.Minimize(w);
			}
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
