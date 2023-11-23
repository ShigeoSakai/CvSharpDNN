namespace Drawing.Dialog
{
	partial class AxisForm
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
			this.BtOk = new System.Windows.Forms.Button();
			this.BtCancel = new System.Windows.Forms.Button();
			this.LbX = new System.Windows.Forms.Label();
			this.TbX = new System.Windows.Forms.TextBox();
			this.TbY = new System.Windows.Forms.TextBox();
			this.LbY = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// BtOk
			// 
			this.BtOk.Location = new System.Drawing.Point(33, 42);
			this.BtOk.Name = "BtOk";
			this.BtOk.Size = new System.Drawing.Size(58, 23);
			this.BtOk.TabIndex = 2;
			this.BtOk.Text = "OK";
			this.BtOk.UseVisualStyleBackColor = true;
			this.BtOk.Click += new System.EventHandler(this.BtOk_Click);
			// 
			// BtCancel
			// 
			this.BtCancel.Location = new System.Drawing.Point(128, 42);
			this.BtCancel.Name = "BtCancel";
			this.BtCancel.Size = new System.Drawing.Size(58, 23);
			this.BtCancel.TabIndex = 3;
			this.BtCancel.Text = "Cancel";
			this.BtCancel.UseVisualStyleBackColor = true;
			this.BtCancel.Click += new System.EventHandler(this.BtCancel_Click);
			// 
			// LbX
			// 
			this.LbX.Location = new System.Drawing.Point(1, 9);
			this.LbX.Name = "LbX";
			this.LbX.Size = new System.Drawing.Size(26, 23);
			this.LbX.TabIndex = 2;
			this.LbX.Text = "X:";
			this.LbX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// TbX
			// 
			this.TbX.Location = new System.Drawing.Point(33, 11);
			this.TbX.Name = "TbX";
			this.TbX.Size = new System.Drawing.Size(64, 19);
			this.TbX.TabIndex = 0;
			this.TbX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// TbY
			// 
			this.TbY.Location = new System.Drawing.Point(128, 11);
			this.TbY.Name = "TbY";
			this.TbY.Size = new System.Drawing.Size(64, 19);
			this.TbY.TabIndex = 1;
			this.TbY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// LbY
			// 
			this.LbY.Location = new System.Drawing.Point(104, 9);
			this.LbY.Name = "LbY";
			this.LbY.Size = new System.Drawing.Size(18, 23);
			this.LbY.TabIndex = 4;
			this.LbY.Text = "Y:";
			this.LbY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// AxisForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(211, 67);
			this.ControlBox = false;
			this.Controls.Add(this.TbY);
			this.Controls.Add(this.LbY);
			this.Controls.Add(this.TbX);
			this.Controls.Add(this.LbX);
			this.Controls.Add(this.BtCancel);
			this.Controls.Add(this.BtOk);
			this.Name = "AxisForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "AxisForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button BtOk;
		private System.Windows.Forms.Button BtCancel;
		private System.Windows.Forms.Label LbX;
		private System.Windows.Forms.TextBox TbX;
		private System.Windows.Forms.TextBox TbY;
		private System.Windows.Forms.Label LbY;
	}
}