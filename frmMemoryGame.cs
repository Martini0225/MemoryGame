using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace MemoryGame
{
    
    public partial class frmMemoryGame : Form
    {
        // 【新增】預覽用的計時器與狀態
        Timer timStartPreview = new Timer();
        bool isPreviewing = false; // 用來記錄現在是不是「開牌讓玩家記」的狀態

        Rectangle[] rects = new Rectangle[16];
        Image[] gridImages = new Image[16];   // 每格真正放的圖片
        Image[] fruitImages;
        Image backImage;
        Random rd = new Random();
        bool[] revealed = new bool[16];       // 每格是否被翻開
        int firstIndex = -1;                   // 第一次翻的格子
        int secondIndex = -1;                  // 第二次翻的格子

        // 【新增】宣告音效播放器
        SoundPlayer playerSuccess = new SoundPlayer(Properties.Resources.bibilabu);
        SoundPlayer playerFail = new SoundPlayer(Properties.Resources.bakayaro);
        

        public frmMemoryGame()
        {
            InitializeComponent();

            // 【新增】綁定預覽計時器的事件
            timStartPreview.Tick += timStartPreview_Tick;

            int index = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    rects[index] = new Rectangle(j * 120, 25 + i * 120, 120, 120);

                    index++;
                }
            }
        }

        private void frmMemoryGame_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < 16; i++)
            {

                if (revealed[i])
                    e.Graphics.DrawImage(gridImages[i], rects[i]);
                else
                    e.Graphics.DrawImage(backImage, rects[i]);

                e.Graphics.DrawRectangle(Pens.Black, rects[i]);

            }
        }

        private void frmMemoryGame_Load(object sender, EventArgs e)
        {
            backImage = Properties.Resources.Back;
            fruitImages = new Image[]
            {
                Properties.Resources.shiba1,
                Properties.Resources.shiba2,
                Properties.Resources.shiba3,
                Properties.Resources.shiba4,
                Properties.Resources.cat1,
                Properties.Resources.cat2,
                Properties.Resources.cat3,
                Properties.Resources.cat4
            };
            // 建立兩張一樣的圖（16 張）
            List<Image> list = new List<Image>();
            foreach (var img in fruitImages)
            {
                list.Add(img);
                list.Add(img);
            }

            // 隨機塞到格子
            for (int i = 0; i < 16; i++)
            {
                int idx = rd.Next(list.Count);
                gridImages[i] = list[idx];
                list.RemoveAt(idx);
            }


            // 設定 timFold（蓋回的 1 秒）
            timFold.Interval = 1000;
            //【新增】直接呼叫 RestartGame()
            // 這樣一開局就會自動洗牌、全開，並啟動 3 秒的預覽計時器
            RestartGame();
        }

        private void frmMemoryGame_MouseClick(object sender, MouseEventArgs e)
        {
            // 【新增】如果正在預覽中，直接略過滑鼠點擊
            if (isPreviewing) return;

            if (timFold.Enabled) return; // 正在等待蓋回

            for (int i = 0; i < 16; i++)
            {
                if (rects[i].Contains(e.Location) && !revealed[i])
                {
                    revealed[i] = true;

                    if (firstIndex == -1)
                    {
                        firstIndex = i;
                    }
                    else
                    {
                        secondIndex = i;

                        if (gridImages[firstIndex] != gridImages[secondIndex])
                        {
                            // 不同 → 播放失敗音效，並啟動蓋回 timFold
                            try { playerFail.Play(); } catch { }
                            timFold.Start();
                        }
                        else
                        {
                            // 一樣 → 播放成功音效，並保持翻開
                            try { playerSuccess.Play(); } catch { }
                            firstIndex = -1;
                            secondIndex = -1;

                           
                            // 過關判斷邏輯
                            // 使用 System.Linq 的 All 方法檢查是否全部都被翻開了
                            if (revealed.All(r => r == true))
                            {
                                
                                // 然後再跳出過關訊息視窗
                                this.BeginInvoke((MethodInvoker)delegate
                                {
                                    DialogResult result = MessageBox.Show(
                                        "恭喜你完成遊戲！\n要再挑戰一次嗎？",
                                        "過關提示",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Information);

                                    if (result == DialogResult.Yes)
                                    {
                                        RestartGame(); // 玩家按「是」就重新開始
                                    }
                                });
                            }
                            
                        }
                    }

                    Invalidate();
                    return;
                }
            }
        }

        private void timFold_Tick(object sender, EventArgs e)
        {
            timFold.Stop();

            // 蓋回
            revealed[firstIndex] = false;
            revealed[secondIndex] = false;

            firstIndex = -1;
            secondIndex = -1;

            Invalidate(); // 重畫
        }

        private void tsmiRestart_Click(object sender, EventArgs e)
        {
            RestartGame();
        }

        // 重新開始
        private void RestartGame()
        {
            timFold.Stop();
            timStartPreview.Stop(); // 確保之前的預覽計時器停下來

            firstIndex = -1;
            secondIndex = -1;

            List<Image> list = new List<Image>();
            foreach (var img in fruitImages)
            {
                list.Add(img);
                list.Add(img);
            }

            for (int i = 0; i < 16; i++)
            {
                int idx = rd.Next(list.Count);
                gridImages[i] = list[idx];
                list.RemoveAt(idx);

                // 一開始先設為 true，讓玩家看得到牌
                revealed[i] = true;
            }

            // 進入預覽狀態，設定看牌時間為 3 秒 
            isPreviewing = true;
            timStartPreview.Interval = 3000;
            timStartPreview.Start();

            Invalidate();
        }

        // 預覽時間結束時執行的動作
        private void timStartPreview_Tick(object sender, EventArgs e)
        {
            timStartPreview.Stop(); // 停止預覽計時

            // 把所有牌蓋回
            for (int i = 0; i < 16; i++)
            {
                revealed[i] = false;
            }

            isPreviewing = false; // 解除預覽狀態，開放玩家點擊
            Invalidate();         // 重畫畫面 (呈現蓋牌狀態)
        }
    }
}
