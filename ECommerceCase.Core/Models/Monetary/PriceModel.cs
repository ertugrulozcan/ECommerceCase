using System;

namespace ECommerceCase.Core.Models.Monetary
{
	public struct PriceModel
	{
		#region Properties

		public double Value { get; set; }
		
		public CurrencyModel Currency { get; set; }

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="value"></param>
		/// <param name="currency"></param>
		public PriceModel(double value, CurrencyModel currency)
		{
			this.Value = value;
			this.Currency = currency;
		}

		#endregion

		#region Methods

		public override string ToString()
		{
			return $"{this.Currency} {this.Value:0.##}";
		}

		#endregion
	}
}