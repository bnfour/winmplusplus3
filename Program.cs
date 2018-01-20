using System;
using System.Windows.Forms;

namespace winmplusplus3
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		private readonly static AutorunManager _autorunManager = new AutorunManager();
		
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm(_autorunManager));
		}
		
	}
}
