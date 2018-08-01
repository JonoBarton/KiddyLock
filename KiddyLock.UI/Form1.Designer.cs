namespace KiddyLock.UI
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
            this.UserCombo = new System.Windows.Forms.ComboBox();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.UsersButton = new System.Windows.Forms.Button();
            this.MainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // UserCombo
            // 
            this.UserCombo.DisplayMember = "Name";
            this.UserCombo.FormattingEnabled = true;
            this.UserCombo.Location = new System.Drawing.Point(71, 12);
            this.UserCombo.Name = "UserCombo";
            this.UserCombo.Size = new System.Drawing.Size(298, 21);
            this.UserCombo.TabIndex = 0;
            this.UserCombo.ValueMember = "Sid";
            this.UserCombo.SelectedIndexChanged += new System.EventHandler(this.UserCombo_SelectedIndexChanged);
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.UserCombo);
            this.MainPanel.Location = new System.Drawing.Point(3, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(437, 146);
            this.MainPanel.TabIndex = 1;
            // 
            // UsersButton
            // 
            this.UsersButton.Location = new System.Drawing.Point(169, 152);
            this.UsersButton.Name = "UsersButton";
            this.UsersButton.Size = new System.Drawing.Size(75, 23);
            this.UsersButton.TabIndex = 2;
            this.UsersButton.Text = "Users";
            this.UsersButton.UseVisualStyleBackColor = true;
            this.UsersButton.Click += new System.EventHandler(this.UsersButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 453);
            this.Controls.Add(this.UsersButton);
            this.Controls.Add(this.MainPanel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MainPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox UserCombo;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Button UsersButton;
    }
}

