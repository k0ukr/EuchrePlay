using OnlineEuchre.Extras;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OnlineEuchre.Extras.Constants;

namespace OnlineEuchre.Classes
{
    public static class LoadCards
    {
        private static IDictionaryEnumerator myEnumerator = null;
        public static Bitmap ClubTrump = null;
        public static Bitmap DiamondTrump = null;
        public static Bitmap CallCoin = null;
        public static Bitmap HeartTrump = null;
        public static Bitmap SpadeTrump = null;
        public static Bitmap CardBack = null;
        public static Bitmap DealerCoin = null;
        public static Bitmap ArrowCoin = null;
        public static Dictionary<string, Card> dictCard = new Dictionary<string, Card>();

        public static void LoadImages(string cardPath)
        {
            string imagePath = Path.Combine(cardPath, "Images");
            List<string> allFiles = CommonMod.GetFilesFromPath(imagePath);
            int index = 0;
            foreach (string file in allFiles)
            {
                index++;
                string filename = Path.GetFileName(file);
                string root = filename.Split('.')[0];
                string[] parts = root.Split('_');
                if (parts[0] == "X")
                {
                    switch (parts[1])
                    {
                        case "CardBack":
                            CardBack = (Bitmap)Image.FromFile(file);
                            break;
                        case "Dealer":
                            DealerCoin = (Bitmap)Image.FromFile(file);
                            break;
                        case "Call":
                            CallCoin = (Bitmap)Image.FromFile(file);
                            break;
                        case "Arrow":
                            ArrowCoin = (Bitmap)Image.FromFile(file);
                            break;
                        case "Club":
                            ClubTrump = (Bitmap)Image.FromFile(file);
                            break;
                        case "Diamond":
                            DiamondTrump = (Bitmap)Image.FromFile(file);
                            break;
                        case "Heart":
                            HeartTrump = (Bitmap)Image.FromFile(file);
                            break;
                        case "Spade":
                            SpadeTrump = (Bitmap)Image.FromFile(file);
                            break;
                    }
                }
                else
                {
                    Card cCard = new Card();
                    cCard.cImage = (Bitmap)Image.FromFile(file);

                    cCard.cRank = (Rank) Enum.Parse(typeof(Rank), parts[0], true);
                    cCard.cSuit = (Suit) Enum.Parse(typeof(Suit), parts[1], true);
                    dictCard.Add(root, cCard);
                }
                //Console.WriteLine(file);
            }
        }

        public static Card GetFirst()
        {
            myEnumerator = dictCard.GetEnumerator();
            return GetNext();
        }

        public static Card GetNext()
        {
            try
            {
                myEnumerator.MoveNext();
                return (Card)myEnumerator.Value;
            }
            catch ( Exception ex)
            {
                throw new Exception("Moved past end of dictCard");
            }
        }

        public static Card GetByElement(int index)
        {
            try
            {
                return (Card)dictCard.Values.ElementAt(index);
            }
            catch (Exception ex)
            {
                throw new Exception("Moved past end of dictCard");
            }
        }
        public static string GetImageResourceName(Rank cRank, Suit cSuit)
        {
            return $"{cRank.ToString()}_{cSuit.ToString()}.jpg";
        }

        // If you need to extract an element key based on an index,
        public static string getCard(int random)
        {
            return dictCard.ElementAt(random).Key;
        }

        // If you need to extract the Key where the element value is equal to the integer generated randomly
        //public string getCard2(int random)
        //{
        //    return dictCard.FirstOrDefault(x => x.Value == random).Key;
        //}

    }
}
