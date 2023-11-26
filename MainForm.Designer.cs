namespace CVSharpDNN
{
	partial class MainForm
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

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			Drawing.ZoomPictureBox.ShowInfoItem showInfoItem1 = new Drawing.ZoomPictureBox.ShowInfoItem();
			this.MainTableLayout = new System.Windows.Forms.TableLayoutPanel();
			this.CtrlPanel = new System.Windows.Forms.Panel();
			this.ModelGroup = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.CbDetectionDef = new System.Windows.Forms.ComboBox();
			this.CbBackendAndtarget = new System.Windows.Forms.ComboBox();
			this.BtEditModel = new System.Windows.Forms.Button();
			this.CbClass = new System.Windows.Forms.ComboBox();
			this.label10 = new System.Windows.Forms.Label();
			this.BtDetectionLoad = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.BtImageFolderOpen = new System.Windows.Forms.Button();
			this.BtImageFolderSelect = new System.Windows.Forms.Button();
			this.TbImageFolder = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.LbImageFile = new System.Windows.Forms.ListBox();
			this.ImagePictureBox = new Drawing.ZoomPictureBox();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.DetectionGroup = new System.Windows.Forms.GroupBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.CbResultDraw = new System.Windows.Forms.CheckBox();
			this.CbReloadModel = new System.Windows.Forms.CheckBox();
			this.TbRepeatNum = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.BtExecAll = new System.Windows.Forms.Button();
			this.BtDetectionExec = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.TbConfidence = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.TbNms = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.TbResult = new System.Windows.Forms.TextBox();
			this.AppMenuStrip = new System.Windows.Forms.MenuStrip();
			this.ToolStripMenuItemModelDefine = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItemReadModel = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.ToolStripMenuItemSaveModel = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItemAbount = new System.Windows.Forms.ToolStripMenuItem();
			this.MainTableLayout.SuspendLayout();
			this.CtrlPanel.SuspendLayout();
			this.ModelGroup.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ImagePictureBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.DetectionGroup.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.AppMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// MainTableLayout
			// 
			this.MainTableLayout.ColumnCount = 3;
			this.MainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 256F));
			this.MainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.MainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
			this.MainTableLayout.Controls.Add(this.CtrlPanel, 0, 0);
			this.MainTableLayout.Controls.Add(this.LbImageFile, 0, 1);
			this.MainTableLayout.Controls.Add(this.ImagePictureBox, 1, 1);
			this.MainTableLayout.Controls.Add(this.splitContainer1, 2, 1);
			this.MainTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainTableLayout.Location = new System.Drawing.Point(0, 24);
			this.MainTableLayout.Name = "MainTableLayout";
			this.MainTableLayout.RowCount = 2;
			this.MainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 112F));
			this.MainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.MainTableLayout.Size = new System.Drawing.Size(902, 585);
			this.MainTableLayout.TabIndex = 0;
			// 
			// CtrlPanel
			// 
			this.MainTableLayout.SetColumnSpan(this.CtrlPanel, 3);
			this.CtrlPanel.Controls.Add(this.ModelGroup);
			this.CtrlPanel.Controls.Add(this.BtImageFolderOpen);
			this.CtrlPanel.Controls.Add(this.BtImageFolderSelect);
			this.CtrlPanel.Controls.Add(this.TbImageFolder);
			this.CtrlPanel.Controls.Add(this.label1);
			this.CtrlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CtrlPanel.Location = new System.Drawing.Point(0, 0);
			this.CtrlPanel.Margin = new System.Windows.Forms.Padding(0);
			this.CtrlPanel.Name = "CtrlPanel";
			this.CtrlPanel.Size = new System.Drawing.Size(902, 112);
			this.CtrlPanel.TabIndex = 0;
			// 
			// ModelGroup
			// 
			this.ModelGroup.Controls.Add(this.label2);
			this.ModelGroup.Controls.Add(this.CbDetectionDef);
			this.ModelGroup.Controls.Add(this.CbBackendAndtarget);
			this.ModelGroup.Controls.Add(this.BtEditModel);
			this.ModelGroup.Controls.Add(this.CbClass);
			this.ModelGroup.Controls.Add(this.label10);
			this.ModelGroup.Controls.Add(this.BtDetectionLoad);
			this.ModelGroup.Controls.Add(this.label4);
			this.ModelGroup.Location = new System.Drawing.Point(7, 6);
			this.ModelGroup.Name = "ModelGroup";
			this.ModelGroup.Size = new System.Drawing.Size(889, 75);
			this.ModelGroup.TabIndex = 24;
			this.ModelGroup.TabStop = false;
			this.ModelGroup.Text = "モデル";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(53, 45);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(127, 23);
			this.label2.TabIndex = 27;
			this.label2.Text = "バックエンドとターゲット:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// CbDetectionDef
			// 
			this.CbDetectionDef.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CbDetectionDef.FormattingEnabled = true;
			this.CbDetectionDef.Location = new System.Drawing.Point(73, 21);
			this.CbDetectionDef.Name = "CbDetectionDef";
			this.CbDetectionDef.Size = new System.Drawing.Size(412, 20);
			this.CbDetectionDef.TabIndex = 26;
			// 
			// CbBackendAndtarget
			// 
			this.CbBackendAndtarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CbBackendAndtarget.FormattingEnabled = true;
			this.CbBackendAndtarget.Location = new System.Drawing.Point(186, 47);
			this.CbBackendAndtarget.Name = "CbBackendAndtarget";
			this.CbBackendAndtarget.Size = new System.Drawing.Size(299, 20);
			this.CbBackendAndtarget.TabIndex = 24;
			this.CbBackendAndtarget.SelectionChangeCommitted += new System.EventHandler(this.CbBackendAndtarget_SelectionChangeCommitted);
			// 
			// BtEditModel
			// 
			this.BtEditModel.Location = new System.Drawing.Point(779, 13);
			this.BtEditModel.Name = "BtEditModel";
			this.BtEditModel.Size = new System.Drawing.Size(104, 34);
			this.BtEditModel.TabIndex = 22;
			this.BtEditModel.Text = "モデル定義編集";
			this.BtEditModel.UseVisualStyleBackColor = true;
			this.BtEditModel.Click += new System.EventHandler(this.BtEditModel_Click);
			// 
			// CbClass
			// 
			this.CbClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CbClass.FormattingEnabled = true;
			this.CbClass.Location = new System.Drawing.Point(563, 21);
			this.CbClass.Name = "CbClass";
			this.CbClass.Size = new System.Drawing.Size(99, 20);
			this.CbClass.TabIndex = 2;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(491, 19);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(66, 23);
			this.label10.TabIndex = 21;
			this.label10.Text = "クラス定義:";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// BtDetectionLoad
			// 
			this.BtDetectionLoad.Location = new System.Drawing.Point(668, 13);
			this.BtDetectionLoad.Name = "BtDetectionLoad";
			this.BtDetectionLoad.Size = new System.Drawing.Size(75, 34);
			this.BtDetectionLoad.TabIndex = 8;
			this.BtDetectionLoad.Text = "読み込み";
			this.BtDetectionLoad.UseVisualStyleBackColor = true;
			this.BtDetectionLoad.Click += new System.EventHandler(this.BtDetectionLoad_Click);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(-4, 19);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(73, 23);
			this.label4.TabIndex = 8;
			this.label4.Text = "モデル定義:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// BtImageFolderOpen
			// 
			this.BtImageFolderOpen.Location = new System.Drawing.Point(805, 84);
			this.BtImageFolderOpen.Name = "BtImageFolderOpen";
			this.BtImageFolderOpen.Size = new System.Drawing.Size(75, 23);
			this.BtImageFolderOpen.TabIndex = 2;
			this.BtImageFolderOpen.Text = "開く";
			this.BtImageFolderOpen.UseVisualStyleBackColor = true;
			this.BtImageFolderOpen.Click += new System.EventHandler(this.BtImageFolderOpen_Click);
			// 
			// BtImageFolderSelect
			// 
			this.BtImageFolderSelect.Location = new System.Drawing.Point(763, 84);
			this.BtImageFolderSelect.Name = "BtImageFolderSelect";
			this.BtImageFolderSelect.Size = new System.Drawing.Size(36, 23);
			this.BtImageFolderSelect.TabIndex = 1;
			this.BtImageFolderSelect.Text = "...";
			this.BtImageFolderSelect.UseVisualStyleBackColor = true;
			this.BtImageFolderSelect.Click += new System.EventHandler(this.BtImageFolderSelect_Click);
			// 
			// TbImageFolder
			// 
			this.TbImageFolder.Location = new System.Drawing.Point(96, 86);
			this.TbImageFolder.Name = "TbImageFolder";
			this.TbImageFolder.Size = new System.Drawing.Size(661, 19);
			this.TbImageFolder.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(5, 84);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(85, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "画像フォルダ:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// LbImageFile
			// 
			this.LbImageFile.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LbImageFile.FormattingEnabled = true;
			this.LbImageFile.ItemHeight = 12;
			this.LbImageFile.Location = new System.Drawing.Point(3, 115);
			this.LbImageFile.Name = "LbImageFile";
			this.LbImageFile.Size = new System.Drawing.Size(250, 467);
			this.LbImageFile.TabIndex = 0;
			this.LbImageFile.SelectedIndexChanged += new System.EventHandler(this.LbImageFile_SelectedIndexChanged);
			// 
			// ImagePictureBox
			// 
			this.ImagePictureBox.AutoRefresh = true;
			this.ImagePictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ImagePictureBox.Image = null;
			this.ImagePictureBox.InfoItem = showInfoItem1;
			this.ImagePictureBox.Location = new System.Drawing.Point(259, 115);
			this.ImagePictureBox.Name = "ImagePictureBox";
			this.ImagePictureBox.Size = new System.Drawing.Size(420, 467);
			this.ImagePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.ImagePictureBox.TabIndex = 2;
			this.ImagePictureBox.TabStop = false;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.IsSplitterFixed = true;
			this.splitContainer1.Location = new System.Drawing.Point(685, 115);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.DetectionGroup);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.TbResult);
			this.splitContainer1.Size = new System.Drawing.Size(214, 467);
			this.splitContainer1.SplitterDistance = 160;
			this.splitContainer1.SplitterWidth = 2;
			this.splitContainer1.TabIndex = 3;
			// 
			// DetectionGroup
			// 
			this.DetectionGroup.Controls.Add(this.groupBox1);
			this.DetectionGroup.Controls.Add(this.BtDetectionExec);
			this.DetectionGroup.Controls.Add(this.label6);
			this.DetectionGroup.Controls.Add(this.TbConfidence);
			this.DetectionGroup.Controls.Add(this.label8);
			this.DetectionGroup.Controls.Add(this.label7);
			this.DetectionGroup.Controls.Add(this.TbNms);
			this.DetectionGroup.Controls.Add(this.label9);
			this.DetectionGroup.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DetectionGroup.Location = new System.Drawing.Point(0, 0);
			this.DetectionGroup.Name = "DetectionGroup";
			this.DetectionGroup.Size = new System.Drawing.Size(214, 160);
			this.DetectionGroup.TabIndex = 25;
			this.DetectionGroup.TabStop = false;
			this.DetectionGroup.Text = "推論";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.CbResultDraw);
			this.groupBox1.Controls.Add(this.CbReloadModel);
			this.groupBox1.Controls.Add(this.TbRepeatNum);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.BtExecAll);
			this.groupBox1.Location = new System.Drawing.Point(8, 81);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(197, 73);
			this.groupBox1.TabIndex = 19;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "一括実行";
			// 
			// CbResultDraw
			// 
			this.CbResultDraw.Location = new System.Drawing.Point(129, 46);
			this.CbResultDraw.Name = "CbResultDraw";
			this.CbResultDraw.Size = new System.Drawing.Size(58, 16);
			this.CbResultDraw.TabIndex = 4;
			this.CbResultDraw.Text = "描画";
			this.CbResultDraw.UseVisualStyleBackColor = true;
			// 
			// CbReloadModel
			// 
			this.CbReloadModel.AutoSize = true;
			this.CbReloadModel.Location = new System.Drawing.Point(9, 46);
			this.CbReloadModel.Name = "CbReloadModel";
			this.CbReloadModel.Size = new System.Drawing.Size(111, 16);
			this.CbReloadModel.TabIndex = 3;
			this.CbReloadModel.Text = "モデル再読み込み";
			this.CbReloadModel.UseVisualStyleBackColor = true;
			// 
			// TbRepeatNum
			// 
			this.TbRepeatNum.Location = new System.Drawing.Point(86, 21);
			this.TbRepeatNum.Name = "TbRepeatNum";
			this.TbRepeatNum.Size = new System.Drawing.Size(31, 19);
			this.TbRepeatNum.TabIndex = 2;
			this.TbRepeatNum.Text = "1";
			this.TbRepeatNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(7, 21);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(73, 19);
			this.label3.TabIndex = 1;
			this.label3.Text = "繰り返し回数:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// BtExecAll
			// 
			this.BtExecAll.Location = new System.Drawing.Point(129, 16);
			this.BtExecAll.Name = "BtExecAll";
			this.BtExecAll.Size = new System.Drawing.Size(58, 24);
			this.BtExecAll.TabIndex = 0;
			this.BtExecAll.Text = "実行";
			this.BtExecAll.UseVisualStyleBackColor = true;
			this.BtExecAll.Click += new System.EventHandler(this.BtExecAll_Click);
			// 
			// BtDetectionExec
			// 
			this.BtDetectionExec.Location = new System.Drawing.Point(137, 19);
			this.BtDetectionExec.Name = "BtDetectionExec";
			this.BtDetectionExec.Size = new System.Drawing.Size(69, 44);
			this.BtDetectionExec.TabIndex = 2;
			this.BtDetectionExec.Text = "実行";
			this.BtDetectionExec.UseVisualStyleBackColor = true;
			this.BtDetectionExec.Click += new System.EventHandler(this.BtDetectionExec_Click);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(6, 19);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(51, 23);
			this.label6.TabIndex = 15;
			this.label6.Text = "信頼度:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// TbConfidence
			// 
			this.TbConfidence.Location = new System.Drawing.Point(63, 21);
			this.TbConfidence.Name = "TbConfidence";
			this.TbConfidence.Size = new System.Drawing.Size(43, 19);
			this.TbConfidence.TabIndex = 0;
			this.TbConfidence.Text = "60.0";
			this.TbConfidence.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(112, 42);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(19, 23);
			this.label8.TabIndex = 2;
			this.label8.Text = "%";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(112, 19);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(13, 23);
			this.label7.TabIndex = 17;
			this.label7.Text = "%";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbNms
			// 
			this.TbNms.Location = new System.Drawing.Point(63, 44);
			this.TbNms.Name = "TbNms";
			this.TbNms.Size = new System.Drawing.Size(43, 19);
			this.TbNms.TabIndex = 1;
			this.TbNms.Text = "60.0";
			this.TbNms.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(6, 42);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(51, 23);
			this.label9.TabIndex = 18;
			this.label9.Text = "NMS:";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// TbResult
			// 
			this.TbResult.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbResult.Location = new System.Drawing.Point(0, 0);
			this.TbResult.Multiline = true;
			this.TbResult.Name = "TbResult";
			this.TbResult.ReadOnly = true;
			this.TbResult.Size = new System.Drawing.Size(214, 305);
			this.TbResult.TabIndex = 1;
			// 
			// AppMenuStrip
			// 
			this.AppMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemModelDefine,
            this.ToolStripMenuItemHelp});
			this.AppMenuStrip.Location = new System.Drawing.Point(0, 0);
			this.AppMenuStrip.Name = "AppMenuStrip";
			this.AppMenuStrip.Size = new System.Drawing.Size(902, 24);
			this.AppMenuStrip.TabIndex = 1;
			this.AppMenuStrip.Text = "menuStrip1";
			// 
			// ToolStripMenuItemModelDefine
			// 
			this.ToolStripMenuItemModelDefine.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemReadModel,
            this.toolStripSeparator1,
            this.ToolStripMenuItemSaveModel});
			this.ToolStripMenuItemModelDefine.Name = "ToolStripMenuItemModelDefine";
			this.ToolStripMenuItemModelDefine.Size = new System.Drawing.Size(90, 20);
			this.ToolStripMenuItemModelDefine.Text = "モデル定義(&M)";
			// 
			// ToolStripMenuItemReadModel
			// 
			this.ToolStripMenuItemReadModel.Name = "ToolStripMenuItemReadModel";
			this.ToolStripMenuItemReadModel.Size = new System.Drawing.Size(195, 22);
			this.ToolStripMenuItemReadModel.Text = "モデル定義読み込み...(&L)";
			this.ToolStripMenuItemReadModel.Click += new System.EventHandler(this.ToolStripMenuItemReadModel_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(192, 6);
			// 
			// ToolStripMenuItemSaveModel
			// 
			this.ToolStripMenuItemSaveModel.Name = "ToolStripMenuItemSaveModel";
			this.ToolStripMenuItemSaveModel.Size = new System.Drawing.Size(195, 22);
			this.ToolStripMenuItemSaveModel.Text = "モデル定義保存...(&S)";
			this.ToolStripMenuItemSaveModel.Click += new System.EventHandler(this.ToolStripMenuItemSaveModel_Click);
			// 
			// ToolStripMenuItemHelp
			// 
			this.ToolStripMenuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemAbount});
			this.ToolStripMenuItemHelp.Name = "ToolStripMenuItemHelp";
			this.ToolStripMenuItemHelp.Size = new System.Drawing.Size(65, 20);
			this.ToolStripMenuItemHelp.Text = "ヘルプ(&H)";
			// 
			// ToolStripMenuItemAbount
			// 
			this.ToolStripMenuItemAbount.Name = "ToolStripMenuItemAbount";
			this.ToolStripMenuItemAbount.Size = new System.Drawing.Size(116, 22);
			this.ToolStripMenuItemAbount.Text = "About...";
			this.ToolStripMenuItemAbount.Click += new System.EventHandler(this.ToolStripMenuItemAbount_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(902, 609);
			this.Controls.Add(this.MainTableLayout);
			this.Controls.Add(this.AppMenuStrip);
			this.MainMenuStrip = this.AppMenuStrip;
			this.Name = "MainForm";
			this.Text = "MainForm";
			this.MainTableLayout.ResumeLayout(false);
			this.CtrlPanel.ResumeLayout(false);
			this.CtrlPanel.PerformLayout();
			this.ModelGroup.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ImagePictureBox)).EndInit();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.DetectionGroup.ResumeLayout(false);
			this.DetectionGroup.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.AppMenuStrip.ResumeLayout(false);
			this.AppMenuStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel MainTableLayout;
		private System.Windows.Forms.Panel CtrlPanel;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button BtImageFolderOpen;
		private System.Windows.Forms.Button BtImageFolderSelect;
		private System.Windows.Forms.TextBox TbImageFolder;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button BtDetectionLoad;
		private System.Windows.Forms.ListBox LbImageFile;
		private Drawing.ZoomPictureBox ImagePictureBox;
		private System.Windows.Forms.ComboBox CbClass;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox TbNms;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox TbConfidence;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox TbResult;
		private System.Windows.Forms.GroupBox ModelGroup;
		private System.Windows.Forms.GroupBox DetectionGroup;
		private System.Windows.Forms.Button BtDetectionExec;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Button BtEditModel;
		private System.Windows.Forms.ComboBox CbBackendAndtarget;
		private System.Windows.Forms.ComboBox CbDetectionDef;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button BtExecAll;
		private System.Windows.Forms.CheckBox CbReloadModel;
		private System.Windows.Forms.TextBox TbRepeatNum;
		private System.Windows.Forms.CheckBox CbResultDraw;
		private System.Windows.Forms.MenuStrip AppMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemModelDefine;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemReadModel;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSaveModel;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemHelp;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemAbount;
	}
}

