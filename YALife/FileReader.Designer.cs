namespace YALife
{
    partial class FileReader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileReader));
            this.TBReader = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // TBReader
            // 
            this.TBReader.BackColor = System.Drawing.Color.MidnightBlue;
            this.TBReader.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TBReader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TBReader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TBReader.ForeColor = System.Drawing.Color.LightSkyBlue;
            this.TBReader.Location = new System.Drawing.Point(0, 0);
            this.TBReader.Multiline = true;
            this.TBReader.Name = "TBReader";
            this.TBReader.ReadOnly = true;
            this.TBReader.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TBReader.Size = new System.Drawing.Size(611, 450);
            this.TBReader.TabIndex = 0;
            // 
            // FileReader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 450);
            this.Controls.Add(this.TBReader);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FileReader";
            this.Text = "FileReader";
            this.Load += new System.EventHandler(this.FileReader_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TBReader;
    }
}