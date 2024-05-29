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
            Show_time = 10;
        }


        public static void Start_Match(Form1 form, Host host, List<CheckBox> checkBoxes, List<TextBox> textBoxes)
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

            Label label = new Label
            {
                Text = maxIndexes.Count == 1 ? "Победитель" : "Победители:",
                Font = new Font("Microsoft Sans Serif", 14),
                AutoSize = true,
                Location = new Point(200, 150)
            };
            form.Controls.Add(label);

            for (int i = 0; i < maxIndexes.Count; i++)
            {
                Label resultLabel = new Label
                {
                    Text = $"{textBoxValues[maxIndexes[i]]}: {clicksCount[maxIndexes[i]]}",
                    Font = new Font("Microsoft Sans Serif", 14),
                    AutoSize = true,
                    Location = new Point(200, 180 + i * 30)
                };
                form.Controls.Add(resultLabel);
            }

            Label allResultsLabel = new Label
            {
                Text = "Результаты всех участников:",
                Font = new Font("Microsoft Sans Serif", 14),
                AutoSize = true,
                Location = new Point(600, 150)
            };
            form.Controls.Add(allResultsLabel);

            for (int i = 0; i < clicksCount.Count; i++)
            {
                Label participantResultLabel = new Label
                {
                    Text = $"{textBoxValues[i]}: {clicksCount[i]}",
                    Font = new Font("Microsoft Sans Serif", 14),
                    AutoSize = true,
                    Location = new Point(600, 180 + i * 30)
                };
                form.Controls.Add(participantResultLabel);
            }
        }
    

        public static void Show_Timer(Label label, int Show_time)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(Show_time);
            label.Text = timeSpan.ToString(@"mm\:ss");
        }

        public void Timer_Tick(object sender, EventArgs e)
        {
            Show_time--;
        }
    }
}
