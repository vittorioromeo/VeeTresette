using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VeeTresette
{
    public static class AI
    {
        public static Game Game { get; set; }
        public static GameForm GameForm { get; set; }

        public static Card CalculateNextMove(Player mPlayer)
        {
            if (Game.Table.Count == 0)
            {
                if (Game.OutOfGame.Count > 0)
                {
                    #region "Finds eventual aces"
                    List<Card> handAces = new List<Card>();

                    foreach (Card card in mPlayer.Hand)
                    {
                        if (card.Value == Card.EValue.Ace)
                            handAces.Add(card);
                    }
                    #endregion

                    if (handAces.Count > 0)
                    {
                        foreach (Card cardAce in handAces)
                        {
                            #region "Checks if two and three of same suit as the ace are out of game"
                            bool twoOut = false;
                            bool threeOut = false;

                            foreach (Card card in Game.OutOfGame)
                            {
                                if (card.Suit
                                    == cardAce.Suit)
                                {
                                    if (card.Value
                                        == Card.EValue.Two)
                                    {
                                        twoOut = true;
                                    }

                                    if (card.Value
                                        == Card.EValue.Three)
                                    {
                                        threeOut = true;
                                    }
                                }
                            }
                            #endregion

                            if (twoOut && threeOut)
                                return cardAce;
                        }
                    }

                    List<Card> scoringCards = new List<Card>();

                    foreach (Card card in mPlayer.Hand)
                    {
                        if (card.Score > 0 && IsCardUnbeatable(card))
                            return card;

                        if (card.Score > 0)
                            scoringCards.Add(card);
                    }

                    if (scoringCards.Count > 0)
                        return WorstRankCard(scoringCards);
                }
                else
                {
                    #region "Finds eventual aces"
                    List<Card> handAces = new List<Card>();

                    foreach (Card card in mPlayer.Hand)
                    {
                        if (card.Value == Card.EValue.Ace)
                            handAces.Add(card);
                    }
                    #endregion

                    if (handAces.Count == 0)
                    {
                        Card.ESuit biggestCardSuit = Card.ESuit.Cups;

                        #region "Counts cards by suits and places them in 4 different lists"
                        List<Card> handCups = new List<Card>();
                        List<Card> handCoins = new List<Card>();
                        List<Card> handSwords = new List<Card>();
                        List<Card> handStaves = new List<Card>();

                        int handCupsNumber = 0;
                        int handCoinsNumber = 0;
                        int handSwordsNumber = 0;
                        int handStavesNumber = 0;
                        int handBiggestNumber = 0;

                        foreach (Card card in mPlayer.Hand)
                        {
                            switch (card.Suit)
                            {
                                case Card.ESuit.Cups:
                                    handCupsNumber++;
                                    handCups.Add(card);
                                    break;
                                case Card.ESuit.Coins:
                                    handCoinsNumber++;
                                    handCoins.Add(card);
                                    break;
                                case Card.ESuit.Swords:
                                    handSwordsNumber++;
                                    handSwords.Add(card);
                                    break;
                                case Card.ESuit.Staves:
                                    handStavesNumber++;
                                    handStaves.Add(card);
                                    break;
                            }
                        }
                        #endregion

                        #region "Finds biggest number of cards per suit"
                        handBiggestNumber = 0;

                        if (handBiggestNumber < handCupsNumber && handCupsNumber > 0)
                        {
                            handBiggestNumber = handCupsNumber;
                            biggestCardSuit = Card.ESuit.Cups;
                        }

                        if (handBiggestNumber < handCoinsNumber && handCoinsNumber > 0)
                        {
                            handBiggestNumber = handCoinsNumber;
                            biggestCardSuit = Card.ESuit.Coins;
                        }

                        if (handBiggestNumber < handSwordsNumber && handSwordsNumber > 0)
                        {
                            handBiggestNumber = handSwordsNumber;
                            biggestCardSuit = Card.ESuit.Swords;
                        }

                        if (handBiggestNumber < handStavesNumber && handStavesNumber > 0)
                        {
                            handBiggestNumber = handStavesNumber;
                            biggestCardSuit = Card.ESuit.Staves;
                        }
                        #endregion

                        List<Card> biggestCardSuitCards = new List<Card>();
                        List<Card> biggestCardSuitCardsThatScore = new List<Card>();

                        foreach (Card card in mPlayer.Hand)
                        {
                            if (card.Suit == biggestCardSuit)
                            {
                                biggestCardSuitCards.Add(card);

                                if (card.Score > 0)
                                {
                                    biggestCardSuitCardsThatScore.Add(card);
                                }
                            }
                        }

                        if (biggestCardSuitCardsThatScore.Count > 0)
                        {
                            return WorstRankCard(biggestCardSuitCardsThatScore);
                        }
                        else
                        {
                            return WorstRankCard(biggestCardSuitCards);
                        }
                    }
                    else if (handAces.Count > 0)
                    {
                        Card.ESuit lowestCardSuit = Card.ESuit.Cups;

                        #region "Counts cards by suits and places them in 4 different lists"
                        List<Card> handCups = new List<Card>();
                        List<Card> handCoins = new List<Card>();
                        List<Card> handSwords = new List<Card>();
                        List<Card> handStaves = new List<Card>();

                        int handCupsNumber = 0;
                        int handCoinsNumber = 0;
                        int handSwordsNumber = 0;
                        int handStavesNumber = 0;
                        int handLowestNumber = 0;

                        foreach (Card card in mPlayer.Hand)
                        {
                            switch (card.Suit)
                            {
                                case Card.ESuit.Cups:
                                    handCupsNumber++;
                                    handCups.Add(card);
                                    break;
                                case Card.ESuit.Coins:
                                    handCoinsNumber++;
                                    handCoins.Add(card);
                                    break;
                                case Card.ESuit.Swords:
                                    handSwordsNumber++;
                                    handSwords.Add(card);
                                    break;
                                case Card.ESuit.Staves:
                                    handStavesNumber++;
                                    handStaves.Add(card);
                                    break;
                            }
                        }
                        #endregion

                        #region "Finds lowest number of cards per suit"
                        handLowestNumber = 999;

                        if (handLowestNumber > handCupsNumber && handCupsNumber > 0)
                        {
                            handLowestNumber = handCupsNumber;
                            lowestCardSuit = Card.ESuit.Cups;
                        }

                        if (handLowestNumber > handCoinsNumber && handCoinsNumber > 0)
                        {
                            handLowestNumber = handCoinsNumber;
                            lowestCardSuit = Card.ESuit.Coins;
                        }

                        if (handLowestNumber > handSwordsNumber && handSwordsNumber > 0)
                        {
                            handLowestNumber = handSwordsNumber;
                            lowestCardSuit = Card.ESuit.Swords;
                        }

                        if (handLowestNumber > handStavesNumber && handStavesNumber > 0)
                        {
                            handLowestNumber = handStavesNumber;
                            lowestCardSuit = Card.ESuit.Staves;
                        }
                        #endregion

                        Card handAce = handAces.First();

                        foreach (Card card in handAces)
                        {
                            if (card.Suit == lowestCardSuit)
                            {
                                handAce = card;
                            }
                        }

                        List<Card> handAceSuitCards = new List<Card>();
                        List<Card> handAceSuitCardsThatScore = new List<Card>();

                        foreach (Card card in mPlayer.Hand)
                        {
                            if (card.Suit == handAce.Suit && card != handAce)
                            {
                                handAceSuitCards.Add(card);

                                if (card.Score > 0)
                                {
                                    handAceSuitCardsThatScore.Add(card);
                                }
                            }
                        }

                        if (handAceSuitCardsThatScore.Count > 0)
                        {
                            return WorstRankCard(handAceSuitCardsThatScore);
                        }
                        else
                        {
                            if (handAceSuitCards.Count > 0)
                            {
                                return WorstRankCard(handAceSuitCards);
                            }
                            else
                            {
                                Card.ESuit biggestCardSuit = Card.ESuit.Cups;

                                int handBiggestNumber = 0;

                                #region "Finds biggest number of cards per suit"
                                handBiggestNumber = handCupsNumber;

                                if (handBiggestNumber < handCoinsNumber && handCoinsNumber > 0)
                                {
                                    handBiggestNumber = handCoinsNumber;
                                    biggestCardSuit = Card.ESuit.Coins;
                                }

                                if (handBiggestNumber < handSwordsNumber && handSwordsNumber > 0)
                                {
                                    handBiggestNumber = handSwordsNumber;
                                    biggestCardSuit = Card.ESuit.Swords;
                                }

                                if (handBiggestNumber < handStavesNumber && handStavesNumber > 0)
                                {
                                    handBiggestNumber = handStavesNumber;
                                    biggestCardSuit = Card.ESuit.Staves;
                                }
                                #endregion

                                List<Card> biggestCardSuitCards = new List<Card>();
                                List<Card> biggestCardSuitCardsThatScore = new List<Card>();

                                foreach (Card card in mPlayer.Hand)
                                {
                                    if (card.Suit == biggestCardSuit)
                                    {
                                        biggestCardSuitCards.Add(card);

                                        if (card.Score > 0)
                                        {
                                            biggestCardSuitCardsThatScore.Add(card);
                                        }
                                    }
                                }

                                if (biggestCardSuitCardsThatScore.Count > 0)
                                {
                                    return WorstRankCard(biggestCardSuitCardsThatScore);
                                }
                                else
                                {
                                    return WorstRankCard(biggestCardSuitCards);
                                }
                            }
                        }
                    }
                }

                // carte fuori dal gioco
                // prima controlla se ha assi - se ha un asso controlla se 2 e 3 di quel seme sono usciti fuori dal gioco -> se si lo gioca
                // se non ha assi controlla tutte le carte che ha in mano allora ne sceglie una a caso che non può essere battuta per fare putno
                // se non ce n'è nessuna che non può essere battuta allora sceglie casualmente ma che sempre gli fa fare punto

                // nessuna carta fuori
                // prima controlla se ha più assi e sceglie il seme del numero minore di carte tra quelli degli assi
                // controlla se ha qualche asso, se si, gioca una carta del seme dell'asso - la peggiore che gli fa fare punto -> altrimenti tira un liscio sempre del seme dell'asso
                // se non ci sono carte conosciute fuori dal gioco o pescate e non si hanno gli assi allora gioca una carta di un seme di cui ha più carte (sempre la carta peggiore che gli fa fare minore punteggio)
            }
            else if (Game.Table.Count > 0)
            {
                #region "Put all playable cards in a list (same suit)"
                List<Card> playableCards = new List<Card>();

                Card.ESuit currentSuit = Game.Table[0].Suit;
                bool hasSameSuit = false;

                foreach (Card card in mPlayer.Hand)
                {
                    if (card.Suit == currentSuit)
                    {
                        hasSameSuit = true;
                        playableCards.Add(card);
                    }
                }
                #endregion

                if (hasSameSuit)
                {
                    #region "Finds best card to beat on the table"
                    List<Card> cardsToBeat = new List<Card>();

                    foreach (Card card in Game.Table)
                    {
                        if (card.Suit == currentSuit)
                        {
                            cardsToBeat.Add(card);
                        }
                    }

                    Card bestCardToBeat = BestRankCard(cardsToBeat);
                    #endregion

                    #region "Finds playable cards that beat the best card on the table (and give score) and an eventual ace"
                    List<Card> handCardsThatBeat = new List<Card>();
                    Card handAce = null;

                    foreach (Card card in playableCards)
                    {
                        if (card.IsWinnerAgainst(bestCardToBeat) && card.Score > 0)
                            handCardsThatBeat.Add(card);

                        if (card.Value == Card.EValue.Ace)
                            handAce = card;
                    }
                    #endregion

                    if (handAce != null)
                    {
                        #region "Checks if two and three are out of game"
                        bool twoOut = false;
                        bool threeOut = false;

                        foreach (Card card in Game.OutOfGame)
                        {
                            if (card.Suit == currentSuit)
                            {
                                if (card.Value == Card.EValue.Two)
                                {
                                    twoOut = true;
                                }

                                if (card.Value == Card.EValue.Three)
                                {
                                    threeOut = true;
                                }
                            }
                        }
                        #endregion

                        if (twoOut && threeOut)
                            return handAce;
                    }

                    if (handCardsThatBeat.Count > 0)
                    {
                        return WorstRankCard(handCardsThatBeat);
                    }
                    else
                    {
                        return WorstRankCard(playableCards);
                    }
                }
                else
                {
                    /*
                    Card.ESuit lowestCardSuit = Card.ESuit.Cups;

                    #region "Counts cards by suits and places them in 4 different lists"

                    List<Card> handCups = new List<Card>();
                    List<Card> handCoins = new List<Card>();
                    List<Card> handSwords = new List<Card>();
                    List<Card> handStaves = new List<Card>();

                    int handCupsNumber = 0;
                    int handCoinsNumber = 0;
                    int handSwordsNumber = 0;
                    int handStavesNumber = 0;
                    int handLowestNumber = 0;

                    foreach (Card card in mPlayer.Hand)
                    {
                        switch (card.Suit)
                        {
                            case Card.ESuit.Cups:
                                handCupsNumber++;
                                handCups.Add(card);
                                break;
                            case Card.ESuit.Coins:
                                handCoinsNumber++;
                                handCoins.Add(card);
                                break;
                            case Card.ESuit.Swords:
                                handSwordsNumber++;
                                handSwords.Add(card);
                                break;
                            case Card.ESuit.Staves:
                                handStavesNumber++;
                                handStaves.Add(card);
                                break;
                        }
                    }
                    #endregion

                    #region "Finds lowest number of cards per suit"
                    handLowestNumber = 999;

                    if (handLowestNumber > handCupsNumber && handCupsNumber > 0)
                    {
                        handLowestNumber = handCupsNumber;
                        lowestCardSuit = Card.ESuit.Cups;
                    }

                    if (handLowestNumber > handCoinsNumber && handCoinsNumber > 0)
                    {
                        handLowestNumber = handCoinsNumber;
                        lowestCardSuit = Card.ESuit.Coins;
                    }

                    if (handLowestNumber > handSwordsNumber && handSwordsNumber > 0)
                    {
                        handLowestNumber = handSwordsNumber;
                        lowestCardSuit = Card.ESuit.Swords;
                    }

                    if (handLowestNumber > handStavesNumber && handStavesNumber > 0)
                    {
                        handLowestNumber = handStavesNumber;
                        lowestCardSuit = Card.ESuit.Staves;
                    }
                    #endregion

                    switch (lowestCardSuit)
                    {
                        case Card.ESuit.Cups:
                            return WorstRankCard(handCups);
                            break;
                        case Card.ESuit.Coins:
                            return WorstRankCard(handCoins);
                            break;
                        case Card.ESuit.Swords:
                            return WorstRankCard(handSwords);
                            break;
                        case Card.ESuit.Staves:
                            return WorstRankCard(handStaves);
                            break;
                    }
                     */

                    return WorstRankCard(mPlayer.Hand);
                }
            }

            MessageBox.Show("Returning first hand card.");
            return mPlayer.Hand.First();
        }

        public static Card BestRankCard(List<Card> mCards)
        {
            Card result = mCards.First();

            foreach (Card card in mCards)
            {
                if (card.Rank < result.Rank)
                {
                    result = card;
                }
            }

            return result;
        }
        public static Card WorstRankCard(List<Card> mCards)
        {
            Card result = mCards.First();

            foreach (Card card in mCards)
            {
                if (card.Rank > result.Rank)
                {
                    result = card;
                }
            }

            return result;
        }
        public static Card BestScoreCard(List<Card> mCards)
        {
            Card result = mCards.First();

            foreach (Card card in mCards)
            {
                if (card.Score < result.Score)
                {
                    result = card;
                }
            }

            return result;
        }
        public static Card WorstScoreCard(List<Card> mCards)
        {
            Card result = mCards.First();

            foreach (Card card in mCards)
            {
                if (card.Score > result.Score)
                {
                    result = card;
                }
            }

            return result;
        }
        public static bool IsCardUnbeatable(Card mCard)
        {
            int cardRank = mCard.Rank;
            List<Card> outOfGameCardsThatBeat = new List<Card>();

            foreach (Card card in Game.OutOfGame)
            {
                if (card.Suit == mCard.Suit)
                {
                    if (card.IsWinnerAgainst(mCard))
                    {
                        outOfGameCardsThatBeat.Add(card);
                    }
                }
            }

            outOfGameCardsThatBeat.Sort(
                delegate(Card p1, Card p2)
                {
                    return p1.Rank.CompareTo(p2.Rank);
                }
            );

            outOfGameCardsThatBeat.Reverse();

            int currentRank = cardRank - 1;

            foreach (Card card in outOfGameCardsThatBeat)
            {
                if (currentRank < 0)
                {
                    return true;
                }

                if (card.Rank == currentRank)
                {
                    currentRank--;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }
    }
}
