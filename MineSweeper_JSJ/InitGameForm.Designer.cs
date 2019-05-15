namespace MineSweeper_JSJ
{
    partial class InitGameForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.trackBar_width = new System.Windows.Forms.TrackBar();
            this.trackBar_height = new System.Windows.Forms.TrackBar();
            this.trackBar_mine = new System.Windows.Forms.TrackBar();
            this.textBox_width = new System.Windows.Forms.TextBox();
            this.textBox_heigth = new System.Windows.Forms.TextBox();
            this.textBox_mines = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_height)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_mine)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Field Width";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Field Height";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "Mines";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(131, 235);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 3;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(212, 235);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // trackBar_width
            // 
            this.trackBar_width.Location = new System.Drawing.Point(15, 28);
            this.trackBar_width.Name = "trackBar_width";
            this.trackBar_width.Size = new System.Drawing.Size(315, 45);
            this.trackBar_width.TabIndex = 5;
            this.trackBar_width.Scroll += new System.EventHandler(this.trackBar_width_Scroll);
            // 
            // trackBar_height
            // 
            this.trackBar_height.Location = new System.Drawing.Point(15, 91);
            this.trackBar_height.Name = "trackBar_height";
            this.trackBar_height.Size = new System.Drawing.Size(317, 45);
            this.trackBar_height.TabIndex = 6;
            this.trackBar_height.Scroll += new System.EventHandler(this.trackBar_height_Scroll);
            // 
            // trackBar_mine
            // 
            this.trackBar_mine.Location = new System.Drawing.Point(15, 154);
            this.trackBar_mine.Name = "trackBar_mine";
            this.trackBar_mine.Size = new System.Drawing.Size(315, 45);
            this.trackBar_mine.TabIndex = 7;
            this.trackBar_mine.Scroll += new System.EventHandler(this.trackBar_mine_Scroll);
            // 
            // textBox_width
            // 
            this.textBox_width.Location = new System.Drawing.Point(336, 37);
            this.textBox_width.Name = "textBox_width";
            this.textBox_width.ReadOnly = true;
            this.textBox_width.Size = new System.Drawing.Size(70, 21);
            this.textBox_width.TabIndex = 8;
            this.textBox_width.Text = "0";
            // 
            // textBox_heigth
            // 
            this.textBox_heigth.Location = new System.Drawing.Point(336, 101);
            this.textBox_heigth.Name = "textBox_heigth";
            this.textBox_heigth.ReadOnly = true;
            this.textBox_heigth.Size = new System.Drawing.Size(70, 21);
            this.textBox_heigth.TabIndex = 9;
            this.textBox_heigth.Text = "0";
            // 
            // textBox_mines
            // 
            this.textBox_mines.Location = new System.Drawing.Point(336, 163);
            this.textBox_mines.Name = "textBox_mines";
            this.textBox_mines.ReadOnly = true;
            this.textBox_mines.Size = new System.Drawing.Size(70, 21);
            this.textBox_mines.TabIndex = 10;
            this.textBox_mines.Text = "0";
            // 
            // InitGameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 270);
            this.ControlBox = false;
            this.Controls.Add(this.textBox_mines);
            this.Controls.Add(this.textBox_heigth);
            this.Controls.Add(this.textBox_width);
            this.Controls.Add(this.trackBar_mine);
            this.Controls.Add(this.trackBar_height);
            this.Controls.Add(this.trackBar_width);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InitGameForm";
            this.Text = "New Game Field";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_height)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_mine)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TrackBar trackBar_width;
        private System.Windows.Forms.TrackBar trackBar_height;
        private System.Windows.Forms.TrackBar trackBar_mine;
        private System.Windows.Forms.TextBox textBox_width;
        private System.Windows.Forms.TextBox textBox_heigth;
        private System.Windows.Forms.TextBox textBox_mines;
    }
}