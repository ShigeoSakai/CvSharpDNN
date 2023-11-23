namespace CVSharpDNN.DetectionDefine
{
	partial class DetectionDefineControl
	{
		/// <summary> 
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region コンポーネント デザイナーで生成されたコード

		/// <summary> 
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.CbDetectionKind = new System.Windows.Forms.ComboBox();
			this.MyContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ToolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.TbDescription = new System.Windows.Forms.TextBox();
			this.CbNetworkSize = new System.Windows.Forms.ComboBox();
			this.TbName = new System.Windows.Forms.TextBox();
			this.TbConfigFile = new System.Windows.Forms.TextBox();
			this.BtConfigFileSelect = new System.Windows.Forms.Button();
			this.BtModelFileSelect = new System.Windows.Forms.Button();
			this.TbModelFile = new System.Windows.Forms.TextBox();
			this.CbFramefork = new System.Windows.Forms.ComboBox();
			this.FileToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.MyContextMenuStrip.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// CbDetectionKind
			// 
			this.CbDetectionKind.ContextMenuStrip = this.MyContextMenuStrip;
			this.CbDetectionKind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CbDetectionKind.FormattingEnabled = true;
			this.CbDetectionKind.Location = new System.Drawing.Point(191, 31);
			this.CbDetectionKind.Name = "CbDetectionKind";
			this.CbDetectionKind.Size = new System.Drawing.Size(128, 20);
			this.CbDetectionKind.TabIndex = 0;
			this.FileToolTip.SetToolTip(this.CbDetectionKind, "推論モデルを選択");
			this.CbDetectionKind.SelectionChangeCommitted += new System.EventHandler(this.CbDetectionKind_SelectionChangeCommitted);
			// 
			// MyContextMenuStrip
			// 
			this.MyContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemDelete});
			this.MyContextMenuStrip.Name = "MyContextMenuStrip";
			this.MyContextMenuStrip.Size = new System.Drawing.Size(115, 26);
			// 
			// ToolStripMenuItemDelete
			// 
			this.ToolStripMenuItemDelete.Name = "ToolStripMenuItemDelete";
			this.ToolStripMenuItemDelete.Size = new System.Drawing.Size(114, 22);
			this.ToolStripMenuItemDelete.Text = "削除(&D)";
			this.ToolStripMenuItemDelete.Click += new System.EventHandler(this.ToolStripMenuItemDelete_Click);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 6;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ContextMenuStrip = this.MyContextMenuStrip;
			this.tableLayoutPanel1.Controls.Add(this.TbDescription, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.CbNetworkSize, 5, 0);
			this.tableLayoutPanel1.Controls.Add(this.TbName, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.TbConfigFile, 3, 1);
			this.tableLayoutPanel1.Controls.Add(this.BtConfigFileSelect, 4, 1);
			this.tableLayoutPanel1.Controls.Add(this.BtModelFileSelect, 4, 0);
			this.tableLayoutPanel1.Controls.Add(this.TbModelFile, 3, 0);
			this.tableLayoutPanel1.Controls.Add(this.CbFramefork, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.CbDetectionKind, 1, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(578, 55);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// TbDescription
			// 
			this.TbDescription.ContextMenuStrip = this.MyContextMenuStrip;
			this.TbDescription.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbDescription.Location = new System.Drawing.Point(3, 33);
			this.TbDescription.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
			this.TbDescription.Name = "TbDescription";
			this.TbDescription.Size = new System.Drawing.Size(182, 19);
			this.TbDescription.TabIndex = 7;
			this.FileToolTip.SetToolTip(this.TbDescription, "推論モデルの説明");
			// 
			// CbNetworkSize
			// 
			this.CbNetworkSize.ContextMenuStrip = this.MyContextMenuStrip;
			this.CbNetworkSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CbNetworkSize.FormattingEnabled = true;
			this.CbNetworkSize.Location = new System.Drawing.Point(495, 3);
			this.CbNetworkSize.Name = "CbNetworkSize";
			this.CbNetworkSize.Size = new System.Drawing.Size(80, 20);
			this.CbNetworkSize.TabIndex = 4;
			this.FileToolTip.SetToolTip(this.CbNetworkSize, "ネットワーク入力サイズ");
			this.CbNetworkSize.SelectionChangeCommitted += new System.EventHandler(this.CbNetworkSize_SelectionChangeCommitted);
			// 
			// TbName
			// 
			this.TbName.ContextMenuStrip = this.MyContextMenuStrip;
			this.TbName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbName.Location = new System.Drawing.Point(3, 5);
			this.TbName.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
			this.TbName.Name = "TbName";
			this.TbName.Size = new System.Drawing.Size(182, 19);
			this.TbName.TabIndex = 5;
			this.FileToolTip.SetToolTip(this.TbName, "推論モデルの識別名");
			// 
			// TbConfigFile
			// 
			this.TbConfigFile.ContextMenuStrip = this.MyContextMenuStrip;
			this.TbConfigFile.Location = new System.Drawing.Point(325, 33);
			this.TbConfigFile.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
			this.TbConfigFile.Name = "TbConfigFile";
			this.TbConfigFile.ReadOnly = true;
			this.TbConfigFile.Size = new System.Drawing.Size(128, 19);
			this.TbConfigFile.TabIndex = 2;
			// 
			// BtConfigFileSelect
			// 
			this.BtConfigFileSelect.ContextMenuStrip = this.MyContextMenuStrip;
			this.BtConfigFileSelect.Location = new System.Drawing.Point(459, 30);
			this.BtConfigFileSelect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 3);
			this.BtConfigFileSelect.Name = "BtConfigFileSelect";
			this.BtConfigFileSelect.Size = new System.Drawing.Size(30, 23);
			this.BtConfigFileSelect.TabIndex = 3;
			this.BtConfigFileSelect.Text = "...";
			this.FileToolTip.SetToolTip(this.BtConfigFileSelect, "Configファイルを選択");
			this.BtConfigFileSelect.UseVisualStyleBackColor = true;
			this.BtConfigFileSelect.Click += new System.EventHandler(this.BtConfigFileSelect_Click);
			// 
			// BtModelFileSelect
			// 
			this.BtModelFileSelect.ContextMenuStrip = this.MyContextMenuStrip;
			this.BtModelFileSelect.Location = new System.Drawing.Point(459, 2);
			this.BtModelFileSelect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 3);
			this.BtModelFileSelect.Name = "BtModelFileSelect";
			this.BtModelFileSelect.Size = new System.Drawing.Size(30, 23);
			this.BtModelFileSelect.TabIndex = 1;
			this.BtModelFileSelect.Text = "...";
			this.FileToolTip.SetToolTip(this.BtModelFileSelect, "モデルファイルを選択");
			this.BtModelFileSelect.UseVisualStyleBackColor = true;
			this.BtModelFileSelect.Click += new System.EventHandler(this.BtModelFileSelect_Click);
			// 
			// TbModelFile
			// 
			this.TbModelFile.ContextMenuStrip = this.MyContextMenuStrip;
			this.TbModelFile.Location = new System.Drawing.Point(325, 5);
			this.TbModelFile.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
			this.TbModelFile.Name = "TbModelFile";
			this.TbModelFile.ReadOnly = true;
			this.TbModelFile.Size = new System.Drawing.Size(128, 19);
			this.TbModelFile.TabIndex = 0;
			// 
			// CbFramefork
			// 
			this.CbFramefork.ContextMenuStrip = this.MyContextMenuStrip;
			this.CbFramefork.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CbFramefork.FormattingEnabled = true;
			this.CbFramefork.Location = new System.Drawing.Point(191, 3);
			this.CbFramefork.Name = "CbFramefork";
			this.CbFramefork.Size = new System.Drawing.Size(128, 20);
			this.CbFramefork.TabIndex = 6;
			this.FileToolTip.SetToolTip(this.CbFramefork, "フレームワークを選択");
			this.CbFramefork.SelectionChangeCommitted += new System.EventHandler(this.CbFramefork_SelectionChangeCommitted);
			// 
			// FileToolTip
			// 
			this.FileToolTip.AutomaticDelay = 100;
			this.FileToolTip.AutoPopDelay = 5000;
			this.FileToolTip.InitialDelay = 100;
			this.FileToolTip.IsBalloon = true;
			this.FileToolTip.ReshowDelay = 20;
			// 
			// DetectionDefineControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.tableLayoutPanel1);
			this.MinimumSize = new System.Drawing.Size(580, 57);
			this.Name = "DetectionDefineControl";
			this.Size = new System.Drawing.Size(578, 55);
			this.MyContextMenuStrip.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ComboBox CbDetectionKind;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TextBox TbModelFile;
		private System.Windows.Forms.Button BtModelFileSelect;
		private System.Windows.Forms.TextBox TbConfigFile;
		private System.Windows.Forms.Button BtConfigFileSelect;
		private System.Windows.Forms.ComboBox CbNetworkSize;
		private System.Windows.Forms.TextBox TbName;
		private System.Windows.Forms.ComboBox CbFramefork;
		private System.Windows.Forms.TextBox TbDescription;
		private System.Windows.Forms.ContextMenuStrip MyContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDelete;
		private System.Windows.Forms.ToolTip FileToolTip;
	}
}
