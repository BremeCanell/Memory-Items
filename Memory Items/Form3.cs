using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Memory_Items
{
    public partial class Form3 : Form
    {
        private List<int> clicksCount = new List<int>();
        private List<string> textBoxValues;
        private List<string> randomItems;
        private Forms Forms;
        private Items Items;

        private Dictionary<TabPage, Timer> tabTimers = new Dictionary<TabPage, Timer>();
        private Dictionary<TabPage, Label> tabTimerLabels = new Dictionary<TabPage, Label>();
        private Dictionary<TabPage, int> tabTimeLeft = new Dictionary<TabPage, int>();

        public Form3(List<string> textBoxValues, List<string> randomItems)
        {
            InitializeComponent();
            this.textBoxValues = textBoxValues;
            this.randomItems = randomItems;
            this.Forms = new Forms();
            this.Items = new Items();
            CreateTabs();

            tabControl1.SelectedIndexChanged += (s, e) =>
            {
                if (tabControl1.SelectedTab != null)
                    tabControl1.SelectedTab = tabControl1.TabPages[0];
            };

            if (tabControl1.TabPages.Count > 0)
            {
                var initialTab = tabControl1.TabPages[0];
                ResetAndStartTimer(initialTab);
            }
        }

        private void CreateTabs()
        {
            foreach (var value in textBoxValues)
            {
                TabPage tabPage = new TabPage(value);
                string[] imageFiles = Items.Get_All_Items();
                List<string> tags = new List<string>();
                tags.Clear();

                tabPage.BackgroundImage = Image.FromFile(@"C:\Users\User\source\repos\Memory Items\Memory Items\Back\1688589300_bogatyr-club-p-derevyannii-parket-tekstura-foni-instagram-10.jpg");
                tabPage.BackgroundImageLayout = ImageLayout.Stretch;

                int selectedCount = 0;

                for (int j = 0; j < 40; j++)
                {
                    PictureBox pictureBox = new PictureBox
                    {
                        Width = 110,
                        Height = 80,
                        BackColor = Color.White,
                    BorderStyle = BorderStyle.FixedSingle,
                        Location = new Point(28 + (j % 8) * 120, 10 + (j / 8) * 90),
                        Image = Image.FromFile(imageFiles[j]),
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Tag = Path.GetFileName(imageFiles[j]),
                    };

                    pictureBox.Click += (s, e) =>
                    {
                        PictureBox pb = s as PictureBox;
                        bool isChecked = tags.Contains(pb.Tag.ToString());

                        if (!isChecked && selectedCount >= 20)
                            return;

                        if (!isChecked)
                        {
                            Forms.Select_Items(pb.Tag.ToString(), tags);
                            selectedCount++;
                        }
                        else
                        {
                            Forms.Deselect_items(pb.Tag.ToString(), tags);
                            selectedCount--;
                        }

                        if (isChecked)
                        {
                            pb.BorderStyle = BorderStyle.FixedSingle;
                            pb.BackColor = Color.White;
                            pb.Padding = new Padding(0);
                        }
                        else
                        {
                            pb.BorderStyle = BorderStyle.Fixed3D;
                            pb.BackColor = Color.LightBlue;
                            pb.Padding = new Padding(5);
                        }
                    };

                    tabPage.Controls.Add(pictureBox);
                }

                PictureBox pictureBox1 = new PictureBox
                {
                    Width = 207,
                    Height = 116,
                    Location = new Point(791, 470),
                    BackColor = Color.Transparent,
                    Image = Image.FromFile(@"C:\Users\User\source\repos\Memory Items\Memory Items\Back\SmallBoard.png"),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                };
                tabPage.Controls.Add(pictureBox1);

                Label timerLabel = new Label
                {
                    Font = new Font("Segoe Print", 25),
                    Text = "",
                    Width = 100,
                    Height = 70,
                    Location = new Point(63, 23),
                    ForeColor = Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(16)))), ((int)(((byte)(13))))),
                    BackColor = Color.Transparent
                };
                pictureBox1.Controls.Add(timerLabel);

                PictureBox pictureBox2 = new PictureBox
                {
                    Width = 296,
                    Height = 86,
                    Location = new Point(363, 500),
                    BackColor = Color.Transparent,
                    Image = Image.FromFile(@"C:\Users\User\source\repos\Memory Items\Memory Items\Back\ShortBoard.png"),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    TabStop = false,
                };
                tabPage.Controls.Add(pictureBox2);

                Button button = new Button
                {
                    Font = new Font("Segoe Print", 15),
                    Text = "Отправить",
                    Width = 247,
                    Height = 72,
                    Location = new Point(25, 8),
                    BackColor = System.Drawing.Color.Transparent,
                    FlatStyle = FlatStyle.Popup,
                };
                button.Click += (s, e) => SaveAndCloseTab(tabPage, tags);
                pictureBox2.Controls.Add(button);

                tabControl1.TabPages.Add(tabPage);

                Timer timer = new Timer
                {
                    Interval = 1000 
                };

                timer.Tick += (s, e) =>
                {
                    tabTimeLeft[tabPage]--;
                    if (tabTimeLeft[tabPage] > 9)
                    {
                        timerLabel.Text = $"{tabTimeLeft[tabPage]}";
                    }
                    else
                    {
                        timerLabel.Text = $"0{tabTimeLeft[tabPage]}";
                    }
                    

                    if (tabTimeLeft[tabPage] <= 0)
                    {
                        timer.Stop();
                        button.PerformClick();
                    }
                };

                tabTimers[tabPage] = timer;
                tabTimerLabels[tabPage] = timerLabel;
                tabTimeLeft[tabPage] = 20;
            }
        }

        private void ResetAndStartTimer(TabPage tabPage)
        {
            if (tabTimers.ContainsKey(tabPage))
            {
                tabTimeLeft[tabPage] = 20;
                tabTimerLabels[tabPage].Text = $"{tabTimeLeft[tabPage]}";
                tabTimers[tabPage].Start();
            }
        }

        private void SaveAndCloseTab(TabPage tabPage, List<string> tags)
        {
            Forms.Send_Form(tabPage, clicksCount, tags, randomItems, tabControl1);
            tabControl1.TabPages.Remove(tabPage);

            if (tabControl1.TabPages.Count > 0)
            {
                tabTimers[tabPage].Stop();
                tabControl1.SelectedIndex = 0;
                var nextTab = tabControl1.TabPages[0];
                ResetAndStartTimer(nextTab);
            }
            else
            {
                tabTimers[tabPage].Stop();
                Form4 form4 = new Form4(clicksCount, textBoxValues);
                form4.Show();
                this.Close();
            }
        }
    }
}
