using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OnlineEuchre.Extras.Constants;

namespace OnlineEuchre.Extras
{
    public static class Globals
    {
        // This will get set in AskForm
        public static bool TrumpCalled = false;
        public static bool PersonPassed = false;
        public static Suit TrumpSuit { get; set; }
}
}
        