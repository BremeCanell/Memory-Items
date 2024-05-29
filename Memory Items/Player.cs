using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Memory_Items
{
    class Player
    {
        public string Name { get; set; }
        public List<string> SelectedValues { get; set; }

        public Player()
        {
            SelectedValues = new List<string>();
        }

        public void CreatePlayer(List<TextBox> textBoxes)
        {
            SelectedValues = textBoxes
                .Where(tb => !string.IsNullOrEmpty(tb.Text))
                .Select(tb => tb.Text)
                .ToList();
        }
    }
}
