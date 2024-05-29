using System.IO;

namespace Memory_Items
{
    class Items
    {
        string Items_name = @"C:\Users\User\source\repos\Memory Items\Memory Items\Items";
        public string[] Get_All_Items()
        {
            
            return Directory.GetFiles(Items_name, "*.png");
        }
    }
}
