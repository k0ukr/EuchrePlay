using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEuchre.Extras
{
    public static class Randomizer
    {
        private static Byte[] randomNumber;
        private static System.Security.Cryptography.RNGCryptoServiceProvider Gen;
        public static int GenRandomNumber(int maxVal)
        {
            Gen.GetBytes(randomNumber);
            int rand = Convert.ToInt32(randomNumber);
            return rand % maxVal;
        }
        /*
         * -------------------------------------------------------------------------------------------
         * This class will contain a list of number is random order 
         * -------------------------------------------------------------------------------------------
         */
        public static class RandomMod
        {
            static Random random = new Random();

            private static HashSet<int> hsRandom = new HashSet<int>();
            public static List<int> GetRandomList(int max)
            {
                try
                {
                    hsRandom.Clear();
                    int rnd;
                    int Count = (int)(max);
                    int NeedCount = Count;
                    do
                    {
                        rnd = GetNext(max);
                        if (!hsRandom.Contains(rnd))
                        {
                            hsRandom.Add(rnd);
                            NeedCount--;
                        }
                    } while (NeedCount > 0);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Random: {ex.Message}");
                }
                return hsRandom.ToList();
            }

            public static double GetRandomInt(int min, int max)
            {
                double rndValue = random.NextDouble();
                return rndValue;
                //  return min + (rndValue * (max - min));
            }

            public static double GetRandomDouble(double min, double max)
            {
                return min + (random.NextDouble() * (max - min));
            }

            // Jon Skeet
            public static int GetNext(int max)
            {
                return (Int32)(1 + (random.NextDouble() * (++max - 1)));
            }
        }
    }
}
