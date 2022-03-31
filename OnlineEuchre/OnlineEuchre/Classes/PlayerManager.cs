using OnlineEuchre.Extras;
using System;
using System.Collections.Generic;
using System.IO;
using System.Timers;
using System.Windows.Forms;
using static OnlineEuchre.Extras.Randomizer;

namespace OnlineEuchre.Classes
{
    public class PlayerManager
    {
        frmAsk AskForm = new frmAsk();
        List<int> lstDeck = null;
        System.Timers.Timer EuchreTimer = null;
        System.Timers.Timer CallTimer = null;
        private frmMain _frmMain { get; set; }
        private static bool GoAheadEuchre = false;
        private static bool GoAheadCall = false;
        private int DealerID { get; set; }
        private int PlayerID { get; set; }
        private int TrumpID { get; set; }

        Dictionary<int, Player> dictPlayer = new Dictionary<int, Player>();
        public PlayerManager (frmMain formMain, Player p1, Player p2, Player p3, Player p4)
        {
            _frmMain = formMain;
            EuchreTimer = new System.Timers.Timer(1000);
            EuchreTimer.Elapsed += new ElapsedEventHandler(OnEuchreTimedEvent);

            CallTimer = new System.Timers.Timer(3000);
            CallTimer.Elapsed += new ElapsedEventHandler(OnCallTimedEvent);

            DealerID = 4;
            PlayerID = 1;
            Constants.gAppPath = Path.GetDirectoryName(Application.ExecutablePath);
            LoadCards.LoadImages(Constants.gAppPath);
            dictPlayer.Add(1, p1);
            dictPlayer.Add(2, p2);
            dictPlayer.Add(3, p3);
            dictPlayer.Add(4, p4);
        }

        private static void OnEuchreTimedEvent(object source, ElapsedEventArgs e)
        {
            ((System.Timers.Timer)source).Stop();
            GoAheadEuchre = true;
        }

        private static void OnCallTimedEvent(object source, ElapsedEventArgs e)
        {
            ((System.Timers.Timer)source).Stop();
            GoAheadCall = true;
        }

        public void Deal()
        {
            Shuffle();
            AdvanceDealer();
            if (AskForm == null)
            {
                AskForm = new frmAsk();
            }
            AskForm.Top = _frmMain.GetAnchorLocation().Y;
            AskForm.Left = _frmMain.GetAnchorLocation().X;
            if (AskForm.ShowDialog() == DialogResult.OK)
            {
                _frmMain.SetDiscardMode(true);
                _frmMain.SetlblDiscardVisibility(true);
            }
            int TurnupCard = lstDeck[20];
        }

        public void TrumpClick()
        {
            Player prevPlayer = null;
            foreach (KeyValuePair<int, Player> keyVal in dictPlayer)
            {
                prevPlayer?.ClearTrump();
                keyVal.Value.SetTrump((Constants.Suit)(keyVal.Key - 1));
                StartEuchreTimer();
                while (!GoAheadEuchre) { Application.DoEvents(); };
                prevPlayer = keyVal.Value;
            }
            prevPlayer?.ClearTrump();
        }

        public void CallClick()
        {
            Player prevPlayer = null;
            foreach (KeyValuePair<int, Player> keyVal in dictPlayer)
            {
                prevPlayer?.ClearArrow();
                keyVal.Value.SetArrow();
                StartCallTimer();
                while (!GoAheadCall) { Application.DoEvents(); };
                prevPlayer = keyVal.Value;
            }
            prevPlayer?.ClearArrow();
        }

        private void HideTurnup()
        {
            _frmMain.SetTurnUp(false, null);
        }

        private void ShowTurnup(int index)
        {
            _frmMain.SetTurnUp(true, CommonMod.RotateBitmap(LoadCards.GetByElement(index - 1).cImage, Constants.Vertical));
        }

        public void Shuffle()
        {
            HideTurnup();
            dictPlayer[1].LoadBack();
            dictPlayer[2].LoadBack();
            dictPlayer[3].LoadBack();
            dictPlayer[4].LoadBack();
        }

        public void AdvanceDealer()
        {
            int index = 1;
            lstDeck = RandomMod.GetRandomList(24);
            dictPlayer[DealerID].UpdateDealer(DealerID, false);
            dictPlayer[DealerID].UpdateCall(DealerID, false);
            DealerID = CommonMod.lstNextWhatever[DealerID - 1];
            dictPlayer[DealerID].UpdateDealer(DealerID, true);
            dictPlayer[DealerID].UpdateCall(DealerID, true);
            int playerId = DealerID;
            for (int i = 0; i < 4; i++)
            {
                StartEuchreTimer();
                while (!GoAheadEuchre) { Application.DoEvents(); };

                playerId = CommonMod.lstNextWhatever[playerId - 1];
                dictPlayer[playerId].ClearHand();
                for (int cardex = 1; cardex < 6; cardex++)
                {
                    dictPlayer[playerId].LoadHand(cardex, lstDeck[index++]);
                }
            }
            ShowTurnup(lstDeck[index++]);
        }

        public void StartEuchreTimer()
        {
            EuchreTimer.Start();
            GoAheadEuchre = false;
        }

        public void StopEuchreTimer()
        {
            EuchreTimer.Stop();
            GoAheadEuchre = true;
        }

        public void StartCallTimer()
        {
            CallTimer.Start();
            GoAheadCall = false;
        }

        public void StopCallTimer()
        {
            CallTimer.Stop();
            GoAheadCall = true;
        }

        public void Common_PictureBoxClick(object sender, ref bool discardMode)
        {
            if (discardMode)
            {
                dictPlayer[DealerID].Pickup((PictureBox)sender, lstDeck[Constants.TURNUP_INDEX]);
                Application.DoEvents();
                discardMode = false;
                _frmMain.SetlblDiscardVisibility(false);
            }

        }
    }
}
