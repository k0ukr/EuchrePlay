using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace OnlineEuchre.Extras
{
    public static class Constants
    {
        public static string gAppPath;

        public enum Suit
        {
            Heart,
            Club,
            Diamond,
            Spade
        }

        public enum Rank
        {
            Nine,
            Ten,
            Jack,
            Queen,
            King,
            Ace,
            LeftBower,
            RightBower
        }

        public enum Values
        {
            NineNoTrump = 1,
            TenNoTrump = 2,
            JackNoTrump = 3,
            QueenNoTrump = 4,
            KingNoTrump = 5,
            AceNoTrump = 10,
            NineTrump = 12,
            TenTrump = 15,
            QueenTrump = 20,
            KingTrump = 25,
            AceTrump = 30,
            LeftBower = 31,
            RightBower = 35,
            NoValue = -1
        }

        public const float Horizontal = 90.0F;
        public const float Vertical = 0.0F;
    }
}
