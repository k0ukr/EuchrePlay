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
        public static int CallWaitTime = 5;
        public static int PlayWaitTime = 9;
        public static string gAppPath;
        public const int TURNUP_INDEX = 21;

        // Typical Hands
        // Lowest Hand
        public static int LowestHand = ((int)Values.NineNoTrump * 3) + ((int)Values.TenNoTrump * 2);
        // 3 Suit Aces
        public static int ThreeNonTrumpAces = ((int)Values.AceNoTrump) * 3;
        // 1 Lowest Trump
        public static int LowestTrumpOne = (int)Values.NineTrump;
        // 2 Lowest Trump
        public static int LowestTrumpTwo = (int)Values.NineTrump + (int)Values.TenTrump;
        // 3 Lowest Trump
        public static int LowestTrumpThree = (int)Values.NineTrump + (int)Values.TenTrump + (int)Values.QueenTrump;
        // 4 Lowest Trump
        public static int LowestTrumpFour = (int)Values.NineTrump + (int)Values.TenTrump + (int)Values.QueenTrump + (int)Values.KingTrump;
        // All Trump: Lowest
        public static int LowestTrumpFive = LowestTrumpThree + (int)Values.KingTrump + (int)Values.AceTrump;
        // All Trump: Highest
        public static int HighestAllTrumpHand = (int)Values.RightBower + (int)Values.LeftBower + (int)Values.AceTrump + (int)Values.KingTrump + (int)Values.QueenTrump;
        // Left-Guarded Suit Ace
        public static int LeftGuardedSuitAce = (int)Values.LeftBower + (int)Values.NineTrump + (int)Values.AceNoTrump + (int)Values.NineNoTrump + (int)Values.TenNoTrump;

        public static int PointsToWin = 10;

        public enum  Team
        {
            TeamA,
            TeamB
        }

        public enum Suit
        {
            None,
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
        public const float VertDown = 180.0F;
        public const float HorzRight = 270.0F;
    }
}
