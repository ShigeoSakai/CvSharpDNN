namespace Drawing
{
    partial class ZoomPictureBox
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
			this.CtxMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.TSMenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMenuItemImageCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMenuItemOrigCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.TSMenuItemSave = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMenuItemImageSave = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMenuItemOrigSave = new System.Windows.Forms.ToolStripMenuItem();
			this.LbInfo = new System.Windows.Forms.Label();
			this.CtxMenuStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// CtxMenuStrip
			// 
			this.CtxMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMenuItemCopy,
            this.toolStripSeparator1,
            this.TSMenuItemSave});
			this.CtxMenuStrip.Name = "CtxMenuStrip";
			this.CtxMenuStrip.Size = new System.Drawing.Size(108, 54);
			this.CtxMenuStrip.Text = "操作メニュー";
			// 
			// TSMenuItemCopy
			// 
			this.TSMenuItemCopy.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMenuItemImageCopy,
            this.TSMenuItemOrigCopy});
			this.TSMenuItemCopy.Name = "TSMenuItemCopy";
			this.TSMenuItemCopy.Size = new System.Drawing.Size(107, 22);
			this.TSMenuItemCopy.Text = "コピー";
			// 
			// TSMenuItemImageCopy
			// 
			this.TSMenuItemImageCopy.Name = "TSMenuItemImageCopy";
			this.TSMenuItemImageCopy.Size = new System.Drawing.Size(122, 22);
			this.TSMenuItemImageCopy.Text = "表示画像";
			this.TSMenuItemImageCopy.Click += new System.EventHandler(this.TSMenuItemImageCopy_Click);
			// 
			// TSMenuItemOrigCopy
			// 
			this.TSMenuItemOrigCopy.Name = "TSMenuItemOrigCopy";
			this.TSMenuItemOrigCopy.Size = new System.Drawing.Size(122, 22);
			this.TSMenuItemOrigCopy.Text = "元画像";
			this.TSMenuItemOrigCopy.Click += new System.EventHandler(this.TSMenuItemOrigCopy_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(104, 6);
			// 
			// TSMenuItemSave
			// 
			this.TSMenuItemSave.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMenuItemImageSave,
            this.TSMenuItemOrigSave});
			this.TSMenuItemSave.Name = "TSMenuItemSave";
			this.TSMenuItemSave.Size = new System.Drawing.Size(107, 22);
			this.TSMenuItemSave.Text = "保存...";
			// 
			// TSMenuItemImageSave
			// 
			this.TSMenuItemImageSave.Name = "TSMenuItemImageSave";
			this.TSMenuItemImageSave.Size = new System.Drawing.Size(122, 22);
			this.TSMenuItemImageSave.Text = "表示画像";
			this.TSMenuItemImageSave.Click += new System.EventHandler(this.TSMenuItemImageSave_Click);
			// 
			// TSMenuItemOrigSave
			// 
			this.TSMenuItemOrigSave.Name = "TSMenuItemOrigSave";
			this.TSMenuItemOrigSave.Size = new System.Drawing.Size(122, 22);
			this.TSMenuItemOrigSave.Text = "元画像";
			this.TSMenuItemOrigSave.Click += new System.EventHandler(this.TSMenuItemOrigSave_Click);
			// 
			// LbInfo
			// 
			this.LbInfo.AutoSize = true;
			this.LbInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.LbInfo.Location = new System.Drawing.Point(0, 0);
			this.LbInfo.Name = "LbInfo";
			this.LbInfo.Size = new System.Drawing.Size(100, 23);
			this.LbInfo.TabIndex = 0;
			this.LbInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.CtxMenuStrip.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.ContextMenuStrip CtxMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem TSMenuItemCopy;
        private System.Windows.Forms.ToolStripMenuItem TSMenuItemImageCopy;
        private System.Windows.Forms.ToolStripMenuItem TSMenuItemOrigCopy;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem TSMenuItemSave;
        private System.Windows.Forms.ToolStripMenuItem TSMenuItemImageSave;
        private System.Windows.Forms.ToolStripMenuItem TSMenuItemOrigSave;
        private System.Windows.Forms.Label LbInfo;
    }
}
