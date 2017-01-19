namespace HansKindberg.VisualStudio.Templating.Old.Forms
{
	partial class TestPropertiesForm
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
			this._integrationTestProjectNameLabel = new System.Windows.Forms.Label();
			this._integrationTestProjectNameTextBox = new System.Windows.Forms.TextBox();
			this._shimTestProjectNameLabel = new System.Windows.Forms.Label();
			this._shimTestProjectNameTextBox = new System.Windows.Forms.TextBox();
			this._unitTestProjectNameLabel = new System.Windows.Forms.Label();
			this._unitTestProjectNameTextBox = new System.Windows.Forms.TextBox();
			this._buttonPanel = new System.Windows.Forms.FlowLayoutPanel();
			this._cancelButton = new System.Windows.Forms.Button();
			this._okButton = new System.Windows.Forms.Button();
			this._inputPanel.SuspendLayout();
			this._buttonPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// _inputPanel
			// 
			this._inputPanel.Controls.Add(this._integrationTestProjectNameLabel);
			this._inputPanel.Controls.Add(this._integrationTestProjectNameTextBox);
			this._inputPanel.Controls.Add(this._shimTestProjectNameLabel);
			this._inputPanel.Controls.Add(this._shimTestProjectNameTextBox);
			this._inputPanel.Controls.Add(this._unitTestProjectNameLabel);
			this._inputPanel.Controls.Add(this._unitTestProjectNameTextBox);
			this._inputPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this._inputPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this._inputPanel.Location = new System.Drawing.Point(0, 0);
			this._inputPanel.Name = "_inputPanel";
			this._inputPanel.Padding = new System.Windows.Forms.Padding(15);
			this._inputPanel.Size = new System.Drawing.Size(584, 147);
			this._inputPanel.TabIndex = 0;
			// 
			// _integrationTestProjectNameLabel
			// 
			this._integrationTestProjectNameLabel.AutoSize = true;
			this._integrationTestProjectNameLabel.Location = new System.Drawing.Point(18, 15);
			this._integrationTestProjectNameLabel.Name = "_integrationTestProjectNameLabel";
			this._integrationTestProjectNameLabel.Size = new System.Drawing.Size(146, 13);
			this._integrationTestProjectNameLabel.TabIndex = 0;
			this._integrationTestProjectNameLabel.Text = "IntegrationTestProject-name";
			// 
			// _integrationTestProjectNameTextBox
			// 
			this._integrationTestProjectNameTextBox.Location = new System.Drawing.Point(18, 31);
			this._integrationTestProjectNameTextBox.Name = "_integrationTestProjectNameTextBox";
			this._integrationTestProjectNameTextBox.Size = new System.Drawing.Size(543, 20);
			this._integrationTestProjectNameTextBox.TabIndex = 1;
			// 
			// _shimTestProjectNameLabel
			// 
			this._shimTestProjectNameLabel.AutoSize = true;
			this._shimTestProjectNameLabel.Location = new System.Drawing.Point(18, 54);
			this._shimTestProjectNameLabel.Name = "_shimTestProjectNameLabel";
			this._shimTestProjectNameLabel.Size = new System.Drawing.Size(119, 13);
			this._shimTestProjectNameLabel.TabIndex = 2;
			this._shimTestProjectNameLabel.Text = "ShimTestProject-name";
			// 
			// _shimTestProjectNameTextBox
			// 
			this._shimTestProjectNameTextBox.Location = new System.Drawing.Point(18, 70);
			this._shimTestProjectNameTextBox.Name = "_shimTestProjectNameTextBox";
			this._shimTestProjectNameTextBox.Size = new System.Drawing.Size(543, 20);
			this._shimTestProjectNameTextBox.TabIndex = 3;
			// 
			// _unitTestProjectNameLabel
			// 
			this._unitTestProjectNameLabel.AutoSize = true;
			this._unitTestProjectNameLabel.Location = new System.Drawing.Point(18, 93);
			this._unitTestProjectNameLabel.Name = "_unitTestProjectNameLabel";
			this._unitTestProjectNameLabel.Size = new System.Drawing.Size(115, 13);
			this._unitTestProjectNameLabel.TabIndex = 4;
			this._unitTestProjectNameLabel.Text = "UnitTestProject-name";
			// 
			// _unitTestProjectNameTextBox
			// 
			this._unitTestProjectNameTextBox.Location = new System.Drawing.Point(18, 109);
			this._unitTestProjectNameTextBox.Name = "_unitTestProjectNameTextBox";
			this._unitTestProjectNameTextBox.Size = new System.Drawing.Size(543, 20);
			this._unitTestProjectNameTextBox.TabIndex = 5;
			// 
			// _buttonPanel
			// 
			this._buttonPanel.Controls.Add(this._cancelButton);
			this._buttonPanel.Controls.Add(this._okButton);
			this._buttonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this._buttonPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this._buttonPanel.Location = new System.Drawing.Point(0, 146);
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
			// TestPropertiesForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 197);
			this.Controls.Add(this._buttonPanel);
			this.Controls.Add(this._inputPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "TestPropertiesForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Test-properties form";
			this._inputPanel.ResumeLayout(false);
			this._inputPanel.PerformLayout();
			this._buttonPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel _inputPanel;
		private System.Windows.Forms.FlowLayoutPanel _buttonPanel;
		private System.Windows.Forms.Label _integrationTestProjectNameLabel;
		private System.Windows.Forms.TextBox _integrationTestProjectNameTextBox;
		private System.Windows.Forms.Label _shimTestProjectNameLabel;
		private System.Windows.Forms.TextBox _shimTestProjectNameTextBox;
		private System.Windows.Forms.Label _unitTestProjectNameLabel;
		private System.Windows.Forms.TextBox _unitTestProjectNameTextBox;
		private System.Windows.Forms.Button _okButton;
		private System.Windows.Forms.Button _cancelButton;
	}
}