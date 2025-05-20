using System;
using System.Drawing;
using System.Windows.Forms;

namespace GamesProject
{
    static class Program
    {
        /// <summary>
        /// ���ε{�����D�J�f�I�C
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
            // �]�m�����ݩ�
            this.Text = "�q���C���t��";
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // ���D����
            Label titleLabel = new Label
            {
                Text = "�w��Ө�q���C���t��",
                Font = new Font("Microsoft JhengHei UI", 16, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(350, 50),
                Location = new Point(25, 30)
            };

            // ��l��j�p���s
            Button diceGameButton = new Button
            {
                Text = "��l��j�p",
                Size = new Size(200, 50),
                Location = new Point(100, 100),
                Font = new Font("Microsoft JhengHei UI", 12)
            };
            diceGameButton.Click += (sender, e) =>
            {
                DiceGameForm diceGame = new DiceGameForm();
                diceGame.ShowDialog();
            };

            // �ŤM���Y�����s
            Button rpsGameButton = new Button
            {
                Text = "�ŤM���Y��",
                Size = new Size(200, 50),
                Location = new Point(100, 170),
                Font = new Font("Microsoft JhengHei UI", 12)
            };
            rpsGameButton.Click += (sender, e) =>
            {
                RockPaperScissorsForm rpsGame = new RockPaperScissorsForm();
                rpsGame.ShowDialog();
            };

            // �K�[����쵡��
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
            // �]�m�����ݩ�
            this.Text = "��l��j�p";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // ���a����
            Label playerLabel = new Label
            {
                Text = "���a��l",
                Font = new Font("Microsoft JhengHei UI", 12),
                Size = new Size(100, 30),
                Location = new Point(50, 20)
            };

            // �q������
            Label computerLabel = new Label
            {
                Text = "�q����l",
                Font = new Font("Microsoft JhengHei UI", 12),
                Size = new Size(100, 30),
                Location = new Point(350, 20)
            };

            // ���a��l�Ϲ�
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

            // �q����l�Ϲ�
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

            // �Y��l���s
            rollButton = new Button
            {
                Text = "�Y��l",
                Size = new Size(120, 40),
                Location = new Point(190, 150),
                Font = new Font("Microsoft JhengHei UI", 12)
            };
            rollButton.Click += RollDice;

            // ���G����
            resultLabel = new Label
            {
                Text = "���Y��l�}�l�C��",
                Font = new Font("Microsoft JhengHei UI", 12),
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(400, 30),
                Location = new Point(50, 210)
            };

            // ���Ƽ���
            scoreLabel = new Label
            {
                Text = "���a: 0  �q��: 0",
                Font = new Font("Microsoft JhengHei UI", 12),
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(200, 30),
                Location = new Point(150, 250)
            };

            // ���m���s
            Button resetButton = new Button
            {
                Text = "���m����",
                Size = new Size(120, 40),
                Location = new Point(190, 290),
                Font = new Font("Microsoft JhengHei UI", 12)
            };
            resetButton.Click += (sender, e) =>
            {
                playerScore = 0;
                computerScore = 0;
                UpdateScore();
                resultLabel.Text = "���Ƥw���m�I���Y��l�}�l�s�C��";
                ClearDiceImages();
            };

            // �K�[����쵡��
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
            // ���a��l
            int playerDice1Value = random.Next(1, 7);
            int playerDice2Value = random.Next(1, 7);
            int playerTotal = playerDice1Value + playerDice2Value;

            // �q����l
            int computerDice1Value = random.Next(1, 7);
            int computerDice2Value = random.Next(1, 7);
            int computerTotal = computerDice1Value + computerDice2Value;

            // ��ܻ�l�Ϲ�
            DisplayDiceImage(playerDice1, playerDice1Value);
            DisplayDiceImage(playerDice2, playerDice2Value);
            DisplayDiceImage(computerDice1, computerDice1Value);
            DisplayDiceImage(computerDice2, computerDice2Value);

            // ��ܵ��G
            string resultText = $"���a�I��: {playerTotal} vs �q���I��: {computerTotal} - ";

            if (playerTotal > computerTotal)
            {
                resultText += "���aĹ�F�I";
                playerScore++;
            }
            else if (playerTotal < computerTotal)
            {
                resultText += "�q��Ĺ�F�I";
                computerScore++;
            }
            else
            {
                resultText += "�����I";
            }

            resultLabel.Text = resultText;
            UpdateScore();
        }

        private void DisplayDiceImage(PictureBox pictureBox, int value)
        {
            // �Ыػ�l�I�ƪ��Ϲ�
            Bitmap bitmap = new Bitmap(60, 60);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
                g.DrawRectangle(Pens.Black, 0, 0, 59, 59);

                // �ھ��I��ø�s�I
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
            scoreLabel.Text = $"���a: {playerScore}  �q��: {computerScore}";
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
            // �]�m�����ݩ�
            this.Text = "�ŤM���Y��";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // ���a����
            Label playerLabel = new Label
            {
                Text = "���a���",
                Font = new Font("Microsoft JhengHei UI", 12),
                Size = new Size(100, 30),
                Location = new Point(50, 20)
            };

            // �q������
            Label computerLabel = new Label
            {
                Text = "�q�����",
                Font = new Font("Microsoft JhengHei UI", 12),
                Size = new Size(100, 30),
                Location = new Point(350, 20)
            };

            // ���a�����ܮ�
            playerChoiceBox = new PictureBox
            {
                Size = new Size(80, 80),
                Location = new Point(50, 60),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            // �q�������ܮ�
            computerChoiceBox = new PictureBox
            {
                Size = new Size(80, 80),
                Location = new Point(350, 60),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            // �ŤM���s
            scissorsButton = new Button
            {
                Text = "�ŤM",
                Size = new Size(100, 40),
                Location = new Point(60, 180),
                Font = new Font("Microsoft JhengHei UI", 12)
            };
            scissorsButton.Click += (sender, e) => PlayGame(1);

            // ���Y���s
            rockButton = new Button
            {
                Text = "���Y",
                Size = new Size(100, 40),
                Location = new Point(190, 180),
                Font = new Font("Microsoft JhengHei UI", 12)
            };
            rockButton.Click += (sender, e) => PlayGame(2);

            // �����s
            paperButton = new Button
            {
                Text = "��",
                Size = new Size(100, 40),
                Location = new Point(320, 180),
                Font = new Font("Microsoft JhengHei UI", 12)
            };
            paperButton.Click += (sender, e) => PlayGame(3);

            // ���G����
            resultLabel = new Label
            {
                Text = "�п�ܰŤM�B���Y�Υ��}�l�C��",
                Font = new Font("Microsoft JhengHei UI", 12),
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(400, 30),
                Location = new Point(50, 240)
            };

            // ���Ƽ���
            scoreLabel = new Label
            {
                Text = "���a: 0  �q��: 0",
                Font = new Font("Microsoft JhengHei UI", 12),
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(200, 30),
                Location = new Point(150, 280)
            };

            // ���m���s
            Button resetButton = new Button
            {
                Text = "���m����",
                Size = new Size(120, 40),
                Location = new Point(190, 320),
                Font = new Font("Microsoft JhengHei UI", 12)
            };
            resetButton.Click += (sender, e) =>
            {
                playerScore = 0;
                computerScore = 0;
                UpdateScore();
                resultLabel.Text = "���Ƥw���m�I�п�ܰŤM�B���Y�Υ��}�l�s�C��";
                playerChoiceBox.Image = null;
                computerChoiceBox.Image = null;
            };

            // �K�[����쵡��
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
            // �q���H�����
            int computerChoice = random.Next(1, 4);

            // ��ܿ�ܹϹ�
            DisplayChoiceImage(playerChoiceBox, playerChoice);
            DisplayChoiceImage(computerChoiceBox, computerChoice);

            // �P�_��Ĺ
            string resultText = $"���a���: {GetChoiceName(playerChoice)} vs �q�����: {GetChoiceName(computerChoice)} - ";

            if (playerChoice == computerChoice)
            {
                resultText += "�����I";
            }
            else if ((playerChoice == 1 && computerChoice == 3) || // �ŤM�ӥ�
                     (playerChoice == 2 && computerChoice == 1) || // ���Y�ӰŤM
                     (playerChoice == 3 && computerChoice == 2))   // ���ӥ��Y
            {
                resultText += "���aĹ�F�I";
                playerScore++;
            }
            else
            {
                resultText += "�q��Ĺ�F�I";
                computerScore++;
            }

            resultLabel.Text = resultText;
            UpdateScore();
        }

        private string GetChoiceName(int choice)
        {
            switch (choice)
            {
                case 1: return "�ŤM";
                case 2: return "���Y";
                case 3: return "��";
                default: return "";
            }
        }

        private void DisplayChoiceImage(PictureBox pictureBox, int choice)
        {
            // �Ыؿ�ܪ��Ϲ�
            Bitmap bitmap = new Bitmap(80, 80);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
                g.DrawRectangle(Pens.Black, 0, 0, 79, 79);

                switch (choice)
                {
                    case 1: // �ŤM
                        g.DrawLine(Pens.Black, 20, 40, 35, 55);
                        g.DrawLine(Pens.Black, 35, 55, 50, 40);
                        g.DrawLine(Pens.Black, 20, 30, 35, 45);
                        g.DrawLine(Pens.Black, 35, 45, 50, 30);
                        g.DrawLine(Pens.Black, 35, 45, 35, 60);
                        break;
                    case 2: // ���Y
                        g.FillEllipse(Brushes.Gray, 20, 20, 40, 40);
                        break;
                    case 3: // ��
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
            scoreLabel.Text = $"���a: {playerScore}  �q��: {computerScore}";
        }
    }
}