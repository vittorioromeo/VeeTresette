using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VeeTresette
{
    public class Game
    {
        public GameForm GameForm { get; set; }
        public List<Card> Deck { get; set; }
        public List<Card> Table { get; set; }
        public List<Card> OutOfGame { get; set; }
        public List<Player> Players { get; set; }
        public Player CurrentPlayer { get; set; }
        public int Turn { get; set; }

        public Game()
        {
            Initialize();
        }

        public void Initialize()
        {
            InitializeVariables();
            InitializeDeck();
        }
        public void InitializeVariables()
        {
            Turn = 0;
            Deck = new List<Card>();
            Table = new List<Card>();
            OutOfGame = new List<Card>();
            Players = new List<Player>();
        }
        public void InitializeDeck()
        {
            for (int iSuit = 0; iSuit < 4; iSuit++)
            {
                for (int iValue = 0; iValue < 10; iValue++)
                {
                    Array suits = Enum.GetValues(typeof(Card.ESuit));
                    Array values = Enum.GetValues(typeof(Card.EValue));
                    Deck.Add(new Card((Card.ESuit)suits.GetValue(iSuit), (Card.EValue)values.GetValue(iValue)));
                }
            }
        }

        public void NewGame(List<Player> mPlayers)
        {
            Initialize();

            foreach (Player player in mPlayers)
            {
                Players.Add(player);
            }

            NewGameDealCards();

            CurrentPlayer = Players[0];
        }
        public void NewGameDealCards()
        {
            foreach (Player player in Players)
            {
                GameForm.AddLog(string.Format("Dealing 10 cards to {0}.", player.Name));

                for (int i = 0; i < 10; i++)
                {
                    Card tempCard = GetRandomDeckCard();
                    Deck.Remove(tempCard);
                    player.GiveCardByDeal(tempCard, true);
                }
            }
        }

        public Card GetRandomDeckCard()
        {
            if (Deck.Count > 0)
            {
                int index = Helper.Random.Next(0, Deck.Count);
                return Deck[index];
            }

            return null;
        }

        public void Next(int selectedCard)
        {
            if (CurrentPlayer.AI == false)
            {
                if (Table.Count == 0)
                {
                    NextPlayCard(CurrentPlayer.Hand[selectedCard]);
                }
                else if (Table.Count > 0)
                {
                    Card.ESuit suit = Table[0].Suit;

                    if (CurrentPlayer.Hand[selectedCard].Suit == suit)
                    {
                        NextPlayCard(CurrentPlayer.Hand[selectedCard]);
                    }
                    else
                    {
                        bool hasSameSuit = false;

                        foreach (Card card in CurrentPlayer.Hand)
                        {
                            if (card.Suit == suit)
                            {
                                hasSameSuit = true;
                            }
                        }

                        if (hasSameSuit)
                        {
                            GameForm.AddLog(string.Format("{0} must play a card of the same suit.", CurrentPlayer.Name));
                        }
                        else
                        {
                            NextPlayCard(CurrentPlayer.Hand[selectedCard]);
                        }
                    }
                }
            }
            else
            {
                NextPlayCard(AI.CalculateNextMove(CurrentPlayer));
            }
        }
        public void NextPlayCard(Card mCardToPlay)
        {
            GameForm.AddLog(string.Format("{0} plays {1}", CurrentPlayer.Name, mCardToPlay.ToString()));

            CurrentPlayer.PlayedCard(mCardToPlay);

            bool lastPlayer = (Table.Count == Players.Count);

            if (lastPlayer)
            {
                Card bestCard = Table.First();

                for (int i = 0; i < Table.Count; i++)
                {
                    if (Table[i].IsWinnerAgainst(bestCard))
                        bestCard = Table[i];
                }

                Player winnerPlayer = bestCard.LastPlayer;

                GameForm.AddLog(string.Format("{0} is the best card, played by {1}", bestCard.ToString(), winnerPlayer.Name));

                for (int index = 0; index < Table.Count; index++)
                {
                    Card card = Table[index];
                    winnerPlayer.Taken.Add(card);
                    GameForm.AddLog(string.Format("{0} wins {1}", winnerPlayer.Name, card.ToString()));
                }

                Table.Clear();

                NextPlayer(winnerPlayer);
                NextDeal();
            }
            else
            {
                NextPlayer();
            }         
        }
        public void NextPlayer(Player mWinner = null)
        {
            if (mWinner == null)
            {
                int currentPlayerIndex = Players.IndexOf(CurrentPlayer);

                if (currentPlayerIndex < Players.Count - 1)
                {
                    currentPlayerIndex++;
                }
                else if (currentPlayerIndex == Players.Count - 1)
                {
                    currentPlayerIndex = 0;
                }

                CurrentPlayer = Players[currentPlayerIndex];
            }
            else
            {
                CurrentPlayer = mWinner;
            }

            GameForm.AddLog(string.Format("Next player: {0}", CurrentPlayer.Name));
        }
        public void NextDeal()
        {
            int currentPlayerIndex = Players.IndexOf(CurrentPlayer);

            for (int i = 0; i < Players.Count; i++)
            {
                if (Deck.Count > 0)
                {
                    Card cardToDeal = GetRandomDeckCard();
                    GameForm.AddLog(string.Format("Dealing a card to {0}: {1}", Players[currentPlayerIndex].Name, cardToDeal.ToString()));
                    Players[currentPlayerIndex].GiveCardByDeal(cardToDeal, false);
                    Deck.Remove(cardToDeal);
                }

                if (currentPlayerIndex < Players.Count - 1)
                {
                    currentPlayerIndex++;
                }
                else if (currentPlayerIndex == Players.Count - 1)
                {
                    currentPlayerIndex = 0;
                }
            }
        }
    }
}
