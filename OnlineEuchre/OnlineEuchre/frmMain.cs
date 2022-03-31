using OnlineEuchre.Classes;
using OnlineEuchre.Extras;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace OnlineEuchre
{
    public partial class frmMain : Form
    {
        PlayerManager pm = null;
        public bool DiscardMode = false;

        public frmMain()
        {
            InitializeComponent();
            Player p1 = new Player( 1, pb01_01, pb01_02, pb01_03, pb01_04, pb01_05, pb01_Play, 
                                    pb01_Call, pb01_Trump, pbTurnup, pb01_Arrow, pb01_Deal, 
                                    Constants.Vertical, Constants.VertDown, Constants.Vertical);
            Player p2 = new Player( 2, pb02_01, pb02_02, pb02_03, pb02_04, pb02_05, pb02_Play, 
                                    pb02_Call, pb02_Trump, pbTurnup, pb02_Arrow, pb02_Deal, 
                                    Constants.Horizontal, Constants.HorzRight, Constants.Horizontal);
            Player p3 = new Player( 3, pb03_01, pb03_02, pb03_03, pb03_04, pb03_05, pb03_Play, 
                                    pb03_Call, pb03_Trump, pbTurnup, pb03_Arrow, pb03_Deal, 
                                    Constants.Vertical, Constants.Vertical, Constants.VertDown);
            Player p4 = new Player( 4, pb04_01, pb04_02, pb04_03, pb04_04, pb04_05, pb04_Play, 
                                    pb04_Call, pb04_Trump, pbTurnup, pb04_Arrow, pb04_Deal,
                                    Constants.Horizontal, Constants.Horizontal, Constants.HorzRight);

            pm = new PlayerManager(this, p1, p2, p3, p4);
        }

        public void SetlblDiscardVisibility(bool state)
        {
            lblDiscard.Visible = state;
        }

        public void SetTurnUp(bool state, Bitmap image)
        {
            pbTurnup.Visible = state;
            pbTurnup.Image = image;
        }
        public void SetDiscardMode(bool state)
        {
            DiscardMode = state;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            MoveTurnup();
        }

        private void MoveTurnup()
        {
            pbTurnup.Size = pb02_Play.Size;
            pbTurnup.Top = pb02_Play.Top;
            pbTurnup.Left = pb03_Play.Left;
        }

        private void btnDeal_Click(object sender, EventArgs e)
        {
            pm.Deal();
        }

        public Point GetAnchorLocation()
        {
            Point anchorPoint = new Point(this.Location.X + pb03_Play.Location.X, this.Location.Y + pb03_Play.Location.Y);
            return anchorPoint;
        }
        private void btnShuffle_Click(object sender, EventArgs e)
        {
            pm.Shuffle();
        }

        public void StopTimers()
        {
            pm.StopEuchreTimer();
            pm.StopCallTimer();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopTimers();
        }

        private void btnTrump_Click(object sender, EventArgs e)
        {
            pm.TrumpClick();
        }

        private void Common_PictureBoxClick(object sender, EventArgs e)
        {
            pm.Common_PictureBoxClick(sender, ref DiscardMode);
        }

        private void btnCall_Click(object sender, EventArgs e)
        {
            pm.CallClick();
        }
    }
}
