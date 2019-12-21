namespace Vinedale
{
    partial class FrontEnd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrontEnd));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.interpShell = new Vinedale.Shell.ShellControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.debugShell = new Vinedale.Shell.ShellControl();
            this.panel3 = new System.Windows.Forms.Panel();
            this.turtleWindow1 = new Vinedale.WfTurtle.TurtleWindow();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.interpShell);
            this.panel1.Name = "panel1";
            // 
            // interpShell
            // 
            resources.ApplyResources(this.interpShell, "interpShell");
            this.interpShell.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.interpShell.Name = "interpShell";
            this.interpShell.Prompt = "? ";
            this.interpShell.ShellTextBackColour = System.Drawing.Color.DarkBlue;
            this.interpShell.ShellTextFont = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.interpShell.ShellTextForeColour = System.Drawing.Color.White;
            this.interpShell.CommandEntered += new Vinedale.Shell.ShellControl.EventCommandEntered(this.interpShell_CommandEntered);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.debugShell);
            this.panel2.Controls.Add(this.label2);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // debugShell
            // 
            resources.ApplyResources(this.debugShell, "debugShell");
            this.debugShell.Name = "debugShell";
            this.debugShell.Prompt = "";
            this.debugShell.ShellTextBackColour = System.Drawing.Color.MidnightBlue;
            this.debugShell.ShellTextFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.debugShell.ShellTextForeColour = System.Drawing.Color.LightGray;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.turtleWindow1);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // turtleWindow1
            // 
            this.turtleWindow1.BackColor = System.Drawing.Color.DarkBlue;
            this.turtleWindow1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.turtleWindow1, "turtleWindow1");
            this.turtleWindow1.Name = "turtleWindow1";
            // 
            // FrontEnd
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FrontEnd";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Shell.ShellControl interpShell;
        private System.Windows.Forms.Label label1;
        private Shell.ShellControl debugShell;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private WfTurtle.TurtleWindow turtleWindow1;
    }
}

