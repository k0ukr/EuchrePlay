using OnlineEuchre.Classes.Static;
using OnlineEuchre.Extras;
using OnlineEuchre.View;
using System.Collections.Generic;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;
using static OnlineEuchre.Extras.Constants;
using static OnlineEuchre.Extras.Randomizer;

namespace OnlineEuchre.Classes
{
    public class PlayerManager
    {
        frmAsk AskForm = new frmAsk();
        frmRoundTwo formRoundTwo = null;
        List<int> lstDeck = null;
        Card TurnupCard = null;
        System.Timers.Timer EuchreTimer = null;
        System.Timers.Timer CallTimer = null;
        private ListBox _lbLog { get; set; }
        private static bool GoAheadEuchre = false;
        private static bool GoAheadCall = false;
        public int DealerID { get; private set; }
        private int PlayerID { get; set; }
        private int CalledTrumpPlayerID { get; set; }
        private int TrumpID { get; set; }

        public Dictionary<int, Player> dictPlayer = new Dictionary<int, Player>();
        public PlayerManager(GameManager gm, ListBox lbLog)
        {
            _gm = gm;
            _lbLog = lbLog;
            EuchreTimer = new System.Timers.Timer(450);
            EuchreTimer.Elapsed += new ElapsedEventHandler(OnEuchreTimedEvent);

            CallTimer = new System.Timers.Timer(1000);
            CallTimer.Elapsed += new ElapsedEventHandler(OnCallTimedEvent);

            DealerID = 4;
            PlayerID = 1;
        }

        private GameManager _gm { get; set; }
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

        public void TrumpClick()
        {
            GoAheadEuchre = false;
            Player prevPlayer = null;
            foreach (KeyValuePair<int, Player> keyVal in dictPlayer)
            {
                prevPlayer?.ClearTrump();
                keyVal.Value.SetTrump((Constants.Suit)(keyVal.Key));
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
                prevPlayer = keyVal.Value;
                prevPlayer?.UpdateCallArrow(true);
                StartCallTimer();
                while (!GoAheadCall) { Application.DoEvents(); };
                prevPlayer?.UpdateCallArrow(false);
            }
            prevPlayer?.UpdateCallArrow(false);
            StopCallTimer();
        }

        private void HideTurnup()
        {
            _gm.SetTurnUp(false, null);
        }

        private void ShowTurnup(int index)
        {
            _gm.SetTurnUp(true, CommonMod.RotateBitmap(LoadCardSingleton.GetByElement(index - 1).cImage, Constants.Vertical));
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
            HideTurnup();
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

            TurnupCard = LoadCardSingleton.GetByElement(lstDeck[index] - 1);
            ShowTurnup(lstDeck[index]);
        }

        //Bid: Round 1
        // Here, we ask each player if they want to 'Pass' or 'Call'.
        public bool Bid_Round1()
        {
            bool retVal = false;
            Globals.TrumpSuit = Suit.None;
            AddToLog("Bid: Round 1");
            int index = 0;
            Globals.TrumpCalled = false;
            int playerID = DealerID;
            // Loop thru each player, break out if someone calls trump
            while (index++ < 4 && !Globals.TrumpCalled)
            {
                ShowAskForm();
                Globals.PersonPassed = false;
                playerID = CommonMod.lstNextWhatever[playerID - 1];
                AddToLog($"Player #:{playerID} Hand Value: {dictPlayer[playerID].EvaluateHand(TurnupCard.cSuit)}");
                AddToLog($"Player #:{playerID} Total Suits: {dictPlayer[playerID].GetSuitCount()}");
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
                Globals.TrumpSuit = TurnupCard.cSuit;
                CalledTrumpPlayerID = playerID;
                dictPlayer[CalledTrumpPlayerID].UpdateCall(true);
                dictPlayer[CalledTrumpPlayerID].SetTrump(TurnupCard.cSuit);

                AddToLog($"Player #:{CalledTrumpPlayerID} called Trump: {TurnupCard.cSuit.ToString()}");
                AddToLog($"Player #:{DealerID} Hand Value: {dictPlayer[DealerID].EvaluateHand(TurnupCard.cSuit)}");
                _gm.SetDiscardMode(true);
                _gm.SetlblDiscardVisibility(true);
                retVal = true;
            }
            else
            {
                HideTurnup();
            }
            AskForm.Hide();
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

        public bool Bid_Round2()
        {
            bool retVal = false;
            AddToLog("Start Round 2 Bidding");

            int index = 0;

            Globals.TrumpCalled = false;
            int playerID = DealerID;
            // Loop thru each player, break out if someone calls trump
            while (index++ < 4 && !Globals.TrumpCalled)
            {
                dictPlayer[playerID].EvaluateAllSuitValue();
                AddToLog($"Player #:{playerID} Club: {dictPlayer[playerID].HandValue(Constants.Suit.Club)}  Diamond: {dictPlayer[playerID].HandValue(Constants.Suit.Diamond)}  Heart: {dictPlayer[playerID].HandValue(Constants.Suit.Heart)}  Spade: {dictPlayer[playerID].HandValue(Constants.Suit.Spade)}");
                ShowRoundTwo();
                Globals.PersonPassed = false;
                playerID = CommonMod.lstNextWhatever[playerID - 1];
                // Loop for CallWaitTime seconds
                for (int ttw = Constants.CallWaitTime; ttw >= 0; ttw--)
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
                    AddToLog($"Player #:{playerID} Second pass");
                }
            }

            if (Globals.TrumpCalled)
            {
                CalledTrumpPlayerID = playerID;
                dictPlayer[CalledTrumpPlayerID].UpdateCall(true);
                //todo: Maybe set Trump at the PlayerManager Level or better yet, the Game Level
                dictPlayer[CalledTrumpPlayerID].SetTrump(Globals.TrumpSuit);

                AddToLog($"Player #:{CalledTrumpPlayerID} called Trump: {Globals.TrumpSuit.ToString()}");
                retVal = true;
            }
            dictPlayer[DealerID].UpdateCallArrow(false);
            formRoundTwo.Hide();
            return retVal;
        }

        private void ShowRoundTwo()
        {
            if (formRoundTwo == null)
            {
                formRoundTwo = new frmRoundTwo();
            }
            formRoundTwo.LoadLabels(TurnupCard.cSuit);
            Point anchorPoint = GetRoundTwoAnchorLocation();
            formRoundTwo.Top = anchorPoint.Y;
            formRoundTwo.Left = anchorPoint.X;
            formRoundTwo.Show();
        }

        public Point GetRoundTwoAnchorLocation()
        {
            Point anchorPoint = new Point(GetHomePt().X + _gm.GetPanelRoundTwoAnchor().X, GetHomePt().Y + _gm.GetPanelRoundTwoAnchor().Y);
            return anchorPoint;
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

        public void Common_PictureBoxPlayerClick(int playerID, object sender, ref bool discardMode)
        {
            if (discardMode && playerID == DealerID)
            {
                AddToLog($"Dealer discarded");
                dictPlayer[DealerID].Pickup((PictureBox)sender, lstDeck[Constants.TURNUP_INDEX]);
                AddToLog($"Player #:{DealerID} New Hand Value: {dictPlayer[DealerID].EvaluateHand(TurnupCard.cSuit)}");
                Application.DoEvents();
                discardMode = false;
                _gm.SetlblDiscardVisibility(false);
                dictPlayer[DealerID].UpdateCallArrow(false);
            }
        }

        public Point GetHomePt()
        {
            return _gm.GetHomePt();
        }

        public void SetDiscardVisibility(bool state)
        {
            dictPlayer[DealerID].UpdateDiscard(state);
        }
    }
}
