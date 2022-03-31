using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using static OnlineEuchre.Extras.Constants;

namespace OnlineEuchre.Extras
{
    public static class CommonMod
    {
        public static List<int> lstNextWhatever = new List<int> { 2, 3, 4, 1 }; // Gives Id of the next player to lay a card, deal, etc

        public static List<string> GetFilesFromPath(string path)
        {
            List<string> files = new List<string>();
            try
            {
                files.AddRange(Directory.GetFiles(path));

                foreach (string dir in Directory.GetDirectories(path))
                {
                    files.AddRange(GetFilesFromPath(dir));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return files;
        }

        public static Suit GetBowerSuit(Suit Trump)
        {
            Suit retVal = Suit.Club;
            switch (Trump)
            {
                case Suit.Heart:
                    retVal = Suit.Diamond;
                    break;
                case Suit.Diamond:
                    retVal = Suit.Heart;
                    break;
                case Suit.Club:
                    retVal = Suit.Spade;
                    break;
                case Suit.Spade:
                    retVal = Suit.Club;
                    break;
            }
            return retVal;
        }

        public static Values GetValue(Suit Trump, Suit cardSuit, Rank cardRank)
        {
            Values retVal = Values.NoValue;
            if (cardSuit == Trump)
            {
                switch (cardRank)
                {
                    case Rank.Nine:
                        retVal = Values.NineTrump;
                        break;
                    case Rank.Ten:
                        retVal = Values.TenTrump;
                        break;
                    case Rank.Queen:
                        retVal = Values.QueenTrump;
                        break;
                    case Rank.King:
                        retVal = Values.KingTrump;
                        break;
                    case Rank.Ace:
                        retVal = Values.AceTrump;
                        break;
                    case Rank.Jack:
                        retVal = Values.RightBower;
                        break;
                }
            }
            else if (cardSuit == GetBowerSuit(Trump) && cardRank == Rank.Jack)
            {
                retVal = Values.LeftBower;
            }
            else
            {
                switch (cardRank)
                {
                    case (Rank.Nine):
                        retVal = Values.NineNoTrump;
                        break;
                    case (Rank.Ten):
                        retVal = Values.TenNoTrump;
                        break;
                    case Rank.Jack:
                        retVal = Values.JackNoTrump;
                        break;
                    case Rank.Queen:
                        retVal = Values.QueenNoTrump;
                        break;
                    case Rank.King:
                        retVal = Values.KingNoTrump;
                        break;
                    case Rank.Ace:
                        retVal = Values.AceNoTrump;
                        break;
                }
            }
            return retVal;
        }
        // Return a bitmap rotated around its center.
        public static Bitmap RotateBitmap(Bitmap bm, float angle)
        {
            // Make a Matrix to represent rotation
            // by this angle.
            Matrix rotate_at_origin = new Matrix();
            rotate_at_origin.Rotate(angle);

            // Rotate the image's corners to see how big
            // it will be after rotation.
            PointF[] points =
            {
                new PointF(0, 0),
                new PointF(bm.Width, 0),
                new PointF(bm.Width, bm.Height),
                new PointF(0, bm.Height),
            };
            rotate_at_origin.TransformPoints(points);
            float xmin, xmax, ymin, ymax;
            GetPointBounds(points, out xmin, out xmax,
                out ymin, out ymax);

            // Make a bitmap to hold the rotated result.
            int wid = (int)Math.Round(xmax - xmin);
            int hgt = (int)Math.Round(ymax - ymin);
            Bitmap result = new Bitmap(wid, hgt);

            // Create the real rotation transformation.
            Matrix rotate_at_center = new Matrix();
            rotate_at_center.RotateAt(angle,
                new PointF(wid / 2f, hgt / 2f));

            // Draw the image onto the new bitmap rotated.
            using (Graphics gr = Graphics.FromImage(result))
            {
                // Use smooth image interpolation.
                gr.InterpolationMode = InterpolationMode.High;

                // Clear with the color in the image's upper left corner.
                gr.Clear(bm.GetPixel(0, 0));

                //// For debugging. (It's easier to see the background.)
                //gr.Clear(Color.LightBlue);

                // Set up the transformation to rotate.
                gr.Transform = rotate_at_center;

                // Draw the image centered on the bitmap.
                int x = (wid - bm.Width) / 2;
                int y = (hgt - bm.Height) / 2;
                gr.DrawImage(bm, x, y);
            }

            // Return the result bitmap.
            return result;
        }
        // Find the bounding rectangle for an array of points.
        public static void GetPointBounds(PointF[] points,
            out float xmin, out float xmax,
            out float ymin, out float ymax)
        {
            xmin = points[0].X;
            xmax = xmin;
            ymin = points[0].Y;
            ymax = ymin;
            foreach (PointF point in points)
            {
                if (xmin > point.X) xmin = point.X;
                if (xmax < point.X) xmax = point.X;
                if (ymin > point.Y) ymin = point.Y;
                if (ymax < point.Y) ymax = point.Y;
            }
        }
    }
}
