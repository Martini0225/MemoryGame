namespace MemoryGame
{
    partial class frmMemoryGame
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mnsMemoryGame = new System.Windows.Forms.MenuStrip();
            this.tsmiGame = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRestart = new System.Windows.Forms.ToolStripMenuItem();
            this.timFold = new System.Windows.Forms.Timer(this.components);
            this.mnsMemoryGame.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnsMemoryGame
            // 
            this.mnsMemoryGame.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnsMemoryGame.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiGame});
            this.mnsMemoryGame.Location = new System.Drawing.Point(0, 0);
            this.mnsMemoryGame.Name = "mnsMemoryGame";
            this.mnsMemoryGame.Size = new System.Drawing.Size(682, 27);
            this.mnsMemoryGame.TabIndex = 0;
            this.mnsMemoryGame.Text = "menuStrip1";
            // 
            // tsmiGame
            // 
            this.tsmiGame.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRestart});
            this.tsmiGame.Name = "tsmiGame";
            this.tsmiGame.Size = new System.Drawing.Size(64, 23);
            this.tsmiGame.Text = "Game";
            // 
            // tsmiRestart
            // 
            this.tsmiRestart.Name = "tsmiRestart";
            this.tsmiRestart.Size = new System.Drawing.Size(141, 26);
            this.tsmiRestart.Text = "Restart";
            this.tsmiRestart.Click += new System.EventHandler(this.tsmiRestart_Click);
            // 
            // timFold
            // 
            this.timFold.Interval = 1000;
            this.timFold.Tick += new System.EventHandler(this.timFold_Tick);
            // 
            // frmMemoryGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 653);
            this.Controls.Add(this.mnsMemoryGame);
            this.MainMenuStrip = this.mnsMemoryGame;
            this.Name = "frmMemoryGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "記憶翻牌";
            this.Load += new System.EventHandler(this.frmMemoryGame_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmMemoryGame_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.frmMemoryGame_MouseClick);
            this.mnsMemoryGame.ResumeLayout(false);
            this.mnsMemoryGame.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnsMemoryGame;
        private System.Windows.Forms.ToolStripMenuItem tsmiGame;
        private System.Windows.Forms.ToolStripMenuItem tsmiRestart;
        private System.Windows.Forms.Timer timFold;
    }
}

