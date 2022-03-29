using OnlineEuchre.Classes;
using OnlineEuchre.Extras;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static OnlineEuchre.Extras.Randomizer;

namespace OnlineEuchre
{
    public partial class frmMain : Form
    {
        PictureBox[] pbArray = new PictureBox[4];
        private bool GoAhead = false;
        Dictionary<int, Player> dictPlayer = new Dictionary<int, Player>();
        List<int> lstNextWhatever = new List<int> { 2, 3, 4, 1 }; // Gives Id of the next player to lay a card, deal, etc
        public frmMain()
        {
            InitializeComponent();
        }

        private int DealerID { get; set; }
        private int PlayerID { get; set; }
        private void frmMain_Load(object sender, EventArgs e)
        {
            LoadPBArray();
            DealerID = 4;
            PlayerID = 1;
            Constants.gAppPath = Path.GetDirectoryName(Application.ExecutablePath);
            LoadCards.LoadImages(Constants.gAppPath);
            dictPlayer.Add(1, new Player(pb01_01, pb01_02, pb01_03, pb01_04, pb01_05, Constants.Vertical));
            dictPlayer.Add(2, new Player(pb02_01, pb02_02, pb02_03, pb02_04, pb02_05, Constants.Horizontal));
            dictPlayer.Add(3, new Player(pb03_01, pb03_02, pb03_03, pb03_04, pb03_05, Constants.Vertical));
            dictPlayer.Add(4, new Player(pb04_01, pb04_02, pb04_03, pb04_04, pb04_05, Constants.Horizontal));

            // Load Dealer Coin
            foreach (var pb in pbArray)
            {
                pb.Image = CommonMod.RotateBitmap(LoadCards.DealerCoin, Constants.Vertical);
            }
        }

        private void LoadPBArray()
        {
            pbArray[0] = pbD1;
            pbArray[1] = pbD2;
            pbArray[2] = pbD3;
            pbArray[3] = pbD4;
        }
        private void TestImages()
        {
            int index = 0;
            List<int> lstRan = RandomMod.GetRandomList(24);
            StartEuchreTimer();
            while (!GoAhead) { Application.DoEvents(); };
            pb01_01.Image = CommonMod.RotateBitmap(LoadCards.GetByElement(lstRan[index++] - 1).cImage, Constants.Vertical);
            pb01_02.Image = CommonMod.RotateBitmap(LoadCards.GetByElement(lstRan[index++] - 1).cImage, Constants.Vertical);
            pb01_03.Image = CommonMod.RotateBitmap(LoadCards.GetByElement(lstRan[index++] - 1).cImage, Constants.Vertical);
            pb01_04.Image = CommonMod.RotateBitmap(LoadCards.GetByElement(lstRan[index++] - 1).cImage, Constants.Vertical);
            pb01_05.Image = CommonMod.RotateBitmap(LoadCards.GetByElement(lstRan[index++] - 1).cImage, Constants.Vertical);

            StartEuchreTimer();
            while (!GoAhead) { Application.DoEvents(); };
            pb02_01.Image = CommonMod.RotateBitmap(LoadCards.GetByElement(lstRan[index++] - 1).cImage, Constants.Horizontal);
            pb02_02.Image = CommonMod.RotateBitmap(LoadCards.GetByElement(lstRan[index++] - 1).cImage, Constants.Horizontal);
            pb02_03.Image = CommonMod.RotateBitmap(LoadCards.GetByElement(lstRan[index++] - 1).cImage, Constants.Horizontal);
            pb02_04.Image = CommonMod.RotateBitmap(LoadCards.GetByElement(lstRan[index++] - 1).cImage, Constants.Horizontal);
            pb02_05.Image = CommonMod.RotateBitmap(LoadCards.GetByElement(lstRan[index++] - 1).cImage, Constants.Horizontal);

            StartEuchreTimer();
            while (!GoAhead) { Application.DoEvents(); };
            pb03_01.Image = CommonMod.RotateBitmap(LoadCards.GetByElement(lstRan[index++] - 1).cImage, Constants.Vertical);
            pb03_02.Image = CommonMod.RotateBitmap(LoadCards.GetByElement(lstRan[index++] - 1).cImage, Constants.Vertical);
            pb03_03.Image = CommonMod.RotateBitmap(LoadCards.GetByElement(lstRan[index++] - 1).cImage, Constants.Vertical);
            pb03_04.Image = CommonMod.RotateBitmap(LoadCards.GetByElement(lstRan[index++] - 1).cImage, Constants.Vertical);
            pb03_05.Image = CommonMod.RotateBitmap(LoadCards.GetByElement(lstRan[index++] - 1).cImage, Constants.Vertical);

            StartEuchreTimer();
            while (!GoAhead) { Application.DoEvents(); };
            pb04_01.Image = CommonMod.RotateBitmap(LoadCards.GetByElement(lstRan[index++] - 1).cImage, Constants.Horizontal);
            pb04_02.Image = CommonMod.RotateBitmap(LoadCards.GetByElement(lstRan[index++] - 1).cImage, Constants.Horizontal);
            pb04_03.Image = CommonMod.RotateBitmap(LoadCards.GetByElement(lstRan[index++] - 1).cImage, Constants.Horizontal);
            pb04_04.Image = CommonMod.RotateBitmap(LoadCards.GetByElement(lstRan[index++] - 1).cImage, Constants.Horizontal);
            pb04_05.Image = CommonMod.RotateBitmap(LoadCards.GetByElement(lstRan[index++] - 1).cImage, Constants.Horizontal);

            pb01_06.Image = CommonMod.RotateBitmap(LoadCards.GetByElement(lstRan[index++] - 1).cImage, Constants.Vertical);
            pb02_06.Image = CommonMod.RotateBitmap(LoadCards.GetByElement(lstRan[index++] - 1).cImage, Constants.Vertical);
            pb03_06.Image = CommonMod.RotateBitmap(LoadCards.GetByElement(lstRan[index++] - 1).cImage, Constants.Vertical);
            pb04_06.Image = CommonMod.RotateBitmap(LoadCards.GetByElement(lstRan[index++] - 1).cImage, Constants.Vertical);

        }
        private void TestImages2()
        {
            StartEuchreTimer();
            while (!GoAhead) { Application.DoEvents(); };
            pb01_01.Image = CommonMod.RotateBitmap(LoadCards.GetFirst().cImage, Constants.Vertical);
            pb01_02.Image = CommonMod.RotateBitmap(LoadCards.GetNext().cImage, Constants.Vertical);
            pb01_03.Image = CommonMod.RotateBitmap(LoadCards.GetNext().cImage, Constants.Vertical);
            pb01_04.Image = CommonMod.RotateBitmap(LoadCards.GetNext().cImage, Constants.Vertical);
            pb01_05.Image = CommonMod.RotateBitmap(LoadCards.GetNext().cImage, Constants.Vertical);

            StartEuchreTimer();
            while (!GoAhead) { Application.DoEvents(); };
            pb02_01.Image = CommonMod.RotateBitmap(LoadCards.GetNext().cImage, Constants.Horizontal);
            pb02_02.Image = CommonMod.RotateBitmap(LoadCards.GetNext().cImage, Constants.Horizontal);
            pb02_03.Image = CommonMod.RotateBitmap(LoadCards.GetNext().cImage, Constants.Horizontal);
            pb02_04.Image = CommonMod.RotateBitmap(LoadCards.GetNext().cImage, Constants.Horizontal);
            pb02_05.Image = CommonMod.RotateBitmap(LoadCards.GetNext().cImage, Constants.Horizontal);

            StartEuchreTimer();
            while (!GoAhead) { Application.DoEvents(); };
            pb03_01.Image = CommonMod.RotateBitmap(LoadCards.GetNext().cImage, Constants.Vertical);
            pb03_02.Image = CommonMod.RotateBitmap(LoadCards.GetNext().cImage, Constants.Vertical);
            pb03_03.Image = CommonMod.RotateBitmap(LoadCards.GetNext().cImage, Constants.Vertical);
            pb03_04.Image = CommonMod.RotateBitmap(LoadCards.GetNext().cImage, Constants.Vertical);
            pb03_05.Image = CommonMod.RotateBitmap(LoadCards.GetNext().cImage, Constants.Vertical);

            StartEuchreTimer();
            while (!GoAhead) { Application.DoEvents(); };
            pb04_01.Image = CommonMod.RotateBitmap(LoadCards.GetNext().cImage, Constants.Horizontal);
            pb04_02.Image = CommonMod.RotateBitmap(LoadCards.GetNext().cImage, Constants.Horizontal);
            pb04_03.Image = CommonMod.RotateBitmap(LoadCards.GetNext().cImage, Constants.Horizontal);
            pb04_04.Image = CommonMod.RotateBitmap(LoadCards.GetNext().cImage, Constants.Horizontal);
            pb04_05.Image = CommonMod.RotateBitmap(LoadCards.GetNext().cImage, Constants.Horizontal);

            pb01_06.Image = CommonMod.RotateBitmap(LoadCards.GetNext().cImage, Constants.Vertical);
            pb02_06.Image = CommonMod.RotateBitmap(LoadCards.GetNext().cImage, Constants.Vertical);
            pb03_06.Image = CommonMod.RotateBitmap(LoadCards.GetNext().cImage, Constants.Vertical);
            pb04_06.Image = CommonMod.RotateBitmap(LoadCards.GetNext().cImage, Constants.Vertical);

        }



        private void btnDeal_Click(object sender, EventArgs e)
        {
            Shuffle();
            AdvanceDealer();
        }

        private void AdvanceDealer()
        {
            int index = 1;
            List<int> lstRan = RandomMod.GetRandomList(24);

            UpdateDealer(DealerID, false);
            DealerID = lstNextWhatever[DealerID - 1];
            UpdateDealer(DealerID, true);
            int playerId = DealerID;
            for (int i = 0; i < 4; i++)
            {
                StartEuchreTimer();
                while (!GoAhead) { Application.DoEvents(); };

                playerId = lstNextWhatever[playerId - 1];
                for (int cardex = 1; cardex < 6; cardex++)
                {
                    dictPlayer[playerId].LoadHand(cardex, lstRan[index++]);
                }
            }
        }

        private void UpdateDealer(int dealerID, bool state)
        {
            pbArray[dealerID - 1].Visible = state;
        }

        private void btnShuffle_Click(object sender, EventArgs e)
        {
           Shuffle();
        }

        private void Shuffle()
        {
            dictPlayer[1].LoadBack();
            dictPlayer[2].LoadBack();
            dictPlayer[3].LoadBack();
            dictPlayer[4].LoadBack();
            pb01_06.Image = CommonMod.RotateBitmap(LoadCards.CardBack, Constants.Vertical);
            pb02_06.Image = CommonMod.RotateBitmap(LoadCards.CardBack, Constants.Vertical);
            pb03_06.Image = CommonMod.RotateBitmap(LoadCards.CardBack, Constants.Vertical);
            pb04_06.Image = CommonMod.RotateBitmap(LoadCards.CardBack, Constants.Vertical);

        }

        private void EuchreTimer_Tick(object sender, EventArgs e)
        {
            StopEuchreTimer();
            UpdateStartBtn(GoAhead);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartEuchreTimer();
            UpdateStartBtn(GoAhead);
        }

        private void StartEuchreTimer()
        {
            EuchreTimer.Start();
            GoAhead = false;
        }

        public void StopEuchreTimer()
        {
            EuchreTimer.Stop();
            GoAhead = true;
        }
        private void UpdateStartBtn(bool state)
        {
            btnStart.Visible = state;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopEuchreTimer();
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            List<int> lstRan = RandomMod.GetRandomList(24);
        }

        private void btnTrump_Click(object sender, EventArgs e)
        {
            pbTrump.Visible = true;
            pbTrump.Image = LoadCards.ClubTrump;
            StartEuchreTimer();
            while (!GoAhead) { Application.DoEvents(); };
            pbTrump.Image = LoadCards.DiamondTrump;
            StartEuchreTimer();
            while (!GoAhead) { Application.DoEvents(); };
            pbTrump.Image = LoadCards.HeartTrump;
            StartEuchreTimer();
            while (!GoAhead) { Application.DoEvents(); };
            pbTrump.Image = LoadCards.SpadeTrump;
            StartEuchreTimer();
            while (!GoAhead) { Application.DoEvents(); };
            pbTrump.Visible = false;
        }
    }
}
