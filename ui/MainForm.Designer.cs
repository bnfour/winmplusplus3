
namespace winmplusplus3
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NotifyIcon trayIcon;
		private System.Windows.Forms.ContextMenuStrip trayMenu;
		private System.Windows.Forms.ToolStripMenuItem enabledToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem runAtStartupToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutWinMToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.label1 = new System.Windows.Forms.Label();
			this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.trayMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.enabledToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.runAtStartupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.aboutWinMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.trayMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(121, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Third time\'s the charm?";
			// 
			// trayIcon
			// 
			this.trayIcon.ContextMenuStrip = this.trayMenu;
			this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
			this.trayIcon.Text = "Win-M++";
			this.trayIcon.Visible = true;
			// 
			// trayMenu
			// 
			this.trayMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.enabledToolStripMenuItem,
			this.runAtStartupToolStripMenuItem,
			this.toolStripSeparator1,
			this.aboutWinMToolStripMenuItem,
			this.quitToolStripMenuItem});
			this.trayMenu.Name = "trayMenu";
			this.trayMenu.Size = new System.Drawing.Size(164, 98);
			// 
			// enabledToolStripMenuItem
			// 
			this.enabledToolStripMenuItem.Checked = true;
			this.enabledToolStripMenuItem.CheckOnClick = true;
			this.enabledToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.enabledToolStripMenuItem.Name = "enabledToolStripMenuItem";
			this.enabledToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
			this.enabledToolStripMenuItem.Text = "Enabled";
			this.enabledToolStripMenuItem.Click += new System.EventHandler(this.EnabledToolStripMenuItemClick);
			// 
			// runAtStartupToolStripMenuItem
			// 
			this.runAtStartupToolStripMenuItem.CheckOnClick = true;
			this.runAtStartupToolStripMenuItem.Name = "runAtStartupToolStripMenuItem";
			this.runAtStartupToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
			this.runAtStartupToolStripMenuItem.Text = "Run at startup";
			this.runAtStartupToolStripMenuItem.Click += new System.EventHandler(this.RunAtStartupToolStripMenuItemClick);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(160, 6);
			// 
			// aboutWinMToolStripMenuItem
			// 
			this.aboutWinMToolStripMenuItem.Name = "aboutWinMToolStripMenuItem";
			this.aboutWinMToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
			this.aboutWinMToolStripMenuItem.Text = "About Win-M++";
			this.aboutWinMToolStripMenuItem.Click += new System.EventHandler(this.AboutWinMToolStripMenuItemClick);
			// 
			// quitToolStripMenuItem
			// 
			this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
			this.quitToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
			this.quitToolStripMenuItem.Text = "Quit";
			this.quitToolStripMenuItem.Click += new System.EventHandler(this.QuitToolStripMenuItemClick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(142, 37);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Win-M++ v. 3";
			this.Shown += new System.EventHandler(this.MainFormShown);
			this.trayMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	}
}
