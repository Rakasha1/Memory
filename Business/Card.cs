using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Card
    {
        public int ID { get; set; }
        public bool Turned { get; set; }
        public int Value { get; set; }
        public string? ImagePath { get; set; }
        public Card(int id, int value)
        {
            Value = value;
            ID = id;
            Turned = false;
        }

    }
}
