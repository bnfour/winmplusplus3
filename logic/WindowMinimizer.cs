using System;
using System.Runtime.InteropServices;

namespace winmplusplus3.Logic
{
	/// <summary>
	/// Class that encapsulates window minimizing.
	/// </summary>
	public class WindowMinimizer
	{
		// winapi constant to minimize via ShowWindowAsync
		private const int SW_MINIMIZE = 0x6;
		
		/// <summary>
		/// Method that utilises Winapi to minimize a single Window.
		/// </summary>
		/// <param name="window">Window to minimize.</param>
		public void Minimize(Window window) => ShowWindowAsync(window.hWnd, SW_MINIMIZE);

		// winapi import below
		
		[DllImport("user32.dll")]
		private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
	}
}
