using System;
using System.Windows.Forms;

using winmplusplus3.Logic;
using winmplusplus3.UI;

namespace winmplusplus3
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm(new AutorunManager(), new KeyboardHookHandler()));
		}
		
	}
}
