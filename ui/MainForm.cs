using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace winmplusplus3
{
	/// <summary>
	/// Main form that houses tray icon and its menu.
	/// </summary>
	public partial class MainForm : Form
	{
		private AboutForm _aboutForm = new AboutForm();
		private readonly AutorunManager _autorunManager;
		private readonly KeyboardHookHandler _keyboardHookHandler;
		
		/// <summary>
		/// MainForm constructor.
		/// </summary>
		public MainForm(AutorunManager autorunManager, KeyboardHookHandler keyboardHookHandler)
		{
			//
			// The InitializeComponent() call is required 
			// for Windows Forms designer support.
			//
			InitializeComponent();
			
			_autorunManager = autorunManager;
			_keyboardHookHandler = keyboardHookHandler;
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
			_keyboardHookHandler.Enabled = enabledToolStripMenuItem.Checked;
			UpdateIcon();
		}
		
		/// <summary>
		/// Handler for "Run at startup" item in the tray menu.
		/// Updates autorun state via AutorunManager.
		/// </summary>
		/// <param name="sender">Event sender, runAtStartupToolStripMenuItem.</param>
		/// <param name="e">Event arguments, unused.</param>
		private void RunAtStartupToolStripMenuItemClick(object sender, EventArgs e)
		{
			_autorunManager.AutorunEnabled = runAtStartupToolStripMenuItem.Checked;
		}
		
		/// <summary>
		/// Handler for "About Win-M++" item in the tray menu.
		/// Displays AboutForm instance from this class if not already shown.
		/// </summary>
		/// <param name="sender">Event sender, aboutWinMToolStripMenuItem.</param>
		/// <param name="e">Event arguments, unused.</param>
		private void AboutWinMToolStripMenuItemClick(object sender, EventArgs e)
		{
			// this probably should be moved to AboutForm itself
			// TODO: consider making AboutForm a Singleton
			if (_aboutForm.IsDisposed || _aboutForm == null)
			{
				_aboutForm = new AboutForm();
			}
			if (!_aboutForm.Visible)
			{
				_aboutForm.Show();
			}
			else
			{
				_aboutForm.BringToFront();
			}
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
		
		/// <summary>
		/// Handler for trayMenu's Opened event.
		/// Re-checks startup state to prevent bad behavior when registry key changes by third party.
		/// Also re-checks whether hook is enabled.
		/// </summary>
		/// <param name="sender">Event sender, trayMenu.</param>
		/// <param name="e">Event arguments, unused.</param>
		private void TrayMenuOpened(object sender, EventArgs e)
		{
			runAtStartupToolStripMenuItem.Checked = _autorunManager.AutorunEnabled;
			enabledToolStripMenuItem.Checked = _keyboardHookHandler.Enabled;
		}
		
		/// <summary>
		/// Sets the tray icon menu according to hook state.
		/// </summary>
		private void UpdateIcon()
		{
			trayIcon.Icon = (_keyboardHookHandler.Enabled)? Resources.tray_on: Resources.tray_off;
		}
		
		/// <summary>
		/// Tray icon double click handler.
		/// Toggles app on and off.
		/// </summary>
		/// <param name="sender">Event sender, trayIcon.</param>
		/// <param name="e">Event arguments, contains mouse button used to click.</param>
		private void TrayIconMouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				_keyboardHookHandler.Enabled = !_keyboardHookHandler.Enabled;
				UpdateIcon();
			}
		}
	}
}
