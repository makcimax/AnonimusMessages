﻿namespace Client10
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.InputName = new System.Windows.Forms.TextBox();
            this.OutputMessage = new System.Windows.Forms.RichTextBox();
            this.InputMessage = new System.Windows.Forms.RichTextBox();
            this.SendButton = new System.Windows.Forms.Button();
            this.AbonentList = new System.Windows.Forms.CheckedListBox();
            this.ConnDisconnButton = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.LoginPanel = new System.Windows.Forms.GroupBox();
            this.SendPanel = new System.Windows.Forms.Panel();
            this.LoginPanel.SuspendLayout();
            this.SendPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // InputName
            // 
            this.InputName.Location = new System.Drawing.Point(6, 19);
            this.InputName.Name = "InputName";
            this.InputName.Size = new System.Drawing.Size(160, 20);
            this.InputName.TabIndex = 2;
            this.InputName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InputName_KeyDown);
            // 
            // OutputMessage
            // 
            this.OutputMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OutputMessage.BackColor = System.Drawing.SystemColors.Window;
            this.OutputMessage.Enabled = false;
            this.OutputMessage.Location = new System.Drawing.Point(205, 62);
            this.OutputMessage.Name = "OutputMessage";
            this.OutputMessage.ReadOnly = true;
            this.OutputMessage.Size = new System.Drawing.Size(583, 284);
            this.OutputMessage.TabIndex = 3;
            this.OutputMessage.Text = "";
            // 
            // InputMessage
            // 
            this.InputMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InputMessage.Enabled = false;
            this.InputMessage.Location = new System.Drawing.Point(0, 0);
            this.InputMessage.Name = "InputMessage";
            this.InputMessage.Size = new System.Drawing.Size(499, 84);
            this.InputMessage.TabIndex = 4;
            this.InputMessage.Text = "";
            this.InputMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InputMessage_KeyDown);
            // 
            // SendButton
            // 
            this.SendButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.SendButton.Enabled = false;
            this.SendButton.Location = new System.Drawing.Point(505, 31);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(75, 23);
            this.SendButton.TabIndex = 5;
            this.SendButton.Text = "Send";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // AbonentList
            // 
            this.AbonentList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.AbonentList.Enabled = false;
            this.AbonentList.FormattingEnabled = true;
            this.AbonentList.Location = new System.Drawing.Point(12, 12);
            this.AbonentList.Name = "AbonentList";
            this.AbonentList.Size = new System.Drawing.Size(187, 424);
            this.AbonentList.TabIndex = 6;
            // 
            // ConnDisconnButton
            // 
            this.ConnDisconnButton.Location = new System.Drawing.Point(172, 17);
            this.ConnDisconnButton.Name = "ConnDisconnButton";
            this.ConnDisconnButton.Size = new System.Drawing.Size(75, 23);
            this.ConnDisconnButton.TabIndex = 7;
            this.ConnDisconnButton.Text = "Connect";
            this.ConnDisconnButton.UseVisualStyleBackColor = true;
            this.ConnDisconnButton.Click += new System.EventHandler(this.ConnDisconnButton_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(253, 17);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(75, 23);
            this.ExitButton.TabIndex = 8;
            this.ExitButton.Text = "Exit";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // LoginPanel
            // 
            this.LoginPanel.Controls.Add(this.InputName);
            this.LoginPanel.Controls.Add(this.ExitButton);
            this.LoginPanel.Controls.Add(this.ConnDisconnButton);
            this.LoginPanel.Location = new System.Drawing.Point(205, 6);
            this.LoginPanel.Name = "LoginPanel";
            this.LoginPanel.Size = new System.Drawing.Size(335, 50);
            this.LoginPanel.TabIndex = 9;
            this.LoginPanel.TabStop = false;
            this.LoginPanel.Text = "Enter your name:";
            // 
            // SendPanel
            // 
            this.SendPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SendPanel.Controls.Add(this.SendButton);
            this.SendPanel.Controls.Add(this.InputMessage);
            this.SendPanel.Location = new System.Drawing.Point(205, 352);
            this.SendPanel.Name = "SendPanel";
            this.SendPanel.Size = new System.Drawing.Size(583, 84);
            this.SendPanel.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.SendPanel);
            this.Controls.Add(this.LoginPanel);
            this.Controls.Add(this.AbonentList);
            this.Controls.Add(this.OutputMessage);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(816, 489);
            this.Name = "Form1";
            this.Text = "Login";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.LoginPanel.ResumeLayout(false);
            this.LoginPanel.PerformLayout();
            this.SendPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox InputName;
        private System.Windows.Forms.RichTextBox OutputMessage;
        private System.Windows.Forms.RichTextBox InputMessage;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.CheckedListBox AbonentList;
        private System.Windows.Forms.Button ConnDisconnButton;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.GroupBox LoginPanel;
        private System.Windows.Forms.Panel SendPanel;
    }
}

