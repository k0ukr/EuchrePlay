using OnlineEuchre.Extras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnlineEuchre.Classes
{
    public class Player
    {
        PictureBox[] pbCardArray = new PictureBox[5];
        public Player(PictureBox pb1, PictureBox pb2, PictureBox pb3, PictureBox pb4, PictureBox pb5, float orient )
        {
            _pb1 = pb1;
            _pb2 = pb2;
            _pb3 = pb3;
            _pb4 = pb4;
            _pb5 = pb5;
            _orient = orient;
            pbCardArray[0] = _pb1;
            pbCardArray[1] = _pb2;
            pbCardArray[2] = _pb3;
            pbCardArray[3] = _pb4;
            pbCardArray[4] = _pb5;
        }

        PictureBox _pb1 { get; set; }
        PictureBox _pb2 { get; set; }
        PictureBox _pb3 { get; set; }
        PictureBox _pb4 { get; set; }
        PictureBox _pb5 { get; set; }
        float _orient { get; set; }

        public void LoadBack()
        {
            foreach ( var pb in pbCardArray)
            {
                pb.Image = LoadCards.CardBack;
            }
        }

        public void LoadHand(int cardex, int index)
        {
            pbCardArray[cardex - 1].Image = CommonMod.RotateBitmap(LoadCards.GetByElement(index - 1).cImage, _orient);
        }
    }
}
