namespace CVSharpDNN.DetectionDefine
{
	partial class DetectionDefineForm
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
			if (disposing && (components != null))
			{
				components.Dispose();
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
			this.label1 = new System.Windows.Forms.Label();
			this.TbBaseFolder = new System.Windows.Forms.TextBox();
			this.BtBaseFolderSelect = new System.Windows.Forms.Button();
			this.FLPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.BtClose = new System.Windows.Forms.Button();
			this.BtSave = new System.Windows.Forms.Button();
			this.BtAdd = new System.Windows.Forms.Button();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Location = new System.Drawing.Point(3, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(74, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "基準フォルダ:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// TbBaseFolder
			// 
			this.TbBaseFolder.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbBaseFolder.Location = new System.Drawing.Point(83, 11);
			this.TbBaseFolder.Name = "TbBaseFolder";
			this.TbBaseFolder.Size = new System.Drawing.Size(439, 19);
			this.TbBaseFolder.TabIndex = 1;
			// 
			// BtBaseFolderSelect
			// 
			this.BtBaseFolderSelect.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BtBaseFolderSelect.Location = new System.Drawing.Point(525, 8);
			this.BtBaseFolderSelect.Margin = new System.Windows.Forms.Padding(0);
			this.BtBaseFolderSelect.Name = "BtBaseFolderSelect";
			this.BtBaseFolderSelect.Size = new System.Drawing.Size(28, 24);
			this.BtBaseFolderSelect.TabIndex = 2;
			this.BtBaseFolderSelect.Text = "...";
			this.BtBaseFolderSelect.UseVisualStyleBackColor = true;
			this.BtBaseFolderSelect.Click += new System.EventHandler(this.BtBaseFolderSelect_Click);
			// 
			// FLPanel
			// 
			this.FLPanel.AutoScroll = true;
			this.FLPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.tableLayoutPanel1.SetColumnSpan(this.FLPanel, 5);
			this.FLPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.FLPanel.Location = new System.Drawing.Point(3, 35);
			this.FLPanel.Name = "FLPanel";
			this.FLPanel.Size = new System.Drawing.Size(607, 233);
			this.FLPanel.TabIndex = 3;
			// 
			// BtClose
			// 
			this.BtClose.Dock = System.Windows.Forms.DockStyle.Right;
			this.BtClose.Location = new System.Drawing.Point(370, 0);
			this.BtClose.Name = "BtClose";
			this.BtClose.Size = new System.Drawing.Size(75, 32);
			this.BtClose.TabIndex = 4;
			this.BtClose.Text = "閉じる";
			this.BtClose.UseVisualStyleBackColor = true;
			this.BtClose.Click += new System.EventHandler(this.BtClose_Click);
			// 
			// BtSave
			// 
			this.BtSave.Dock = System.Windows.Forms.DockStyle.Left;
			this.BtSave.Location = new System.Drawing.Point(0, 0);
			this.BtSave.Margin = new System.Windows.Forms.Padding(3, 3, 3, 5);
			this.BtSave.Name = "BtSave";
			this.BtSave.Size = new System.Drawing.Size(75, 32);
			this.BtSave.TabIndex = 5;
			this.BtSave.Text = "保存";
			this.BtSave.UseVisualStyleBackColor = true;
			this.BtSave.Click += new System.EventHandler(this.BtSave_Click);
			// 
			// BtAdd
			// 
			this.BtAdd.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BtAdd.Location = new System.Drawing.Point(585, 8);
			this.BtAdd.Margin = new System.Windows.Forms.Padding(0);
			this.BtAdd.Name = "BtAdd";
			this.BtAdd.Size = new System.Drawing.Size(28, 24);
			this.BtAdd.TabIndex = 6;
			this.BtAdd.Text = "＋";
			this.BtAdd.UseVisualStyleBackColor = true;
			this.BtAdd.Click += new System.EventHandler(this.BtAdd_Click);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 5;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 28F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 28F));
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.BtAdd, 4, 1);
			this.tableLayoutPanel1.Controls.Add(this.TbBaseFolder, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.FLPanel, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.BtBaseFolderSelect, 2, 1);
			this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 3);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 5;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(613, 311);
			this.tableLayoutPanel1.TabIndex = 7;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.BtSave);
			this.panel1.Controls.Add(this.BtClose);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(80, 271);
			this.panel1.Margin = new System.Windows.Forms.Padding(0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(445, 32);
			this.panel1.TabIndex = 7;
			// 
			// DetectionDefineForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(613, 311);
			this.Controls.Add(this.tableLayoutPanel1);
			this.MinimumSize = new System.Drawing.Size(621, 240);
			this.Name = "DetectionDefineForm";
			this.Text = "DetectionDefineForm";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox TbBaseFolder;
		private System.Windows.Forms.Button BtBaseFolderSelect;
		private System.Windows.Forms.FlowLayoutPanel FLPanel;
		private System.Windows.Forms.Button BtClose;
		private System.Windows.Forms.Button BtSave;
		private System.Windows.Forms.Button BtAdd;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Panel panel1;
	}
}