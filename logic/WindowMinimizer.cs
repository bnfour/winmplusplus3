using System;
using System.Runtime.InteropServices;

namespace winmplusplus3.Logic
{
	/// <summary>
	/// Class that encapsulates window minimizing.
	/// </summary>
	public class WindowMinimizer
	{
		#region WinAPI constant

		private const int SW_MINIMIZE = 0x6;

		#endregion

		/// <summary>
		/// Method that utilises Winapi to minimize a single Window.
		/// </summary>
		/// <param name="window">Window to minimize.</param>
		public void Minimize(Window window) => ShowWindowAsync(window.hWnd, SW_MINIMIZE);


		#region WinAPI import

		[DllImport("user32.dll")]
		private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

		#endregion
	}
}
