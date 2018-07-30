using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace winmplusplus3.Logic
{
	/// <summary>
	/// Class that incapsulates getting actual window info as instances of Window.
	/// </summary>
	public class WindowEnumerator
	{
		/// <summary>
		/// List used to store values returned by callback.
		/// </summary>
		private readonly List<Window> _windows = new List<Window>();
		
		/// <summary>
		/// Handle to assure our delegate will not be eaten by GC.
		/// Might be unnecessary, not willing to test that.
		/// </summary>
		private readonly GCHandle gch;
		
		/// <summary>
		/// Callback to EnumWindows.
		/// </summary>
		private readonly EnumWindowsCallback callback;
		
		/// <summary>
		/// Constructor which sets handles.
		/// </summary>
		public WindowEnumerator()
		{
			callback = new EnumWindowsCallback(Callback);
			// better safe than sorry, I've wasted ~8 hours before finding this
			gch = GCHandle.Alloc(callback);
		}
		
		/// <summary>
		/// A function to enumerate all windows.
		/// </summary>
		/// <param name="hWnd">Handle to window.</param>
		/// <param name="lParam">Unused.</param>
		/// <returns>True to continue enumeration, false otherwise.</returns>
		private delegate bool EnumWindowsCallback(IntPtr hWnd, IntPtr lParam);
		
		/// <summary>
		/// Gets window title by its handle.
		/// </summary>
		/// <param name="hwnd">Handle to window.</param>
		/// <returns>Window's title as a string.</returns>
		private string GetWindowTitle(IntPtr hwnd)
		{
			// + 1 is necessary
			int size = GetWindowTextLength(hwnd) + 1;
			var sb = new StringBuilder(size);
			GetWindowText(hwnd, sb, size);
			return sb.ToString();
		}
		
		/// <summary>
		/// Constructs new Window instance by its handle.
		/// </summary>
		/// <param name="hwnd">Handle to window.</param>
		/// <returns>Window instance with set fields.</returns>
		private Window ConstructWindow(IntPtr hwnd)
		{
			var title = GetWindowTitle(hwnd);
			var screen = System.Windows.Forms.Screen.FromHandle(hwnd);
			var visible = IsWindowVisible(hwnd);
			return new Window(hwnd, title, screen, visible);
		}
		
		/// <summary>
		/// Returns Window which is currently focused.
		/// </summary>
		/// <returns>Foreground window as Window.</returns>
		public Window GetForeground()
		{
			return ConstructWindow(GetForegroundWindow());
		}
		
		/// <summary>
		/// Enumerates all windows in system.
		/// </summary>
		/// <returns>List of Windows in system.</returns>
		public IEnumerable<Window> Enumerate()
		{
			_windows.Clear();
			EnumWindows(callback, IntPtr.Zero);
			return _windows;
		}
		
		/// <summary>
		/// EnumWindows callback.
		/// See EnumWindowsCallback for details.
		/// </summary>
		private bool Callback(IntPtr hWnd, IntPtr lParam)
		{
			if (!hWnd.Equals(IntPtr.Zero))
			{
				_windows.Add(ConstructWindow(hWnd));
			}
			return true;
		}
		
		/// <summary>
		/// Destructor which removes handle.
		/// </summary>
		~WindowEnumerator()
		{
			gch.Free();
		}
		
		// winapi imports below
		
		[DllImport("user32.dll")] 
		private static extern int GetWindowText(IntPtr hWnd, StringBuilder strText, int maxCount); 
		
		[DllImport("user32.dll")] 
		private static extern int GetWindowTextLength(IntPtr hWnd); 
		
		[DllImport("user32.dll")]
		private static extern bool IsWindowVisible(IntPtr hWnd);
		
		[DllImport("user32.dll")]
		private static extern IntPtr GetForegroundWindow();
		
		[DllImport("user32.dll")]
		private static extern bool EnumWindows(EnumWindowsCallback enumProc, IntPtr lParam);
	}
}
