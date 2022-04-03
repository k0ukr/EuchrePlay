﻿using OnlineEuchre.Extras;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        Card TurnupCard = null;
        System.Timers.Timer EuchreTimer = null;
//        System.Timers.Timer CallCountdownTimer = null;
        System.Timers.Timer CallTimer = null;
        private frmMain _frmMain { get; set; }
        private ListBox _lbLog { get; set; }
        private static bool GoAheadEuchre = false;
        private static bool GoAheadCall = false;
        //private static bool GoAheadCallCountdown = false;
        private int DealerID { get; set; }
        private int PlayerID { get; set; }
        private int CalledTrumpPlayerID { get; set; }
        private int TrumpID { get; set; }

        Dictionary<int, Player> dictPlayer = new Dictionary<int, Player>();
        public PlayerManager(frmMain formMain, ListBox lbLog)
        {
            _frmMain = formMain;
            _lbLog = lbLog;
            EuchreTimer = new System.Timers.Timer(450);
            EuchreTimer.Elapsed += new ElapsedEventHandler(OnEuchreTimedEvent);

            CallTimer = new System.Timers.Timer(1000);
            CallTimer.Elapsed += new ElapsedEventHandler(OnCallTimedEvent);

            //CallCountdownTimer = new System.Timers.Timer(1000);
            //CallCountdownTimer.Elapsed += new ElapsedEventHandler(OnCallCountdownTimedEvent);

            DealerID = 4;
            PlayerID = 1;
            Constants.gAppPath = Path.GetDirectoryName(Application.ExecutablePath);
            LoadCards.LoadImages(Constants.gAppPath);
        }
        public void LoadPlayers (Player p1, Player p2, Player p3, Player p4)
        {
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

        //private static void OnCallCountdownTimedEvent(object source, ElapsedEventArgs e)
        //{
        //    ((System.Timers.Timer)source).Stop();
        //    GoAheadCallCountdown = true;
        //}

        public void TrumpClick()
        {
            GoAheadEuchre = false;
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
            GoAheadCall = false;
            Player prevPlayer = null;
            foreach (KeyValuePair<int, Player> keyVal in dictPlayer)
            {
                prevPlayer?.UpdateCallArrow(true);
                StartCallTimer();
                while (!GoAheadCall) { Application.DoEvents(); };
                prevPlayer = keyVal.Value;
            }
            prevPlayer?.UpdateCallArrow(false);
            StopCallTimer();
        }

        private void HideTurnup()
        {
            _frmMain.SetTurnUp(false, null);
        }

        private void ShowTurnup(int index)
        {
            _frmMain.SetTurnUp(true, CommonMod.RotateBitmap(LoadCards.GetByElement(index - 1).cImage, Constants.Vertical));
        }

        public void ClearLog()
        {
            _lbLog.Items.Clear();
        }

        public void AddToLog(string msg)
        {
            _lbLog.Items.Add(msg);
        }

        public void ResetPlayerControls()
        {
            foreach (KeyValuePair<int, Player> kvp in dictPlayer)
            {
                kvp.Value.ResetControls();
            }
        }

        public void Deal()
        {
            ResetPlayerControls();
            ClearLog();
            AddToLog("Deal");
            Shuffle();
            AdvanceDealer();
            DealCards();
            // Round 1 Bid
            if ( !Bid_Round1() )
            {
                Bid_Round2();
            }
        }

        public void Shuffle()
        {
            AddToLog("Shuffle Deck");
            HideTurnup();
            dictPlayer[1].LoadBack();
            dictPlayer[2].LoadBack();
            dictPlayer[3].LoadBack();
            dictPlayer[4].LoadBack();
            lstDeck = RandomMod.GetRandomList(24);
        }

        public void AdvanceDealer()
        {
            dictPlayer[DealerID].UpdateCallArrow(false);
            dictPlayer[DealerID].UpdateDealer(false);
            DealerID = CommonMod.lstNextWhatever[DealerID - 1];
            dictPlayer[DealerID].UpdateCallArrow(true);
            dictPlayer[DealerID].UpdateDealer(true);
            AddToLog($"Dealer #: {DealerID}");
        }

        private void DealCards()
        {
            AddToLog("Deal Cards");
            int index = 1;
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

            TurnupCard = LoadCards.GetByElement(lstDeck[index] - 1);
            ShowTurnup(lstDeck[index]);
        }

        //Bid: Round 1
        // Here, we ask each player if they want to 'Pass' or 'Call'.

        private bool Bid_Round1()
        {
            AddToLog("Bid: Round 1");
            int index = 0;
            bool retVal = false;
            Globals.TrumpCalled = false;
            int playerID = DealerID;
            int timeToWait = Constants.CallWaitTime;
            // Loop thru each player, break out if someone calls trump
            while (index++ < 4 && !Globals.TrumpCalled)
            {
                ShowAskForm();
                Globals.PersonPassed = false;
                playerID = CommonMod.lstNextWhatever[playerID - 1];
                AddToLog($"Player #:{playerID} Hand Value: {dictPlayer[playerID].EvaluateHand(TurnupCard.cSuit)}");
                AddToLog($"Player #:{playerID} Total Suits: {dictPlayer[playerID].GetSuits()}");
                // Loop for CallWaitTime seconds
                for (int ttw = Constants.CallWaitTime; ttw >= 0;ttw--)
                {
                    GoAheadCall = false;
                    StartCallTimer();
                    dictPlayer[playerID].UpdateTimeToWait(ttw);
                    while (!GoAheadCall && ttw >= 0) { Application.DoEvents(); };
                    if (Globals.TrumpCalled || Globals.PersonPassed)
                    {
                        break;
                    }
                }
                if (Globals.TrumpCalled)
                {
                    dictPlayer[playerID].UpdateTimeToWait(0);
                    break;
                }
                if (Globals.PersonPassed)
                {
                    dictPlayer[playerID].UpdateTimeToWait(0);
                    AddToLog($"Player #:{playerID} passed");
                }
            }

            if (Globals.TrumpCalled )
            {
                CalledTrumpPlayerID = playerID;
                dictPlayer[CalledTrumpPlayerID].UpdateCall(true);
                dictPlayer[CalledTrumpPlayerID].SetTrump(TurnupCard.cSuit);

                AddToLog($"Player #:{CalledTrumpPlayerID} called Trump: {TurnupCard.cSuit.ToString()}");
                AddToLog($"Player #:{DealerID} Hand Value: {dictPlayer[DealerID].EvaluateHand(TurnupCard.cSuit)}");
                _frmMain.SetDiscardMode(true);
                _frmMain.SetlblDiscardVisibility(true);
            }
            else
            {
                HideTurnup();
                dictPlayer[DealerID].UpdateCallArrow(false);
            }
            return retVal;
        }

        private void ShowAskForm()
        {
            if (AskForm == null)
            {
                AskForm = new frmAsk();
            }
            AskForm.Top = dictPlayer[2].GetAskAnchorLocation().Y;
            AskForm.Left = dictPlayer[2].GetAskAnchorLocation().X;
            AskForm.Show();
        }

        private bool Bid_Round2()
        {
            bool retVal = false;
            AddToLog("Start Round 2 Bidding");
            frmRoundTwo frm = new frmRoundTwo(TurnupCard.cSuit);
            frm.ShowDialog();
            return retVal;
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

        //public void StartCallCountdownTimer()
        //{
        //    CallCountdownTimer.Start();
        //    GoAheadCallCountdown = false;
        //}

        //public void StopCallCountdownTimer()
        //{
        //    CallCountdownTimer.Stop();
        //    GoAheadCallCountdown = true;
        //}

        public void Common_PictureBoxPlayer01Click(object sender, ref bool discardMode)
        {
            if (discardMode && DealerID == 1)
            {
                Common_PictureBoxClick(sender, ref discardMode);
            }
        }
        public void Common_PictureBoxPlayer02Click(object sender, ref bool discardMode)
        {
            if (discardMode && DealerID == 2)
            {
                Common_PictureBoxClick(sender, ref discardMode);
            }
        }
        public void Common_PictureBoxPlayer03Click(object sender, ref bool discardMode)
        {
            if (discardMode && DealerID == 3)
            {
                Common_PictureBoxClick(sender, ref discardMode);
            }
        }
        public void Common_PictureBoxPlayer04Click(object sender, ref bool discardMode)
        {
            if (discardMode && DealerID == 4)
            {
                Common_PictureBoxClick(sender, ref discardMode);
            }
        }

        public void Common_PictureBoxClick(object sender, ref bool discardMode)
        {
            if (discardMode)
            {
                AddToLog($"Dealer discarded");
                dictPlayer[DealerID].Pickup((PictureBox)sender, lstDeck[Constants.TURNUP_INDEX]);
                AddToLog($"Player #:{DealerID} New Hand Value: {dictPlayer[DealerID].EvaluateHand(TurnupCard.cSuit)}");
                Application.DoEvents();
                discardMode = false;
                _frmMain.SetlblDiscardVisibility(false);
                dictPlayer[DealerID].UpdateCallArrow(false);
            }
        }
        public Point GetHomePt()
        {
            return _frmMain.Location;
        }

        public void SetDiscardVisibility(bool state)
        {
            dictPlayer[DealerID].UpdateDiscard(state);
        }
    }
}
