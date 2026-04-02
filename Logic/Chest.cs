using ISIP323_Khachatryan_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP323_Khachatryan_WPF.Logic
{
    public class Chest
    {
        private Random random;
        private List<Item> possibleItems;

        public Chest()
        {
            random = new Random();
            possibleItems = new List<Item>
            {
                new Potion("Святая вода"),
                new Potion("Эликсир жизни"),
                new Weapon("Кинжал", 8),
                new Weapon("Меч", 12),
                new Weapon("Топор", 15),
                new Weapon("Лук", 11),
                new Weapon("Двуручный меч", 18),
                new Armor("Кожаная броня", 6),
                new Armor("Кольчуга", 10),
                new Armor("Латные доспехи", 15),
                new Armor("Драконья чешуя", 20),
                new Armor("Броня костяного стража", 16)
            };
        }

        public Item Open()
        {
            return possibleItems[random.Next(possibleItems.Count)];
        }
    }
}
