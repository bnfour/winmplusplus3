using System;
using System.Windows.Forms;

using Process = System.Diagnostics.Process;

namespace winmplusplus3.UI
{
	/// <summary>
	/// Description of AboutForm.
	/// </summary>
	public partial class AboutForm : Form
	{
		public AboutForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			versionLabel.Text = "version " + Application.ProductVersion;
		}
		
		/// <summary>
		/// linkLabel Click event handler.
		/// Opens project's github repo from link.
		/// </summary>
		/// <param name="sender">Event sender, linkLabel.</param>
		/// <param name="e">Event arguments, unused.</param>
		private void LinkLabelLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
			=> Process.Start(linkLabel.Text);
	}
}
