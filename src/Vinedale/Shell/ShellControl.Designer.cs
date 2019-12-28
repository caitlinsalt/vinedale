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
            components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ShellTextBox = new Vinedale.Shell.ShellTextBox();
            this.SuspendLayout();
            // 
            // shellTextBox
            // 
            this.ShellTextBox.AcceptsReturn = true;
            this.ShellTextBox.AcceptsTab = true;
            this.ShellTextBox.BackColor = System.Drawing.Color.Black;
            this.ShellTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ShellTextBox.Enabled = false;
            this.ShellTextBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShellTextBox.ForeColor = System.Drawing.Color.LawnGreen;
            this.ShellTextBox.Location = new System.Drawing.Point(0, 0);
            this.ShellTextBox.MaxLength = 0;
            this.ShellTextBox.Multiline = true;
            this.ShellTextBox.Name = "shellTextBox";
            this.ShellTextBox.Prompt = "? ";
            this.ShellTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ShellTextBox.Size = new System.Drawing.Size(232, 216);
            this.ShellTextBox.TabIndex = 0;
            // 
            // ShellControl
            // 
            this.Controls.Add(this.ShellTextBox);
            this.Name = "ShellControl";
            this.Size = new System.Drawing.Size(232, 216);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private ShellTextBox ShellTextBox;
    }
}
