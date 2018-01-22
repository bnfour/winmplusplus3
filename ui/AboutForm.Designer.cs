
namespace winmplusplus3
{
	partial class AboutForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.PictureBox pictureBox;
		private System.Windows.Forms.Label titleLabel;
		private System.Windows.Forms.Label versionLabel;
		private System.Windows.Forms.LinkLabel linkLabel;
		private System.Windows.Forms.Label labelInfo;
		
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
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.titleLabel = new System.Windows.Forms.Label();
			this.versionLabel = new System.Windows.Forms.Label();
			this.linkLabel = new System.Windows.Forms.LinkLabel();
			this.labelInfo = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox
			// 
			this.pictureBox.Image = global::winmplusplus3.Resources.about_logo;
			this.pictureBox.InitialImage = null;
			this.pictureBox.Location = new System.Drawing.Point(14, 12);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(65, 48);
			this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBox.TabIndex = 0;
			this.pictureBox.TabStop = false;
			// 
			// titleLabel
			// 
			this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.titleLabel.Location = new System.Drawing.Point(85, 12);
			this.titleLabel.Name = "titleLabel";
			this.titleLabel.Size = new System.Drawing.Size(133, 32);
			this.titleLabel.TabIndex = 1;
			this.titleLabel.Text = "Win-M++";
			this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// versionLabel
			// 
			this.versionLabel.Location = new System.Drawing.Point(85, 47);
			this.versionLabel.Name = "versionLabel";
			this.versionLabel.Size = new System.Drawing.Size(133, 16);
			this.versionLabel.TabIndex = 2;
			this.versionLabel.Text = "version will load here";
			this.versionLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// linkLabel
			// 
			this.linkLabel.AutoSize = true;
			this.linkLabel.Location = new System.Drawing.Point(14, 63);
			this.linkLabel.Name = "linkLabel";
			this.linkLabel.Size = new System.Drawing.Size(203, 13);
			this.linkLabel.TabIndex = 3;
			this.linkLabel.TabStop = true;
			this.linkLabel.Text = "https://github.com/bnfour/winmplusplus3";
			this.linkLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelLinkClicked);
			// 
			// labelInfo
			// 
			this.labelInfo.AutoSize = true;
			this.labelInfo.Location = new System.Drawing.Point(108, 76);
			this.labelInfo.Name = "labelInfo";
			this.labelInfo.Size = new System.Drawing.Size(110, 13);
			this.labelInfo.TabIndex = 4;
			this.labelInfo.Text = "by Bn4, January 2018";
			this.labelInfo.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// AboutForm
			// 
			this.ClientSize = new System.Drawing.Size(229, 94);
			this.Controls.Add(this.labelInfo);
			this.Controls.Add(this.linkLabel);
			this.Controls.Add(this.versionLabel);
			this.Controls.Add(this.titleLabel);
			this.Controls.Add(this.pictureBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = global::winmplusplus3.Resources.app;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutForm";
			this.ShowIcon = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "About Win-M++";
			this.Shown += new System.EventHandler(this.AboutFormShown);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
