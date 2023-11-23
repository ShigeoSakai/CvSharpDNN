namespace Drawing.Dialog
{
	partial class PropertyForm
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.PropGrid = new System.Windows.Forms.PropertyGrid();
			this.panel1 = new System.Windows.Forms.Panel();
			this.BtClose = new System.Windows.Forms.Button();
			this.BtRevert = new System.Windows.Forms.Button();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.PropGrid, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(392, 314);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// PropGrid
			// 
			this.PropGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PropGrid.Location = new System.Drawing.Point(3, 3);
			this.PropGrid.Name = "PropGrid";
			this.PropGrid.Size = new System.Drawing.Size(386, 276);
			this.PropGrid.TabIndex = 0;
			this.PropGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.PropGrid_PropertyValueChanged);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.BtRevert);
			this.panel1.Controls.Add(this.BtClose);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 282);
			this.panel1.Margin = new System.Windows.Forms.Padding(0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(392, 32);
			this.panel1.TabIndex = 1;
			// 
			// BtClose
			// 
			this.BtClose.Location = new System.Drawing.Point(151, 6);
			this.BtClose.Name = "BtClose";
			this.BtClose.Size = new System.Drawing.Size(75, 23);
			this.BtClose.TabIndex = 0;
			this.BtClose.Text = "閉じる";
			this.BtClose.UseVisualStyleBackColor = true;
			this.BtClose.Click += new System.EventHandler(this.BtClose_Click);
			// 
			// BtRevert
			// 
			this.BtRevert.Location = new System.Drawing.Point(267, 6);
			this.BtRevert.Name = "BtRevert";
			this.BtRevert.Size = new System.Drawing.Size(75, 23);
			this.BtRevert.TabIndex = 1;
			this.BtRevert.Text = "元に戻す";
			this.BtRevert.UseVisualStyleBackColor = true;
			this.BtRevert.Click += new System.EventHandler(this.BtRevert_Click);
			// 
			// PropertyForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(392, 314);
			this.ControlBox = false;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "PropertyForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "PropertyForm";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.PropertyGrid PropGrid;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button BtClose;
		private System.Windows.Forms.Button BtRevert;
	}
}