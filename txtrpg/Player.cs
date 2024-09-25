using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace txtrpg
{
    public class Player
    {
        public int level { get; set; }
        public string name { get; set; }
        public string job { get; set; }
        public int atk { get; set; }
        public int def { get; set; }
        public int hp { get; set; }
        public int gold { get; set; }
        public bool equipitematk { get; set; }
        public bool equipitemdef { get; set; }

        public Player(string Name, int Level, string Job, int Atk, int Def, int Hp, int Gold)
        {
            name = Name;
            level = Level;
            job = Job;
            atk = Atk;
            def = Def;
            hp = Hp;
            gold = Gold;
            equipitematk = false;
            equipitemdef = false;
        }
    }
}