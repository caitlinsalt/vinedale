namespace Vinedale.Shell
{
    partial class ShellControl
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
            this.shellTextBox = new Vinedale.Shell.ShellTextBox();
            this.SuspendLayout();
            // 
            // shellTextBox
            // 
            this.shellTextBox.AcceptsReturn = true;
            this.shellTextBox.AcceptsTab = true;
            this.shellTextBox.BackColor = System.Drawing.Color.Black;
            this.shellTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shellTextBox.Enabled = false;
            this.shellTextBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shellTextBox.ForeColor = System.Drawing.Color.LawnGreen;
            this.shellTextBox.Location = new System.Drawing.Point(0, 0);
            this.shellTextBox.MaxLength = 0;
            this.shellTextBox.Multiline = true;
            this.shellTextBox.Name = "shellTextBox";
            this.shellTextBox.Prompt = "? ";
            this.shellTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.shellTextBox.Size = new System.Drawing.Size(232, 216);
            this.shellTextBox.TabIndex = 0;
            // 
            // ShellControl
            // 
            this.Controls.Add(this.shellTextBox);
            this.Name = "ShellControl";
            this.Size = new System.Drawing.Size(232, 216);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ShellTextBox shellTextBox;

    }
}
