using OnlineEuchre.Extras;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEuchre.Classes
{
    public class Card
    {
        public Constants.Suit cSuit{ get; set; }
        public Constants.Rank cRank { get; set; }
        public Bitmap cImage { get; set; }
    }
}
