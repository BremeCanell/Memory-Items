using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Memory_Items
{
    class Tray
    {
        private List<string> RandomItems = new List<string>();

        public List<string> GetRandomItems()
        {
            string[] imageFiles = Directory.GetFiles(@"C:\Users\User\source\repos\Memory Items\Memory Items\Items", "*.png");
            Random rnd = new Random();
            imageFiles = imageFiles.OrderBy(f => rnd.Next()).ToArray();
            for (int i = 0; i < Math.Min(20, imageFiles.Length); i++)
            {
                RandomItems.Add(Path.GetFileName(imageFiles[i]));
            }
            return RandomItems;
        }
    }
}
