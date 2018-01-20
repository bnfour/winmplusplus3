using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace winmplusplus3
{
	/// <summary>
	/// Main form that houses tray icon and its menu.
	/// </summary>
	public partial class MainForm : Form
	{
		// TODO: add AboutForm field and references to AutorunManager and KeyBoardHookHandler.
		
		/// <summary>
		/// MainForm constructor.
		/// </summary>
		public MainForm()
		{
			//
			// The InitializeComponent() call is required 
			// for Windows Forms designer support.
			//
			InitializeComponent();
			
			// TODO: pass the references for classes used throughout handlers
			// TODO: set Run at startup value from AutorunManager
		}
		
		/// <summary>
		/// Form Shown event handler.
		/// Hides the form.
		/// </summary>
		/// <param name="sender">Event sender, MainForm itself.</param>
		/// <param name="e">Event arguments, unused.</param>
		private void MainFormShown(object sender, EventArgs e)
		{
			Hide();
		}
		
		/// <summary>
		/// Handler for "Enabled" item in the tray menu.
		/// Enables or disables app action.
		/// </summary>
		/// <param name="sender">Event sender, enabledToolStripMenuItem.</param>
		/// <param name="e">Event arguments, unused.</param>
		private void EnabledToolStripMenuItemClick(object sender, EventArgs e)
		{
			// TODO: set KeyboardHookHandler state accordingly and update icon
		}
		
		/// <summary>
		/// Handler for "Run at startup" item in the tray menu.
		/// Updates autorun state via AutorunManager.
		/// </summary>
		/// <param name="sender">Event sender, runAtStartupToolStripMenuItem.</param>
		/// <param name="e">Event arguments, unused.</param>
		private void RunAtStartupToolStripMenuItemClick(object sender, EventArgs e)
		{
			// TODO: set AutorunManager state accordingly
		}
		
		/// <summary>
		/// Handler for "About Win-M++" item in the tray menu.
		/// Displays AboutForm instance from this class if not already shown.
		/// </summary>
		/// <param name="sender">Event sender, aboutWinMToolStripMenuItem.</param>
		/// <param name="e">Event arguments, unused.</param>
		private void AboutWinMToolStripMenuItemClick(object sender, EventArgs e)
		{
			// TODO: show AboutForm after it's designed
		}
		
		/// <summary>
		/// Handler for "Quit" item in the tray menu.
		/// Obviously quits the program.
		/// </summary>
		/// <param name="sender">Event sender, quitToolStripMenuItem.</param>
		/// <param name="e">Event arguments, unused.</param>
		private void QuitToolStripMenuItemClick(object sender, EventArgs e)
		{
			Application.Exit();
		}
	}
}
