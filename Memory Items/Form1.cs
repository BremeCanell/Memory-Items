using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Memory_Items
{
    public partial class Form1 : Form
    {
        private Host host;

        public Form1()
        {
            InitializeComponent();
            host = new Host();
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

            host.Value = textBox9.Text;

            Match.Start_Match(this, host, checkBoxes, textBoxes);
        }
    }
}
