namespace ChatSqlServer
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.chatBox = new System.Windows.Forms.RichTextBox();
            this.sm1_button = new System.Windows.Forms.Button();
            this.sm2_button = new System.Windows.Forms.Button();
            this.sm3_button = new System.Windows.Forms.Button();
            this.sm4_button = new System.Windows.Forms.Button();
            this.inputBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(494, 553);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(64, 62);
            this.button1.TabIndex = 2;
            this.button1.Text = "Send";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 2500;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(564, 551);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(39, 64);
            this.button2.TabIndex = 4;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // chatBox
            // 
            this.chatBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chatBox.Location = new System.Drawing.Point(23, 24);
            this.chatBox.Name = "chatBox";
            this.chatBox.Size = new System.Drawing.Size(580, 477);
            this.chatBox.TabIndex = 5;
            this.chatBox.Text = "";
            this.chatBox.Enter += new System.EventHandler(this.chatBox_Enter);
            // 
            // sm1_button
            // 
            this.sm1_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sm1_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.sm1_button.FlatAppearance.BorderSize = 0;
            this.sm1_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sm1_button.Image = ((System.Drawing.Image)(resources.GetObject("sm1_button.Image")));
            this.sm1_button.Location = new System.Drawing.Point(23, 507);
            this.sm1_button.Name = "sm1_button";
            this.sm1_button.Size = new System.Drawing.Size(46, 36);
            this.sm1_button.TabIndex = 6;
            this.sm1_button.UseVisualStyleBackColor = true;
            this.sm1_button.Click += new System.EventHandler(this.sm1_button_Click);
            // 
            // sm2_button
            // 
            this.sm2_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sm2_button.FlatAppearance.BorderSize = 0;
            this.sm2_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sm2_button.Image = ((System.Drawing.Image)(resources.GetObject("sm2_button.Image")));
            this.sm2_button.Location = new System.Drawing.Point(75, 507);
            this.sm2_button.Name = "sm2_button";
            this.sm2_button.Size = new System.Drawing.Size(46, 38);
            this.sm2_button.TabIndex = 7;
            this.sm2_button.UseVisualStyleBackColor = true;
            // 
            // sm3_button
            // 
            this.sm3_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sm3_button.FlatAppearance.BorderSize = 0;
            this.sm3_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sm3_button.Image = ((System.Drawing.Image)(resources.GetObject("sm3_button.Image")));
            this.sm3_button.Location = new System.Drawing.Point(127, 507);
            this.sm3_button.Name = "sm3_button";
            this.sm3_button.Size = new System.Drawing.Size(46, 38);
            this.sm3_button.TabIndex = 8;
            this.sm3_button.UseVisualStyleBackColor = true;
            // 
            // sm4_button
            // 
            this.sm4_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sm4_button.FlatAppearance.BorderSize = 0;
            this.sm4_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sm4_button.Image = ((System.Drawing.Image)(resources.GetObject("sm4_button.Image")));
            this.sm4_button.Location = new System.Drawing.Point(179, 507);
            this.sm4_button.Name = "sm4_button";
            this.sm4_button.Size = new System.Drawing.Size(46, 38);
            this.sm4_button.TabIndex = 9;
            this.sm4_button.UseVisualStyleBackColor = true;
            // 
            // inputBox
            // 
            this.inputBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inputBox.Location = new System.Drawing.Point(12, 553);
            this.inputBox.Name = "inputBox";
            this.inputBox.Size = new System.Drawing.Size(476, 62);
            this.inputBox.TabIndex = 10;
            this.inputBox.Text = "";
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 627);
            this.Controls.Add(this.inputBox);
            this.Controls.Add(this.sm4_button);
            this.Controls.Add(this.sm3_button);
            this.Controls.Add(this.sm2_button);
            this.Controls.Add(this.sm1_button);
            this.Controls.Add(this.chatBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.MinimumSize = new System.Drawing.Size(430, 600);
            this.Name = "Form1";
            this.Text = "SQL Server Chat";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RichTextBox chatBox;
        private System.Windows.Forms.Button sm1_button;
        private System.Windows.Forms.Button sm2_button;
        private System.Windows.Forms.Button sm3_button;
        private System.Windows.Forms.Button sm4_button;
        private System.Windows.Forms.RichTextBox inputBox;
    }
}

