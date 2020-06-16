namespace ECommerceCase.Core.Models.Monetary
{
	public struct CurrencyModel
	{
		#region Supported Currencies

		public static CurrencyModel TRY = new CurrencyModel(CurrencyEnum.TRY, "₺");
		public static CurrencyModel EUR = new CurrencyModel(CurrencyEnum.EUR, "€");
		public static CurrencyModel USD = new CurrencyModel(CurrencyEnum.USD, "$");

		#endregion
		
		#region Properties

		public CurrencyEnum Value { get; set; }
		
		public string Symbol { get; set; }
		
		public double Coefficient { get; set; }

		#endregion

		#region Constructors
		
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="value"></param>
		/// <param name="symbol"></param>
		private CurrencyModel(CurrencyEnum value, string symbol)
		{
			this.Value = value;
			this.Symbol = symbol;
			this.Coefficient = 1.0d;
		}
		
		#endregion

		#region Methods

		public override string ToString()
		{
			return this.Symbol;
		}

		#endregion
	}

	public enum CurrencyEnum
	{
		TRY,
		EUR,
		USD
	}
}