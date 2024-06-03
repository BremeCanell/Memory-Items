using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Memory_Items
{
    public partial class Form4 : Form
    {
        public Form4(List<int> clicksCount, List<string> textBoxValues)
        {
            InitializeComponent();
            
            Match match = new Match();
            match.Show_Winner(this, clicksCount, textBoxValues);

            PictureBox pictureBox1 = new PictureBox
            {
                Width = 296,
                Height = 86,
                Location = new Point(363, 535),
                BackColor = Color.Transparent,
                Image = Image.FromFile(@"C:\Users\User\source\repos\Memory Items\Memory Items\Back\ShortBoard.png"),
                SizeMode = PictureBoxSizeMode.StretchImage,
                TabStop = false,
            };
            this.Controls.Add(pictureBox1);

            Button button = new Button
            {
                Parent = pictureBox1,
                Font = new Font("Segoe Print", 15),
                Text = "Новая игра",
                Width = 247,
                Height = 72,
                Location = new Point(25, 8),
                BackColor = System.Drawing.Color.Transparent,
                FlatStyle = FlatStyle.Popup,
            };
            button.Click += (s, e) =>
            {
                Form1 form1 = new Form1();
                form1.Show();
                this.Close();
            };
            pictureBox1.Controls.Add(button);
        }
    }
}
