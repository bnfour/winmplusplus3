using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace winmplusplus3
{
	/// <summary>
	/// Description of WindowMinimizer.
	/// </summary>
	public class WindowMinimizer
	{
		// winapi constant to minimize via ShowWindowAsync
		private const int SW_MINIMIZE = 0x6;
		
		/// <summary>
		/// Constructor that does literally nothing.
		/// </summary>
		public WindowMinimizer()
		{
		}
		
		/// <summary>
		/// Minimizes all Windows in a given list.
		/// </summary>
		/// <param name="windows">List of Windows to minimize.</param>
		public void Minimize(List<Window> windows)
		{
			foreach (var window in windows)
			{
				Minimize(window);
			}
		}
		
		/// <summary>
		/// Method that utilises Winapi to minimize a single Window.
		/// </summary>
		/// <param name="window">Window to minimize.</param>
		private void Minimize(Window window)
		{
			ShowWindowAsync(window.hWnd, SW_MINIMIZE);
		}
		
		// winapi import below
		
		[DllImport("user32.dll")]
		private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
	}
}
