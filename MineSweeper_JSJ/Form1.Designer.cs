namespace MineSweeper_JSJ
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.mineFieldRenderBox = new System.Windows.Forms.PictureBox();
            this.gameStartButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.mineFieldRenderBox)).BeginInit();
            this.SuspendLayout();
            // 
            // mineFieldRenderBox
            // 
            this.mineFieldRenderBox.Location = new System.Drawing.Point(10, 100);
            this.mineFieldRenderBox.Name = "mineFieldRenderBox";
            this.mineFieldRenderBox.Size = new System.Drawing.Size(780, 780);
            this.mineFieldRenderBox.TabIndex = 0;
            this.mineFieldRenderBox.TabStop = false;
            this.mineFieldRenderBox.Paint += new System.Windows.Forms.PaintEventHandler(this.mineFieldRenderBox_Paint);
            this.mineFieldRenderBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mineFieldRenderBox_MouseClick);
            this.mineFieldRenderBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.mineFieldRenderBox_MouseDoubleClick);
            // 
            // gameStartButton
            // 
            this.gameStartButton.Location = new System.Drawing.Point(373, 37);
            this.gameStartButton.Name = "gameStartButton";
            this.gameStartButton.Size = new System.Drawing.Size(43, 44);
            this.gameStartButton.TabIndex = 1;
            this.gameStartButton.UseVisualStyleBackColor = true;
            this.gameStartButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gameStartButton_MouseClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 900);
            this.Controls.Add(this.gameStartButton);
            this.Controls.Add(this.mineFieldRenderBox);
            this.Name = "Form1";
            this.Text = "CPP-Shooter\'s MineSweeper";
            ((System.ComponentModel.ISupportInitialize)(this.mineFieldRenderBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox mineFieldRenderBox;
        private System.Windows.Forms.Button gameStartButton;
    }
}

