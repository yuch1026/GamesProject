using System;
using System.Drawing;
using System.Windows.Forms;

namespace GamesProject
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主入口點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }

    public class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            // 設置窗體屬性
            this.Text = "益智遊戲系統";
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // 標題標籤
            Label titleLabel = new Label
            {
                Text = "歡迎來到益智遊戲系統",
                Font = new Font("Microsoft JhengHei UI", 16, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(350, 50),
                Location = new Point(25, 30)
            };

            // 骰子比大小按鈕
            Button diceGameButton = new Button
            {
                Text = "骰子比大小",
                Size = new Size(200, 50),
                Location = new Point(100, 100),
                Font = new Font("Microsoft JhengHei UI", 12)
            };
            diceGameButton.Click += (sender, e) =>
            {
                DiceGameForm diceGame = new DiceGameForm();
                diceGame.ShowDialog();
            };

            // 剪刀石頭布按鈕
            Button rpsGameButton = new Button
            {
                Text = "剪刀石頭布",
                Size = new Size(200, 50),
                Location = new Point(100, 170),
                Font = new Font("Microsoft JhengHei UI", 12)
            };
            rpsGameButton.Click += (sender, e) =>
            {
                RockPaperScissorsForm rpsGame = new RockPaperScissorsForm();
                rpsGame.ShowDialog();
            };

            // 添加控件到窗體
            this.Controls.Add(titleLabel);
            this.Controls.Add(diceGameButton);
            this.Controls.Add(rpsGameButton);
        }
    }

    public class DiceGameForm : Form
    {
        private Label resultLabel;
        private PictureBox playerDice1;
        private PictureBox playerDice2;
        private PictureBox computerDice1;
        private PictureBox computerDice2;
        private Button rollButton;
        private Label scoreLabel;
        private int playerScore = 0;
        private int computerScore = 0;
        private Random random = new Random();

        public DiceGameForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            // 設置窗體屬性
            this.Text = "骰子比大小";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // 玩家標籤
            Label playerLabel = new Label
            {
                Text = "玩家骰子",
                Font = new Font("Microsoft JhengHei UI", 12),
                Size = new Size(100, 30),
                Location = new Point(50, 20)
            };

            // 電腦標籤
            Label computerLabel = new Label
            {
                Text = "電腦骰子",
                Font = new Font("Microsoft JhengHei UI", 12),
                Size = new Size(100, 30),
                Location = new Point(350, 20)
            };

            // 玩家骰子圖像
            playerDice1 = new PictureBox
            {
                Size = new Size(60, 60),
                Location = new Point(40, 60),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            playerDice2 = new PictureBox
            {
                Size = new Size(60, 60),
                Location = new Point(110, 60),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            // 電腦骰子圖像
            computerDice1 = new PictureBox
            {
                Size = new Size(60, 60),
                Location = new Point(330, 60),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            computerDice2 = new PictureBox
            {
                Size = new Size(60, 60),
                Location = new Point(400, 60),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            // 擲骰子按鈕
            rollButton = new Button
            {
                Text = "擲骰子",
                Size = new Size(120, 40),
                Location = new Point(190, 150),
                Font = new Font("Microsoft JhengHei UI", 12)
            };
            rollButton.Click += RollDice;

            // 結果標籤
            resultLabel = new Label
            {
                Text = "按擲骰子開始遊戲",
                Font = new Font("Microsoft JhengHei UI", 12),
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(400, 30),
                Location = new Point(50, 210)
            };

            // 分數標籤
            scoreLabel = new Label
            {
                Text = "玩家: 0  電腦: 0",
                Font = new Font("Microsoft JhengHei UI", 12),
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(200, 30),
                Location = new Point(150, 250)
            };

            // 重置按鈕
            Button resetButton = new Button
            {
                Text = "重置分數",
                Size = new Size(120, 40),
                Location = new Point(190, 290),
                Font = new Font("Microsoft JhengHei UI", 12)
            };
            resetButton.Click += (sender, e) =>
            {
                playerScore = 0;
                computerScore = 0;
                UpdateScore();
                resultLabel.Text = "分數已重置！按擲骰子開始新遊戲";
                ClearDiceImages();
            };

            // 添加控件到窗體
            this.Controls.Add(playerLabel);
            this.Controls.Add(computerLabel);
            this.Controls.Add(playerDice1);
            this.Controls.Add(playerDice2);
            this.Controls.Add(computerDice1);
            this.Controls.Add(computerDice2);
            this.Controls.Add(rollButton);
            this.Controls.Add(resultLabel);
            this.Controls.Add(scoreLabel);
            this.Controls.Add(resetButton);
        }

        private void RollDice(object sender, EventArgs e)
        {
            // 玩家骰子
            int playerDice1Value = random.Next(1, 7);
            int playerDice2Value = random.Next(1, 7);
            int playerTotal = playerDice1Value + playerDice2Value;

            // 電腦骰子
            int computerDice1Value = random.Next(1, 7);
            int computerDice2Value = random.Next(1, 7);
            int computerTotal = computerDice1Value + computerDice2Value;

            // 顯示骰子圖像
            DisplayDiceImage(playerDice1, playerDice1Value);
            DisplayDiceImage(playerDice2, playerDice2Value);
            DisplayDiceImage(computerDice1, computerDice1Value);
            DisplayDiceImage(computerDice2, computerDice2Value);

            // 顯示結果
            string resultText = $"玩家點數: {playerTotal} vs 電腦點數: {computerTotal} - ";

            if (playerTotal > computerTotal)
            {
                resultText += "玩家贏了！";
                playerScore++;
            }
            else if (playerTotal < computerTotal)
            {
                resultText += "電腦贏了！";
                computerScore++;
            }
            else
            {
                resultText += "平局！";
            }

            resultLabel.Text = resultText;
            UpdateScore();
        }

        private void DisplayDiceImage(PictureBox pictureBox, int value)
        {
            // 創建骰子點數的圖像
            Bitmap bitmap = new Bitmap(60, 60);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
                g.DrawRectangle(Pens.Black, 0, 0, 59, 59);

                // 根據點數繪製點
                switch (value)
                {
                    case 1:
                        g.FillEllipse(Brushes.Black, 25, 25, 10, 10);
                        break;
                    case 2:
                        g.FillEllipse(Brushes.Black, 10, 10, 10, 10);
                        g.FillEllipse(Brushes.Black, 40, 40, 10, 10);
                        break;
                    case 3:
                        g.FillEllipse(Brushes.Black, 10, 10, 10, 10);
                        g.FillEllipse(Brushes.Black, 25, 25, 10, 10);
                        g.FillEllipse(Brushes.Black, 40, 40, 10, 10);
                        break;
                    case 4:
                        g.FillEllipse(Brushes.Black, 10, 10, 10, 10);
                        g.FillEllipse(Brushes.Black, 40, 10, 10, 10);
                        g.FillEllipse(Brushes.Black, 10, 40, 10, 10);
                        g.FillEllipse(Brushes.Black, 40, 40, 10, 10);
                        break;
                    case 5:
                        g.FillEllipse(Brushes.Black, 10, 10, 10, 10);
                        g.FillEllipse(Brushes.Black, 40, 10, 10, 10);
                        g.FillEllipse(Brushes.Black, 25, 25, 10, 10);
                        g.FillEllipse(Brushes.Black, 10, 40, 10, 10);
                        g.FillEllipse(Brushes.Black, 40, 40, 10, 10);
                        break;
                    case 6:
                        g.FillEllipse(Brushes.Black, 10, 10, 10, 10);
                        g.FillEllipse(Brushes.Black, 40, 10, 10, 10);
                        g.FillEllipse(Brushes.Black, 10, 25, 10, 10);
                        g.FillEllipse(Brushes.Black, 40, 25, 10, 10);
                        g.FillEllipse(Brushes.Black, 10, 40, 10, 10);
                        g.FillEllipse(Brushes.Black, 40, 40, 10, 10);
                        break;
                }
            }
            pictureBox.Image = bitmap;
        }

        private void UpdateScore()
        {
            scoreLabel.Text = $"玩家: {playerScore}  電腦: {computerScore}";
        }

        private void ClearDiceImages()
        {
            playerDice1.Image = null;
            playerDice2.Image = null;
            computerDice1.Image = null;
            computerDice2.Image = null;
        }
    }

    public class RockPaperScissorsForm : Form
    {
        private Button rockButton;
        private Button paperButton;
        private Button scissorsButton;
        private Label resultLabel;
        private Label scoreLabel;
        private PictureBox playerChoiceBox;
        private PictureBox computerChoiceBox;
        private int playerScore = 0;
        private int computerScore = 0;
        private Random random = new Random();

        public RockPaperScissorsForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            // 設置窗體屬性
            this.Text = "剪刀石頭布";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // 玩家標籤
            Label playerLabel = new Label
            {
                Text = "玩家選擇",
                Font = new Font("Microsoft JhengHei UI", 12),
                Size = new Size(100, 30),
                Location = new Point(50, 20)
            };

            // 電腦標籤
            Label computerLabel = new Label
            {
                Text = "電腦選擇",
                Font = new Font("Microsoft JhengHei UI", 12),
                Size = new Size(100, 30),
                Location = new Point(350, 20)
            };

            // 玩家選擇顯示框
            playerChoiceBox = new PictureBox
            {
                Size = new Size(80, 80),
                Location = new Point(50, 60),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            // 電腦選擇顯示框
            computerChoiceBox = new PictureBox
            {
                Size = new Size(80, 80),
                Location = new Point(350, 60),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            // 剪刀按鈕
            scissorsButton = new Button
            {
                Text = "剪刀",
                Size = new Size(100, 40),
                Location = new Point(60, 180),
                Font = new Font("Microsoft JhengHei UI", 12)
            };
            scissorsButton.Click += (sender, e) => PlayGame(1);

            // 石頭按鈕
            rockButton = new Button
            {
                Text = "石頭",
                Size = new Size(100, 40),
                Location = new Point(190, 180),
                Font = new Font("Microsoft JhengHei UI", 12)
            };
            rockButton.Click += (sender, e) => PlayGame(2);

            // 布按鈕
            paperButton = new Button
            {
                Text = "布",
                Size = new Size(100, 40),
                Location = new Point(320, 180),
                Font = new Font("Microsoft JhengHei UI", 12)
            };
            paperButton.Click += (sender, e) => PlayGame(3);

            // 結果標籤
            resultLabel = new Label
            {
                Text = "請選擇剪刀、石頭或布開始遊戲",
                Font = new Font("Microsoft JhengHei UI", 12),
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(400, 30),
                Location = new Point(50, 240)
            };

            // 分數標籤
            scoreLabel = new Label
            {
                Text = "玩家: 0  電腦: 0",
                Font = new Font("Microsoft JhengHei UI", 12),
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(200, 30),
                Location = new Point(150, 280)
            };

            // 重置按鈕
            Button resetButton = new Button
            {
                Text = "重置分數",
                Size = new Size(120, 40),
                Location = new Point(190, 320),
                Font = new Font("Microsoft JhengHei UI", 12)
            };
            resetButton.Click += (sender, e) =>
            {
                playerScore = 0;
                computerScore = 0;
                UpdateScore();
                resultLabel.Text = "分數已重置！請選擇剪刀、石頭或布開始新遊戲";
                playerChoiceBox.Image = null;
                computerChoiceBox.Image = null;
            };

            // 添加控件到窗體
            this.Controls.Add(playerLabel);
            this.Controls.Add(computerLabel);
            this.Controls.Add(playerChoiceBox);
            this.Controls.Add(computerChoiceBox);
            this.Controls.Add(scissorsButton);
            this.Controls.Add(rockButton);
            this.Controls.Add(paperButton);
            this.Controls.Add(resultLabel);
            this.Controls.Add(scoreLabel);
            this.Controls.Add(resetButton);
        }

        private void PlayGame(int playerChoice)
        {
            // 電腦隨機選擇
            int computerChoice = random.Next(1, 4);

            // 顯示選擇圖像
            DisplayChoiceImage(playerChoiceBox, playerChoice);
            DisplayChoiceImage(computerChoiceBox, computerChoice);

            // 判斷輸贏
            string resultText = $"玩家選擇: {GetChoiceName(playerChoice)} vs 電腦選擇: {GetChoiceName(computerChoice)} - ";

            if (playerChoice == computerChoice)
            {
                resultText += "平局！";
            }
            else if ((playerChoice == 1 && computerChoice == 3) || // 剪刀勝布
                     (playerChoice == 2 && computerChoice == 1) || // 石頭勝剪刀
                     (playerChoice == 3 && computerChoice == 2))   // 布勝石頭
            {
                resultText += "玩家贏了！";
                playerScore++;
            }
            else
            {
                resultText += "電腦贏了！";
                computerScore++;
            }

            resultLabel.Text = resultText;
            UpdateScore();
        }

        private string GetChoiceName(int choice)
        {
            switch (choice)
            {
                case 1: return "剪刀";
                case 2: return "石頭";
                case 3: return "布";
                default: return "";
            }
        }

        private void DisplayChoiceImage(PictureBox pictureBox, int choice)
        {
            // 創建選擇的圖像
            Bitmap bitmap = new Bitmap(80, 80);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
                g.DrawRectangle(Pens.Black, 0, 0, 79, 79);

                switch (choice)
                {
                    case 1: // 剪刀
                        g.DrawLine(Pens.Black, 20, 40, 35, 55);
                        g.DrawLine(Pens.Black, 35, 55, 50, 40);
                        g.DrawLine(Pens.Black, 20, 30, 35, 45);
                        g.DrawLine(Pens.Black, 35, 45, 50, 30);
                        g.DrawLine(Pens.Black, 35, 45, 35, 60);
                        break;
                    case 2: // 石頭
                        g.FillEllipse(Brushes.Gray, 20, 20, 40, 40);
                        break;
                    case 3: // 布
                        g.FillRectangle(Brushes.LightBlue, 20, 20, 40, 40);
                        g.DrawLine(Pens.Black, 30, 30, 50, 30);
                        g.DrawLine(Pens.Black, 30, 40, 50, 40);
                        g.DrawLine(Pens.Black, 30, 50, 50, 50);
                        break;
                }
            }
            pictureBox.Image = bitmap;
        }

        private void UpdateScore()
        {
            scoreLabel.Text = $"玩家: {playerScore}  電腦: {computerScore}";
        }
    }
}