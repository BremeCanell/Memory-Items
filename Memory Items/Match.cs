using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Memory_Items
{
    class Match
    {
        public int Show_time { get; set; }

        public Match()
        {
            Show_time = 5;
        }
        class ParticipantResult
        {
            public string Name { get; set; }
            public int CorrectAnswers { get; set; }
        }

        public static void Start_Match(Form1 form, List<CheckBox> checkBoxes, List<TextBox> textBoxes)
        {
            bool areFieldsValid = true;
            HashSet<string> uniqueValues = new HashSet<string>();

            for (int i = 0; i < checkBoxes.Count; i++)
            {
                if (checkBoxes[i].Checked)
                {
                    if (string.IsNullOrEmpty(textBoxes[i].Text))
                    {
                        areFieldsValid = false;
                        break;
                    }
                    if (!uniqueValues.Add(textBoxes[i].Text))
                    {
                        MessageBox.Show("Обнаружено повторяющееся значение.");
                        return;
                    }
                }
            }
            if (!areFieldsValid)
            {
                MessageBox.Show("Заполните все необходимые поля.");
                return;
            }
            if (checkBoxes.Any(cb => cb.Checked))
            {
                Player player = new Player();
                player.CreatePlayer(textBoxes);
                Form2 form2 = new Form2(player.SelectedValues);
                form2.Show();
                form.Hide();
            }
            else
            {
                MessageBox.Show("Заполните все необходимые поля.");
            }
        }

        public void Show_Items(List<string> itemPaths, Form form)
        {
            for (int i = 0; i < Math.Min(20, itemPaths.Count); i++)
            {
                PictureBox pictureBox = form.Controls.Find("pictureBox" + (i + 1), true).FirstOrDefault() as PictureBox;
                pictureBox.Image = Image.FromFile(@"C:\Users\User\source\repos\Memory Items\Memory Items\Items\" + itemPaths[i]);
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        public List<int> Check_Winner(List<int> clicksCount)
        {
            int maxValue = clicksCount.Max();
            List<int> maxIndexes = new List<int>();

            for (int i = 0; i < clicksCount.Count; i++)
            {
                if (clicksCount[i] == maxValue)
                    maxIndexes.Add(i);
            }

            return maxIndexes;
        }

        public void Show_Winner(Form form, List<int> clicksCount, List<string> textBoxValues)
        {

            List<int> maxIndexes = Check_Winner(clicksCount);

            PictureBox pictureBox1 = new PictureBox
            {
                Width = 574,
                Height = 394,
                Location = new Point(225, 130),
                BackColor = Color.Transparent,
                Image = Image.FromFile(@"C:\Users\User\source\repos\Memory Items\Memory Items\Back\VeryBoard.png"),
                SizeMode = PictureBoxSizeMode.StretchImage,
            };
            form.Controls.Add(pictureBox1);

            Label allResultsLabel = new Label
            {
                BackColor = Color.Transparent,
                Text = "Результаты всех участников:",
                Font = new Font("Segoe Print", 15),
                AutoSize = true,
                Location = new Point(94, 25)
            };
            pictureBox1.Controls.Add(allResultsLabel);

            List<ParticipantResult> participantResults = new List<ParticipantResult>();

            for (int i = 0; i < textBoxValues.Count; i++)
            {
                participantResults.Add(new ParticipantResult
                {
                    Name = textBoxValues[i],
                    CorrectAnswers = clicksCount[i]
                });
            }

            participantResults = participantResults.OrderByDescending(x => x.CorrectAnswers).ToList();

            for (int i = 0; i < participantResults.Count; i++)
            {
                Label participantResultLabel = new Label
                {
                    BackColor = Color.Transparent,
                    TextAlign = ContentAlignment.TopCenter,
                    Text = $"{participantResults[i].Name}: {participantResults[i].CorrectAnswers}",
                    Font = new Font("Segoe Print", 17),
                    AutoSize = true,
                    Location = new Point(100, 60 + i * 35)
                };
                pictureBox1.Controls.Add(participantResultLabel);
            }
        }
    
        public static void Show_Timer(Label label, int Show_time)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(Show_time);
            label.Text = timeSpan.ToString(@"ss");
        }

        public void Timer_Tick(object sender, EventArgs e)
        {
            Show_time--;
        }
    }
}
