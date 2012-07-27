namespace VeeTresette
{
	public class Card
	{
		#region ESuit enum
		public enum ESuit
		{
			Cups = 0,
			Coins = 1,
			Swords = 2,
			Staves = 3
		}
		#endregion
		#region EValue enum
		public enum EValue
		{
			Ace = 2,
			Two = 1,
			Three = 0,
			Four = 9,
			Five = 8,
			Six = 7,
			Seven = 6,
			Knave = 5,
			Knight = 4,
			King = 3
		}
		#endregion

		public Card(ESuit mSuit, EValue mValue)
		{
			Suit = mSuit;
			Value = mValue;
			Initialize();
		}

		public ESuit Suit { get; private set; }
		public EValue Value { get; private set; }
		public int Score { get; private set; }
		public int Rank { get; private set; }
		public Player LastPlayer { get; set; }

		public void Initialize()
		{
			InitializeVariables();
		}

		public void InitializeVariables()
		{
			switch (Value)
			{
				case EValue.Ace:
					Score = 3;
					break;
				case EValue.Knave:
					Score = 1;
					break;
				case EValue.Knight:
					Score = 1;
					break;
				case EValue.King:
					Score = 1;
					break;
				case EValue.Three:
					Score = 1;
					break;
				case EValue.Two:
					Score = 1;
					break;
				default:
					Score = 0;
					break;
			}

			Rank = (int) Value;
		}

		public bool IsWinnerAgainst(Card mCard)
		{
			return Suit == mCard.Suit && Rank < mCard.Rank;
		}

		public override string ToString()
		{
			return string.Format("[{0}] [{1}]", Suit, Value);
		}
	}
}