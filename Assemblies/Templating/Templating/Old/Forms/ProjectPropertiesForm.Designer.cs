namespace HansKindberg.VisualStudio.Templating.Old.Forms
{
	partial class ProjectPropertiesForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this._inputPanel = new System.Windows.Forms.FlowLayoutPanel();
			this._assemblyNameLabel = new System.Windows.Forms.Label();
			this._assemblyNameTextBox = new System.Windows.Forms.TextBox();
			this._rootNamespaceLabel = new System.Windows.Forms.Label();
			this._rootNamespaceTextBox = new System.Windows.Forms.TextBox();
			this._buttonPanel = new System.Windows.Forms.FlowLayoutPanel();
			this._cancelButton = new System.Windows.Forms.Button();
			this._okButton = new System.Windows.Forms.Button();
			this._inputPanel.SuspendLayout();
			this._buttonPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// _inputPanel
			// 
			this._inputPanel.Controls.Add(this._assemblyNameLabel);
			this._inputPanel.Controls.Add(this._assemblyNameTextBox);
			this._inputPanel.Controls.Add(this._rootNamespaceLabel);
			this._inputPanel.Controls.Add(this._rootNamespaceTextBox);
			this._inputPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this._inputPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this._inputPanel.Location = new System.Drawing.Point(0, 0);
			this._inputPanel.Name = "_inputPanel";
			this._inputPanel.Padding = new System.Windows.Forms.Padding(15);
			this._inputPanel.Size = new System.Drawing.Size(584, 108);
			this._inputPanel.TabIndex = 0;
			// 
			// _assemblyNameLabel
			// 
			this._assemblyNameLabel.AutoSize = true;
			this._assemblyNameLabel.Location = new System.Drawing.Point(18, 15);
			this._assemblyNameLabel.Name = "_assemblyNameLabel";
			this._assemblyNameLabel.Size = new System.Drawing.Size(80, 13);
			this._assemblyNameLabel.TabIndex = 0;
			this._assemblyNameLabel.Text = "Assembly-name";
			// 
			// _assemblyNameTextBox
			// 
			this._assemblyNameTextBox.Location = new System.Drawing.Point(18, 31);
			this._assemblyNameTextBox.Name = "_assemblyNameTextBox";
			this._assemblyNameTextBox.Size = new System.Drawing.Size(543, 20);
			this._assemblyNameTextBox.TabIndex = 1;
			// 
			// _rootNamespaceLabel
			// 
			this._rootNamespaceLabel.AutoSize = true;
			this._rootNamespaceLabel.Location = new System.Drawing.Point(18, 54);
			this._rootNamespaceLabel.Name = "_rootNamespaceLabel";
			this._rootNamespaceLabel.Size = new System.Drawing.Size(88, 13);
			this._rootNamespaceLabel.TabIndex = 2;
			this._rootNamespaceLabel.Text = "Root-namespace";
			// 
			// _rootNamespaceTextBox
			// 
			this._rootNamespaceTextBox.Location = new System.Drawing.Point(18, 70);
			this._rootNamespaceTextBox.Name = "_rootNamespaceTextBox";
			this._rootNamespaceTextBox.Size = new System.Drawing.Size(543, 20);
			this._rootNamespaceTextBox.TabIndex = 3;
			// 
			// _buttonPanel
			// 
			this._buttonPanel.Controls.Add(this._cancelButton);
			this._buttonPanel.Controls.Add(this._okButton);
			this._buttonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this._buttonPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this._buttonPanel.Location = new System.Drawing.Point(0, 106);
			this._buttonPanel.Name = "_buttonPanel";
			this._buttonPanel.Padding = new System.Windows.Forms.Padding(10, 5, 10, 10);
			this._buttonPanel.Size = new System.Drawing.Size(584, 51);
			this._buttonPanel.TabIndex = 1;
			// 
			// _cancelButton
			// 
			this._cancelButton.Location = new System.Drawing.Point(486, 8);
			this._cancelButton.Name = "_cancelButton";
			this._cancelButton.Size = new System.Drawing.Size(75, 25);
			this._cancelButton.TabIndex = 1;
			this._cancelButton.Text = "Cancel";
			this._cancelButton.UseVisualStyleBackColor = true;
			// 
			// _okButton
			// 
			this._okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this._okButton.Location = new System.Drawing.Point(405, 8);
			this._okButton.Name = "_okButton";
			this._okButton.Size = new System.Drawing.Size(75, 25);
			this._okButton.TabIndex = 0;
			this._okButton.Text = "OK";
			this._okButton.UseVisualStyleBackColor = true;
			// 
			// ProjectPropertiesForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 157);
			this.Controls.Add(this._buttonPanel);
			this.Controls.Add(this._inputPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ProjectPropertiesForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Project-properties form";
			this._inputPanel.ResumeLayout(false);
			this._inputPanel.PerformLayout();
			this._buttonPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel _inputPanel;
		private System.Windows.Forms.FlowLayoutPanel _buttonPanel;
		private System.Windows.Forms.Label _assemblyNameLabel;
		private System.Windows.Forms.TextBox _assemblyNameTextBox;
		private System.Windows.Forms.Label _rootNamespaceLabel;
		private System.Windows.Forms.TextBox _rootNamespaceTextBox;
		private System.Windows.Forms.Button _okButton;
		private System.Windows.Forms.Button _cancelButton;
	}
}