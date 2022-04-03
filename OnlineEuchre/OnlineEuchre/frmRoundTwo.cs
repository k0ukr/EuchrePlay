using OnlineEuchre.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static OnlineEuchre.Extras.Constants;

namespace OnlineEuchre
{
    public partial class frmRoundTwo : Form
    {
        public frmRoundTwo(Suit cSuit)
        {
            InitializeComponent();
            _cSuit = cSuit;
        }

        private Suit _cSuit { get; set; }

        private void frmRoundTwo_Load(object sender, EventArgs e)
        {
            LoadLabels();
        }

        private void LoadLabels()
        {
            int index = 0;
            List<BidSuit> lstCallBtn = new List<BidSuit>();
            lstCallBtn.Add(new BidSuit(btnCall01));
            lstCallBtn.Add(new BidSuit(btnCall02));
            lstCallBtn.Add(new BidSuit(btnCall03));
            foreach (Suit cardSuit in Enum.GetValues(typeof(Suit)))
            {
                if ( cardSuit != _cSuit)
                {
                    lstCallBtn[index]._cSuit = cardSuit;
                    lstCallBtn[index]._btn.Text = cardSuit.ToString();
                    index++;
                }
            }
        }
    }
}
