using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Memory_Items
{
    public partial class Form2 : Form
    {
        private List<string> textBoxValues;
        private List<string> randomItems;
        private Timer timer;
        Match match = new Match();

        public Form2(List<string> textBoxValues)
        {
            InitializeComponent();
            this.textBoxValues = textBoxValues;

            Tray tray = new Tray();
            randomItems = tray.GetRandomItems();
                        
            match.Show_Items(randomItems, this);

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Next_Page;
            timer.Start();

            Match.Show_Timer(label2, match.Show_time);
        }

        public void Next_Page(object sender, EventArgs e)
        {
            match.Timer_Tick(sender, e);

            if (match.Show_time <= 0)
            {
                timer.Stop();
                Form3 form3 = new Form3(textBoxValues, randomItems);
                form3.Show();
                this.Hide();
            }

            Match.Show_Timer(label2, match.Show_time);
        }

        
    }
}
