using OnlineEuchre.Classes.Static;
using OnlineEuchre.Extras;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static OnlineEuchre.Extras.Constants;

namespace OnlineEuchre.Classes
{
    public class Player : Hand
    {
        public Player(int id, PlayerManager manager, PictureBox pb1, PictureBox pb2, PictureBox pb3,
                      PictureBox pb4, PictureBox pb5, PictureBox pbPlay,
                      PictureBox pbCall, PictureBox pbTrump, PictureBox pbTurnup, 
                      PictureBox pbArrow, PictureBox pbDeal, PictureBox pbDiscard,
                      Label lblWait, float orient, float arrowOrient, float dealerOrient) : base(orient)
        {
            _manager = manager;
            _Id = id;
            _pb1 = pb1;
            _pb2 = pb2;
            _pb3 = pb3;
            _pb4 = pb4;
            _pb5 = pb5;
            _pbPlay = pbPlay;
            _pbCall = pbCall;
            _pbTrump = pbTrump;
            _pbTurnup = pbTurnup;
            _pbArrow = pbArrow;
            _pbDeal = pbDeal;
            _pbDiscard = pbDiscard;
            _lblWait = lblWait;
            _arrowOrient = arrowOrient;
            _dealerOrient = dealerOrient;
            pbCardArray[0] = _pb1;
            pbCardArray[1] = _pb2;
            pbCardArray[2] = _pb3;
            pbCardArray[3] = _pb4;
            pbCardArray[4] = _pb5;
        }

        private PlayerManager _manager { get; set; }
        private int _Id { get; set; }
        private PictureBox _pb1 { get; set; }
        private PictureBox _pb2 { get; set; }
        private PictureBox _pb3 { get; set; }
        private PictureBox _pb4 { get; set; }
        private PictureBox _pb5 { get; set; }
        private PictureBox _pbPlay { get; set; }
        private PictureBox _pbCall { get; set; }
        private PictureBox _pbTrump { get; set; }
        private PictureBox _pbTurnup { get; set; }
        private PictureBox _pbArrow { get; set; }
        private PictureBox _pbDeal { get; set; }
        private PictureBox _pbDiscard { get; set; }
        private Label _lblWait { get; set; }
        private float _arrowOrient { get; set; }
        private float _dealerOrient { get; set; }
        public void LoadBack()
        {
            foreach ( var pb in pbCardArray)
            {
                pb.Image = LoadCardSingleton.CardBack;
            }
        }

        public List<Card> GetHand()
        {
            return lstCards;
        }
        public void Pickup(PictureBox pb, int index)
        {
            Card prevCard = dictHand[pb];
            Card turnupCrd = LoadCardSingleton.GetByElement(index - 1);
            dictHand[pb] = turnupCrd;
            pb.Image = CommonMod.RotateBitmap(dictHand[pb].cImage, _orient);
            Card newCard = dictHand[pb];
            _pbTurnup.Image = null;
            _pbTurnup.Visible = false;
        }

        public void SetTrump(Constants.Suit suit)
        {
            Bitmap trumpImage = null;
            switch (suit)
            {
                case Constants.Suit.Club:
                    trumpImage = LoadCardSingleton.ClubTrump;
                    break;
                case Constants.Suit.Diamond:
                    trumpImage = LoadCardSingleton.DiamondTrump;
                    break;
                case Constants.Suit.Heart:
                    trumpImage = LoadCardSingleton.HeartTrump;
                    break;
                case Constants.Suit.Spade:
                    trumpImage = LoadCardSingleton.SpadeTrump;
                    break;
            }
            _pbTrump.Image = trumpImage;
        }

        public void ClearTrump()
        {
            _pbTrump.Image = null;
            //_pbTrump.Visible = false;
        }

        public void UpdateCallArrow(bool state)
        {
            _pbArrow.Image = CommonMod.RotateBitmap(LoadCardSingleton.ArrowCoin, _arrowOrient);
            _pbArrow.Visible = state;
        }

        public void ClearCallArrow()
        {
            _pbArrow.Image = null;
            _pbArrow.Visible = false;
        }
        public void UpdateDealer(bool state)
        {
            _pbDeal.Image = CommonMod.RotateBitmap(LoadCardSingleton.DealerCoin, _dealerOrient);
            _pbDeal.Visible = state;
        }
        public void ClearDealer()
        {
            _pbDeal.Image = null;
            _pbDeal.Visible = false;
        }
        public void UpdateCall(bool state)
        {
            _pbCall.Image = CommonMod.RotateBitmap(LoadCardSingleton.CallCoin, _dealerOrient);
            _pbCall.Visible = state;
        }
        public void ClearCall()
        {
            _pbCall.Image = null;
            _pbCall.Visible = false;
        }

        public void UpdateDiscard(bool state)
        {
            _pbDiscard.Image = CommonMod.RotateBitmap(LoadCardSingleton.DiscardLabel, _dealerOrient);
            _pbDiscard.Visible = state;
        }

        public void ClearDiscard()
        {
            _pbDiscard.Image = null;
            _pbDiscard.Visible = false;
        }
        public Point GetAskAnchorLocation()
        {
            Point anchorPoint = new Point(_manager.GetHomePt().X + _pbArrow.Right, _manager.GetHomePt().Y + _pbArrow.Top);
            return anchorPoint;
        }

        public void UpdateTimeToWait(int timeToWait)
        {
            _lblWait.Visible = (timeToWait > 0);
            _lblWait.Text = $"{timeToWait}";
        }

        public void ResetControls()
        {
            SuitsInHand.Clear();
            ClearDealer();
            ClearDiscard();
            ClearCall();
            ClearTrump();
            ClearHand();
            ClearCallArrow();
        }
    }
}
