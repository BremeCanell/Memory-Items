using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace Memory_Items
{
    public partial class Form1 : Form
    {
        private Host host;
        
        public Form1()
        {
            InitializeComponent();
            host = new Host();
            label1.Parent = pictureBox1;
            label2.Parent = pictureBox2;
            button1.Parent = pictureBox3;
            checkBox1.Parent = pictureBox2;
            checkBox2.Parent = pictureBox2;
            checkBox3.Parent = pictureBox2;
            checkBox4.Parent = pictureBox2;
            checkBox5.Parent = pictureBox2;
            checkBox6.Parent = pictureBox2;
            checkBox7.Parent = pictureBox2;
            checkBox8.Parent = pictureBox2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<CheckBox> checkBoxes = new List<CheckBox>();
            for (int i = 0; i < 8; i++)
            {
                CheckBox checkBox = Controls.Find("checkBox" + (i + 1), true).FirstOrDefault() as CheckBox;
                if (checkBox != null)
                {
                    checkBoxes.Add(checkBox);
                }
            }
            List<TextBox> textBoxes = new List<TextBox>();
            for (int i = 0; i < 8; i++)
            {
                TextBox textBox = Controls.Find("textBox" + (i + 1), true).FirstOrDefault() as TextBox;
                if (textBox != null)
                {
                    textBoxes.Add(textBox);
                }
            }

            host.Name = textBox9.Text;

            Match.Start_Match(this, checkBoxes, textBoxes);
        }
    }
}
