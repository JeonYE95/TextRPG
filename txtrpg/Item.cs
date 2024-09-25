using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace txtrpg
{
    internal class Item
    {
        public string ItemName { get; set; }
        public string ToolTip { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Price { get; set; }
        public bool Purchase { get; set; }

        public Item(string itemName, string toolTip, int attack, int defense, int price)
        {
            ItemName = itemName;
            ToolTip = toolTip;
            Attack = attack;
            Defense = defense;
            Price = price; 
            Purchase = false;
        }

    }
}
