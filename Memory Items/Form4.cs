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

            Button button = new Button
            {
                Text = "Новая игра",
                Font = new Font("Microsoft Sans Serif", 14),
                Width = 216,
                Height = 60,
                Location = new Point(200, 440)
            };
            button.Click += (s, e) =>
            {
                Form1 form1 = new Form1();
                form1.Show();
                this.Hide();
            };
            this.Controls.Add(button);
        }
    }
}
