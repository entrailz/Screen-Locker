namespace Testing
{
    partial class Form1
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
            this.nsTheme1 = new ClassLibrary2.NSTheme();
            this.nsButton1 = new ClassLibrary2.NSButton();
            this.nsTextBox1 = new ClassLibrary2.NSTextBox();
            this.nsTheme1.SuspendLayout();
            this.SuspendLayout();
            // 
            // nsTheme1
            // 
            this.nsTheme1.AccentOffset = 42;
            this.nsTheme1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.nsTheme1.BorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.nsTheme1.Colors = new ClassLibrary2.Bloom[0];
            this.nsTheme1.Controls.Add(this.nsButton1);
            this.nsTheme1.Controls.Add(this.nsTextBox1);
            this.nsTheme1.Customization = "";
            this.nsTheme1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nsTheme1.Font = new System.Drawing.Font("Verdana", 8F);
            this.nsTheme1.Image = null;
            this.nsTheme1.Location = new System.Drawing.Point(0, 0);
            this.nsTheme1.Movable = true;
            this.nsTheme1.Name = "nsTheme1";
            this.nsTheme1.NoRounding = false;
            this.nsTheme1.Sizable = true;
            this.nsTheme1.Size = new System.Drawing.Size(644, 455);
            this.nsTheme1.SmartBounds = true;
            this.nsTheme1.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultLocation;
            this.nsTheme1.TabIndex = 0;
            this.nsTheme1.Text = "Computer Lock";
            this.nsTheme1.TransparencyKey = System.Drawing.Color.Empty;
            this.nsTheme1.Transparent = false;
            // 
            // nsButton1
            // 
            this.nsButton1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.nsButton1.Location = new System.Drawing.Point(281, 275);
            this.nsButton1.Name = "nsButton1";
            this.nsButton1.Size = new System.Drawing.Size(75, 23);
            this.nsButton1.TabIndex = 1;
            this.nsButton1.Text = "    Login";
            this.nsButton1.Click += new System.EventHandler(this.nsButton1_Click);
            // 
            // nsTextBox1
            // 
            this.nsTextBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.nsTextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.nsTextBox1.Location = new System.Drawing.Point(0, 220);
            this.nsTextBox1.MaxLength = 32767;
            this.nsTextBox1.Multiline = false;
            this.nsTextBox1.Name = "nsTextBox1";
            this.nsTextBox1.ReadOnly = false;
            this.nsTextBox1.Size = new System.Drawing.Size(644, 23);
            this.nsTextBox1.TabIndex = 0;
            this.nsTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.nsTextBox1.UseSystemPasswordChar = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 455);
            this.Controls.Add(this.nsTheme1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.nsTheme1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ClassLibrary2.NSTheme nsTheme1;
        private ClassLibrary2.NSTextBox nsTextBox1;
        private ClassLibrary2.NSButton nsButton1;

    }
}

