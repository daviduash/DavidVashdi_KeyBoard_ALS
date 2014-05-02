namespace KeyBoard
{
    partial class frmFixScreen
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
            this.keyBoardButton7 = new KeyBoard.KeyBoardButton();
            this.keyBoardButton6 = new KeyBoard.KeyBoardButton();
            this.keyBoardButton5 = new KeyBoard.KeyBoardButton();
            this.keyBoardButton4 = new KeyBoard.KeyBoardButton();
            this.keyBoardButton3 = new KeyBoard.KeyBoardButton();
            this.keyBoardButton2 = new KeyBoard.KeyBoardButton();
            this.keyBoardButton1 = new KeyBoard.KeyBoardButton();
            this.SuspendLayout();
            // 
            // keyBoardButton7
            // 
            this.keyBoardButton7.Location = new System.Drawing.Point(61, 180);
            this.keyBoardButton7.Name = "keyBoardButton7";
            this.keyBoardButton7.Size = new System.Drawing.Size(75, 23);
            this.keyBoardButton7.TabIndex = 6;
            this.keyBoardButton7.Text = "keyBoardButton7";
            this.keyBoardButton7.UseVisualStyleBackColor = true;
            // 
            // keyBoardButton6
            // 
            this.keyBoardButton6.Location = new System.Drawing.Point(181, 195);
            this.keyBoardButton6.Name = "keyBoardButton6";
            this.keyBoardButton6.Size = new System.Drawing.Size(75, 23);
            this.keyBoardButton6.TabIndex = 5;
            this.keyBoardButton6.Text = "keyBoardButton6";
            this.keyBoardButton6.UseVisualStyleBackColor = true;
            this.keyBoardButton6.Click += new System.EventHandler(this.keyBoardButton6_Click);
            // 
            // keyBoardButton5
            // 
            this.keyBoardButton5.Location = new System.Drawing.Point(170, 31);
            this.keyBoardButton5.Name = "keyBoardButton5";
            this.keyBoardButton5.Size = new System.Drawing.Size(75, 23);
            this.keyBoardButton5.TabIndex = 4;
            this.keyBoardButton5.Text = "keyBoardButton5";
            this.keyBoardButton5.UseVisualStyleBackColor = true;
            // 
            // keyBoardButton4
            // 
            this.keyBoardButton4.Location = new System.Drawing.Point(44, 90);
            this.keyBoardButton4.Name = "keyBoardButton4";
            this.keyBoardButton4.Size = new System.Drawing.Size(75, 23);
            this.keyBoardButton4.TabIndex = 3;
            this.keyBoardButton4.Text = "keyBoardButton4";
            this.keyBoardButton4.UseVisualStyleBackColor = true;
            // 
            // keyBoardButton3
            // 
            this.keyBoardButton3.BackColor = System.Drawing.SystemColors.Control;
            this.keyBoardButton3.Location = new System.Drawing.Point(170, 90);
            this.keyBoardButton3.Name = "keyBoardButton3";
            this.keyBoardButton3.Size = new System.Drawing.Size(75, 23);
            this.keyBoardButton3.TabIndex = 2;
            this.keyBoardButton3.Text = "keyBoardButton3";
            this.keyBoardButton3.UseVisualStyleBackColor = false;
            this.keyBoardButton3.Click += new System.EventHandler(this.keyBoardButton3_Click);
            this.keyBoardButton3.MouseEnter += new System.EventHandler(this.keyBoardButton3_MouseEnter);
            // 
            // keyBoardButton2
            // 
            this.keyBoardButton2.Location = new System.Drawing.Point(105, 119);
            this.keyBoardButton2.Name = "keyBoardButton2";
            this.keyBoardButton2.Size = new System.Drawing.Size(75, 23);
            this.keyBoardButton2.TabIndex = 1;
            this.keyBoardButton2.Text = "keyBoardButton2";
            this.keyBoardButton2.UseVisualStyleBackColor = true;
            // 
            // keyBoardButton1
            // 
            this.keyBoardButton1.Location = new System.Drawing.Point(12, 12);
            this.keyBoardButton1.Name = "keyBoardButton1";
            this.keyBoardButton1.Size = new System.Drawing.Size(75, 23);
            this.keyBoardButton1.TabIndex = 0;
            this.keyBoardButton1.Text = "keyBoardButton1";
            this.keyBoardButton1.UseVisualStyleBackColor = true;
            // 
            // frmFixScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.keyBoardButton7);
            this.Controls.Add(this.keyBoardButton6);
            this.Controls.Add(this.keyBoardButton5);
            this.Controls.Add(this.keyBoardButton4);
            this.Controls.Add(this.keyBoardButton3);
            this.Controls.Add(this.keyBoardButton2);
            this.Controls.Add(this.keyBoardButton1);
            this.Name = "frmFixScreen";
            this.Text = "frmFixScreen";
            this.Load += new System.EventHandler(this.frmFixScreen_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private KeyBoardButton keyBoardButton1;
        private KeyBoardButton keyBoardButton2;
        private KeyBoardButton keyBoardButton3;
        private KeyBoardButton keyBoardButton4;
        private KeyBoardButton keyBoardButton5;
        private KeyBoardButton keyBoardButton6;
        private KeyBoardButton keyBoardButton7;

    }
}