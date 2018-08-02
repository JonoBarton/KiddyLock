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
            this.panel1 = new System.Windows.Forms.Panel();
            this.SubscriberNameLabel = new System.Windows.Forms.Label();
            this.SubscriberImage = new System.Windows.Forms.PictureBox();
            this.MainPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SubscriberImage)).BeginInit();
            this.SuspendLayout();
            // 
            // UserCombo
            // 
            this.UserCombo.DisplayMember = "Name";
            this.UserCombo.FormattingEnabled = true;
            this.UserCombo.Location = new System.Drawing.Point(99, 241);
            this.UserCombo.Name = "UserCombo";
            this.UserCombo.Size = new System.Drawing.Size(298, 21);
            this.UserCombo.TabIndex = 0;
            this.UserCombo.ValueMember = "Sid";
            this.UserCombo.SelectedIndexChanged += new System.EventHandler(this.UserCombo_SelectedIndexChanged);
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.panel1);
            this.MainPanel.Controls.Add(this.UsersButton);
            this.MainPanel.Controls.Add(this.UserCombo);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(618, 453);
            this.MainPanel.TabIndex = 1;
            this.MainPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.MainPanel_Paint);
            // 
            // UsersButton
            // 
            this.UsersButton.Location = new System.Drawing.Point(170, 123);
            this.UsersButton.Name = "UsersButton";
            this.UsersButton.Size = new System.Drawing.Size(75, 23);
            this.UsersButton.TabIndex = 2;
            this.UsersButton.Text = "Users";
            this.UsersButton.UseVisualStyleBackColor = true;
            this.UsersButton.Click += new System.EventHandler(this.UsersButton_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel1.Controls.Add(this.SubscriberImage);
            this.panel1.Controls.Add(this.SubscriberNameLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(618, 86);
            this.panel1.TabIndex = 3;
            // 
            // SubscriberNameLabel
            // 
            this.SubscriberNameLabel.AutoSize = true;
            this.SubscriberNameLabel.Location = new System.Drawing.Point(320, 32);
            this.SubscriberNameLabel.Name = "SubscriberNameLabel";
            this.SubscriberNameLabel.Size = new System.Drawing.Size(68, 13);
            this.SubscriberNameLabel.TabIndex = 0;
            this.SubscriberNameLabel.Text = "Signed in as ";
            // 
            // SubscriberImage
            // 
            this.SubscriberImage.Location = new System.Drawing.Point(515, 3);
            this.SubscriberImage.Name = "SubscriberImage";
            this.SubscriberImage.Size = new System.Drawing.Size(100, 80);
            this.SubscriberImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.SubscriberImage.TabIndex = 1;
            this.SubscriberImage.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 453);
            this.Controls.Add(this.MainPanel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MainPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SubscriberImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox UserCombo;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Button UsersButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox SubscriberImage;
        private System.Windows.Forms.Label SubscriberNameLabel;
    }
}

