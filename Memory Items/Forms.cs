using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Memory_Items
{
    class Forms
    {
        public void Send_Form(TabPage tabPage, List<int> clicksCount, List<string> tags, List<string> randomItems, TabControl tabControl1)
        {
            int tabClicks = tags.Intersect(randomItems).Count();
            clicksCount.Add(tabClicks);

            tabControl1.TabPages.Remove(tabPage);
        }

        public void Select_Items(string tag, List<string> tags)
        {
            tags.Add(tag);
        }

        public void Deselect_items(string tag, List<string> tags)
        {
            tags.Remove(tag);
        }
    }
}
