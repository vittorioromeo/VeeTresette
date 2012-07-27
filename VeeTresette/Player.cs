#region
using System.Collections.Generic;
#endregion

namespace VeeTresette
{
    public class Player
    {
        public Game Game { get; set; }
        public List<Card> Hand { get; set; }
        public List<Card> Taken { get; set; }
        public List<Card> KnownCards { get; set; }
        public string Name { get; set; }
        public bool AI { get; set; }
        public int Score { get; set; }

        public Player(Game mGame, string mName, bool mAI)
        {
            Game = mGame;
            Hand = new List<Card>();
            Taken = new List<Card>();
            KnownCards = new List<Card>();
            Name = mName;
            AI = mAI;
            Score = 0;
        }

        public void GiveCardByDeal(Card mCard, bool hidden)
        {
            Hand.Add(mCard);

            if (hidden == false)
            {
                KnownCards.Add(mCard);
            }
        }
        public void PlayedCard(Card mCard)
        {
            mCard.LastPlayer = this;
            Game.Table.Add(mCard);
            Hand.Remove(mCard);
            Game.OutOfGame.Add(mCard);
        }

        public void CalculateScore()
        {
            Score = 0;

            foreach (Card card in Taken)
            {
                Score += card.Score;
            }
        }
    }
}