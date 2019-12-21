using System.Windows.Forms;

namespace Vinedale.Shell
{
    partial class ShellTextBox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ShellTextBox
            // 
            this.AcceptsReturn = true;
            this.AcceptsTab = true;
            this.BackColor = System.Drawing.Color.Black;
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.LawnGreen;
            this.MaxLength = 0;
            this.Multiline = true;
            this.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.Size = new System.Drawing.Size(400, 176);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.shellTextBox_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.shellTextBox_KeyPress);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
