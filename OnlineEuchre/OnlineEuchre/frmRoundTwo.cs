using OnlineEuchre.Classes;
using OnlineEuchre.Extras;
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
        private List<BidSuit> lstCallBtn = new List<BidSuit>();
        public frmRoundTwo()
        {
            InitializeComponent();
        }

        private void frmRoundTwo_Load(object sender, EventArgs e)
        {
        }

        public void LoadLabels(Suit cSuit)
        {
            int index = 0;
            lstCallBtn.Add(new BidSuit(btnCall01));
            lstCallBtn.Add(new BidSuit(btnCall02));
            lstCallBtn.Add(new BidSuit(btnCall03));
            foreach (Suit cardSuit in Enum.GetValues(typeof(Suit)))
            {
                if (cardSuit != Suit.None && cardSuit != cSuit)
                {
                    lstCallBtn[index]._cSuit = cardSuit;
                    lstCallBtn[index]._btn.Text = cardSuit.ToString();
                    index++;
                }
            }
        }

        private void btnPass_Click(object sender, EventArgs e)
        {
            Globals.PersonPassed = true;
            this.Hide();
        }

        private void btnCall01_Click(object sender, EventArgs e)
        {
            SetTrump(0);
        }

        private void btnCall02_Click(object sender, EventArgs e)
        {
            SetTrump(1);
        }

        private void btnCall03_Click(object sender, EventArgs e)
        {
            SetTrump(2);
        }

        private void SetTrump(int index)
        {
            Globals.TrumpCalled = true;
            Globals.TrumpSuit = lstCallBtn[index]._cSuit;
            this.Hide();
        }
    }
}
