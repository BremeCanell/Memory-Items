using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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

        public Form3(List<string> textBoxValues, List<string> randomItems)
        {
            InitializeComponent();
            this.textBoxValues = textBoxValues;
            this.randomItems = randomItems;
            this.Forms = new Forms();
            this.Items = new Items();
            CreateTabs();
        }

        private void CreateTabs()
        {
             foreach (var value in textBoxValues)
            {
                TabPage tabPage = new TabPage(value);
                string[] imageFiles = Items.Get_All_Items();
                List<string> tags = new List<string>();
                tags.Clear();

                for (int j = 0; j < 40; j++)
                {
                    PictureBox pictureBox = new PictureBox
                    {
                        Width = 110,
                        Height = 80,
                        BorderStyle = BorderStyle.FixedSingle,
                        Location = new Point(10 + (j % 8) * 120, 10 + (j / 8) * 90),
                        Image = Image.FromFile(imageFiles[j]),
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Tag = Path.GetFileName(imageFiles[j]),
                    };

                    pictureBox.Click += (s, e) =>
                    {
                        PictureBox pb = s as PictureBox;
                        bool isChecked = tags.Contains(pb.Tag.ToString());

                        int selectedCount = tabPage.Controls.OfType<PictureBox>().Count(p => tags.Contains(p.Tag.ToString()));
                        if (isChecked && selectedCount >= 20)
                            return;

                        if (!isChecked)
                        {
                            Forms.Select_Items(pb.Tag.ToString(), tags);
                        }
                        else
                        {
                            Forms.Deselect_items(pb.Tag.ToString(), tags);
                        }

                        pb.BorderStyle = isChecked ? BorderStyle.FixedSingle : BorderStyle.Fixed3D;
                    };
                    tabPage.Controls.Add(pictureBox);
                }

                Button button = new Button
                {
                    Font = new Font("Microsoft Sans Serif", 14),
                    Text = "Отправить",
                    Width = 216,
                    Height = 60,
                    Location = new Point(376, 480)
                };
                button.Click += (s, e) => SaveAndCloseTab(tabPage, tags);
                tabPage.Controls.Add(button);

                tabControl1.TabPages.Add(tabPage);
            }
        }

        private void SaveAndCloseTab(TabPage tabPage, List<string> tags)
        {
            Forms.Send_Form(tabPage, clicksCount, tags, randomItems, tabControl1);

            if (tabControl1.TabPages.Count == 0)
            {
                Form4 form4 = new Form4(clicksCount, textBoxValues);
                form4.Show();
                this.Hide();
            }
        }
    }
}
